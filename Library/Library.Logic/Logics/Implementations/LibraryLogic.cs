using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Logic.EventBus;
using Library.Logic.Logics;
using Library.Services.Models;
using Library.Services.Services;
using MassTransit;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Logic.LogicModels
{
    public class LibraryLogic :ILibraryLogic
    {

        private readonly IBookService _bookService;
        private readonly IHolderService _holdersService;
        private readonly INotificationService _notificationService;
        private readonly IKeyWordService _keyWordService;
        private readonly IStatusLogService _statusLogService;
        private readonly IUserService _userService;
        private readonly IRequestClient<IMailSend> _requestClient;
        private readonly IImageLogic _imageLogic;

        public LibraryLogic(IBookService bookService, IHolderService holdersService, INotificationService notificationService, IKeyWordService keyWordService, IStatusLogService statusLogService, IUserService userService, IRequestClient<IMailSend> requestClient, IImageLogic imageLogic)
        {

            _bookService = bookService;
            _holdersService = holdersService;
            _notificationService = notificationService;
            _keyWordService = keyWordService;
            _statusLogService = statusLogService;
            _userService = userService;
            _requestClient = requestClient;
            _imageLogic = imageLogic;
        }




        public BookModel Create(BookViewModel model)
        {
            BookModel book = model;

            book.Cover = _imageLogic.ToBytes(model.Cover);
            book.KeyWordsId = _keyWordService.CheckWord(model.KeyWordsName);
            book.Categories = model.IdCategories;

            var result = _bookService.Create(book);

            return result;
        }

        public async Task<BookModel> Update(BookViewModel model, string url)
        {
            BookModel book = model;

            if(model.Cover != null)
            {

                book.Cover = _imageLogic.ToBytes(model.Cover);

            }
            else
            {

                book.Cover = null;

            }

            book.KeyWordsId = _keyWordService.CheckWord(model.KeyWordsName);
            book.Categories = model.IdCategories;
            book.Aviable += model.Count - model.PrevCount;

            var result = await _bookService.Update(book);

            if (model.Count > model.PrevCount)
            {
                var listNotification = _notificationService.GetList(model.Id);
                var bookCardURL = url.Replace("UpdateBook", $"BookCard?bookId={book.Id}");


                foreach (var notification in listNotification)
                {
                    var user = _userService.GetUserById(notification.UserId);

                    await _requestClient.Create(new
                    {
                        // Добавить ссылку на книгу
                        MailTo = user.Email,
                        Subject = "Интересующая Вас книга появилась в наличии ММТР Библиотеки",
                        Body = $"Уважаемый(ая) {user.SecondName} {user.Patronymic}, книга {book.Title} {book.Author} появилась в библиотеке ММТР. Вы можете взять её по ссылке {bookCardURL}"

                    }).GetResponse<IMailSent>();

                    await _notificationService.Delete(notification);
                }
            }

            

            return result;
        }

        public bool CheckBook(Guid bookId)
        {
            var holders = _holdersService.GetAllHoldersBook(bookId);
            var notifications = _notificationService.GetList(bookId);

            if (holders.Count != 0 || notifications.Count != 0)
            {

                return true;

            }
            else
            {

                return false;

            }

        }


        public BookViewModel GetBook(Guid bookId)
        {
            var book = _bookService.GetBook(bookId);
            BookViewModel result = book;

            result.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);
            result.Categories = GetNameCategories(book.Categories);
            result.PrevCount = result.Count;

            return result;
        }

        public async Task<BookModel> Receiving(Guid bookId, string userId)
        {
            ActiveHolderModel newHolder = new ActiveHolderModel()
            {
                BookId = bookId,
                UserId = userId,
                DateOfReceipt = DateTime.Now
            };

            StatusLogModel newLog = new StatusLogModel()
            {
                BookId = bookId,
                UserId = userId,
                Date = DateTime.Now,
                Operation = Operations.Take
            };

            await _holdersService.Create(newHolder);
            await _statusLogService.Create(newLog);

            var result = _bookService.ReceivingBook(bookId);

            return result;

        }

        public async Task<BookModel> Return(Guid bookId, string userId, string url)
        {

            ActiveHolderModel holder = new ActiveHolderModel()
            {
                BookId = bookId,
                UserId = userId,
                DateOfReceipt = DateTime.Now
            };

            StatusLogModel newLog = new StatusLogModel()
            {
                BookId = bookId,
                UserId = userId,
                Date = DateTime.Now,
                Operation = Operations.Returned
            };


            await _holdersService.Delete(holder);
            await _statusLogService.Create(newLog);

            var result = _bookService.ReturnBook(bookId);

            var listNotification = _notificationService.GetList(bookId);
            

            foreach (var notification in listNotification)
            {
                var user = _userService.GetUserById(notification.UserId);
                var book = _bookService.GetBook(bookId);
                var bookCardURL = url.Replace("UpdateBook", $"BookCard?bookId={book.Id}");
                _requestClient.Create(new
                {
                    // Добавить ссылку на книгу
                    MailTo = user.Email,
                    Subject = "Интересующая Вас книга появилась в наличии ММТР Библиотеки",
                    Body = $"Уважаемый(ая) {user.SecondName} {user.Patronymic}, книга {book.Title} {book.Author} появилась в библиотеке ММТР. Вы можете взять её по ссылке {bookCardURL}"

                });

                await _notificationService.Delete(notification);
            }

            return result;
        }

        public async Task<NotificationModel> CreateNotification(Guid bookId, string userId)
        {
            NotificationModel notification = new NotificationModel()
            {
                BookId = bookId,
                UserId = userId
            };

            var result = await _notificationService.Create(notification);

            return result;
        }

        public ListBooksViewModel GetAllBook(string userId, SearchViewModel model)
        {
            ListBooksViewModel View = new ListBooksViewModel();
            List<BookViewModel> listBooks = new List<BookViewModel>();
            List<BookModel> books;

            if (model != null)
            {

                books = _bookService.GetAllBooks(model);

            }
            else
            {

                books = _bookService.GetAllBooks();

            }

            foreach (var book in books)
            {
                BookViewModel bookView = book;
                bookView.Categories = GetNameCategories(book.Categories);
                bookView.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);

                listBooks.Add(bookView);
            }

            View.Books = listBooks;
            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            return View;
        }

        public ListBooksViewModel GetCurrentReadBooks(string userId, SearchViewModel model)
        {

            ListBooksViewModel View = new ListBooksViewModel();
            List<BookViewModel> listBooks = new List<BookViewModel>();
            List<BookModel> books;

            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            if (model != null)
            {

                books = _bookService.GetBooks(View.HoldersList, model);

            }
            else
            {

                books = _bookService.GetBooks(View.HoldersList);

            }

            foreach (var book in books)
            {
                BookViewModel bookView = book;
                bookView.Categories = GetNameCategories(book.Categories);
                bookView.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);

                listBooks.Add(bookView);
            }

            View.Books = listBooks;
            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            return View;
        }

        public ListBooksViewModel GetPreviousReadBooks(string userId, SearchViewModel model)
        {
            ListBooksViewModel View = new ListBooksViewModel();
            List<BookViewModel> listBooks = new List<BookViewModel>();
            List<BookModel> books;
            List<Guid> booksId = new List<Guid>();

            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            var logs = _statusLogService.GetList(userId);

            foreach (var log in logs)
            {
                if (log.Operation == Operations.Returned && !View.HoldersList.Contains(log.BookId))
                {
                    booksId.Add(log.BookId);
                }
            }

            if (model != null)
            {

                books = _bookService.GetBooks(booksId.Distinct().ToList(), model);

            }
            else
            {

                books = _bookService.GetBooks(booksId.Distinct().ToList());

            }

            foreach (var book in books)
            {
                BookViewModel bookView = book;
                bookView.Categories = GetNameCategories(book.Categories);
                bookView.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);

                listBooks.Add(bookView);
            }

            View.Books = listBooks;
            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            return View;
        }

        public BookCardViewModel GetBookCard(Guid bookId, string userId)
        {
            BookCardViewModel View = new BookCardViewModel();

            var book = _bookService.GetBook(bookId);

            View.Book = book;
            View.Book.Categories = GetNameCategories(book.Categories);
            View.Book.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);
            View.ActiveHolder = _holdersService.CheckHolder(userId, bookId);
            View.Notification = _notificationService.Check(userId, bookId);

            return View;
        }

        public ListViewModel<StatusLogViewModel> GetLogsBook(Guid bookId, int count = 5, int countRequest = 0)
        {
            ListViewModel<StatusLogViewModel> logViews = new ListViewModel<StatusLogViewModel>
            {
                List = new List<StatusLogViewModel>()
            };
            var logsList = _statusLogService.GetList(bookId);

            if(logsList.Count != 0)
            {

                var taked = count * countRequest;
                var difference = logsList.Count - taked;

                if (difference > 5)
                {

                    logViews.NextExists = true;

                }
                else
                {

                    logViews.NextExists = false;

                }

                if (difference > 0 && difference < 5)
                {
                    count -= difference;
                }

                foreach (var log in logsList.Skip(taked).Take(count))
                {

                    var user = _userService.GetUserById(log.UserId);
                    StatusLogViewModel logView = log;

                    logView.User = user.SecondName + " " + user.FirstName + " " + user.Patronymic;

                    switch (log.Operation)
                    {

                        case Operations.Take:
                            logView.Operation = "Взял";
                            break;

                        case Operations.Returned:
                            logView.Operation = "Вернул";
                            break;

                    }

                    logViews.List.Add(logView);

                }

                return logViews;

            }
            else
            {

                return null;

            }

        }

        public ListViewModel<ActiveHolderViewModel> GetHolderBook(Guid bookId, int count = 5, int countRequest = 0)
        {
            ListViewModel<ActiveHolderViewModel> holderViews = new ListViewModel<ActiveHolderViewModel>
            {
                List = new List<ActiveHolderViewModel>()
            };
            var holderList = _holdersService.GetAllHoldersBook(bookId);

            if(holderList.Count != 0)
            {

                var taked = count * countRequest;
                var difference = holderList.Count - taked;

                if (difference > 5)
                {

                    holderViews.NextExists = true;

                }
                else
                {

                    holderViews.NextExists = false;

                }

                if (difference > 0 && difference < 5)
                {
                    count -= difference;
                }


                foreach (var holder in holderList.Skip(taked).Take(count))
                {

                    var user = _userService.GetUserById(holder.UserId);
                    ActiveHolderViewModel holderView = new ActiveHolderViewModel
                    {
                        User = user.SecondName + " " + user.FirstName + " " + user.Patronymic,
                        DateOfReceipt = holder.DateOfReceipt
                    };

                    holderViews.List.Add(holderView);

                }

                return holderViews;

            }
            else
            {

                return null;

            }

        }
        public ListBooksViewModel GetMyBooks(string userId)
        {
            ListBooksViewModel View = new ListBooksViewModel();

            var idList = _holdersService.GetIdBooksByUser(userId);
            var books = _bookService.GetBooks(idList);

            foreach (var book in books)
            {
                BookViewModel bookView = book;

                bookView.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);

                View.Books.Add(bookView);
            }

            return View;
        }


        /// <summary>
        /// Возвращает список названий категорий по списку их Id
        /// </summary>
        /// <param name="idList"> Список Id категорий </param>
        /// <returns> Список названий категорий </returns>
        List<string> GetNameCategories(List<int> idList)
        {
            List<string> nameList = new List<string>();

            foreach (var id in idList)
            {
                switch (id)
                {
                    case (int)BookCategory.All:
                        nameList.Add("");
                        break;

                    case (int)BookCategory.Analytics:
                        nameList.Add("Аналитика");
                        break;

                    case (int)BookCategory.Development:
                        nameList.Add("Разработка");
                        break;

                    case (int)BookCategory.Maintenance:
                        nameList.Add("Сопровождение");
                        break;

                    case (int)BookCategory.Management:
                        nameList.Add("Управление");
                        break;

                    case (int)BookCategory.Testing:
                        nameList.Add("Тестирование");
                        break;
                }
            }

            return nameList;
        }

    }
}
