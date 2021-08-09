using AutoMapper;
using Library.Common.Enums;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Exceptions;
using Library.Logic.EventBus;
using Library.Logic.Logics;
using Library.Services.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Logic.LogicModels
{
    public class LibraryLogic : ILibraryLogic
    {
        private readonly IBookService _bookService;
        private readonly IHolderService _holdersService;
        private readonly INotificationService _notificationService;
        private readonly IKeyWordService _keyWordService;
        private readonly IStatusLogService _statusLogService;
        private readonly IUserService _userService;
        private readonly IRequestClient<IMailSend> _requestClient;
        private readonly IImageLogic _imageLogic;
        private readonly IRaitingBooksService _raitingBooksService;
        private readonly IMapper _mapper;

        public LibraryLogic(IBookService bookService, IHolderService holdersService, INotificationService notificationService, IKeyWordService keyWordService, IStatusLogService statusLogService, IUserService userService, IRequestClient<IMailSend> requestClient, IImageLogic imageLogic, IRaitingBooksService raitingBooksService, IMapper mapper)
        {
            _bookService = bookService;
            _holdersService = holdersService;
            _notificationService = notificationService;
            _keyWordService = keyWordService;
            _statusLogService = statusLogService;
            _userService = userService;
            _requestClient = requestClient;
            _imageLogic = imageLogic;
            _raitingBooksService = raitingBooksService;
            _mapper = mapper;
        }

        public async Task Create(BookViewModel model)
        {
            BookModel book = _mapper.Map<BookModel>(model);

            book.CoverBytes = _imageLogic.ToBytes(model.Cover);
            book.KeyWordsId = _keyWordService.CheckWordByNames(model.KeyWordsName).ToList();
            book.CategoriesId = model.IdCategories;

            await _bookService.Create(book);
        }

        public async Task Update(BookViewModel model, string url)
        {
            BookModel book = _mapper.Map<BookModel>(model);

            if (model.Cover == null)
            {
                book.CoverBytes = _imageLogic.ToBytes(model.Cover);
            }
            else
            {
                book.CoverBytes = null;
            }

            book.KeyWordsId = _keyWordService.CheckWordByNames(model.KeyWordsName);
            book.CategoriesId = model.IdCategories;
            book.Aviable += model.Count - model.PrevCount;

            await _bookService.Update(book);

            if (model.Count > model.PrevCount)
            {
                var listNotification = _notificationService.GetList(model.Id);
                var bookCardURL = url.Replace("UpdateBook", $"BookCard?bookId={book.Id}");

                foreach (var notification in listNotification)
                {
                    var user = await _userService.GetUserById(notification.UserId);

                    await _requestClient.Create(new
                    {
                        MailTo = user.Email,
                        Subject = "Интересующая Вас книга появилась в наличии Библиотеки ММТР",
                        Body = $"Уважаемый(ая) {user.SecondName} {user.FirstName}, книга {book.Title} {book.Author} появилась в библиотеке ММТР. Вы можете взять её по ссылке {bookCardURL}"
                    }).GetResponse<IMailSent>();

                    await _notificationService.Delete(notification);
                }
            }

        }

        public void CheckBook(Guid bookId)
        {
            var holders = _holdersService.GetAllHoldersBook(bookId);
            var notifications = _notificationService.GetList(bookId);

            if (holders.Count() != 0 || notifications.Count() != 0)
            {
                throw new BuisnessException("Данная книга используется пользователями, удаление невозможно");
            }
        }

        public BookViewModel GetBook(Guid bookId)
        {
            var book = _bookService.GetBook(bookId);
            BookViewModel result = _mapper.Map<BookViewModel>(book);

            result.KeyWordsName = _keyWordService.CheckWordById(book.KeyWordsId);
            result.Categories = GetNameCategories(book.CategoriesId);
            result.PrevCount = result.Count;

            return result;
        }

        public async Task Receiving(Guid bookId, string userId)
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

            var result = await _bookService.ReceivingBook(bookId);

            if (result == null)
            {
                throw new BuisnessException("При получении книги возникла ошибка");
            }
        }

        public async Task Return(Guid bookId, string userId, string url)
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

            var result = await _bookService.ReturnBook(bookId);

            var listNotification = _notificationService.GetList(bookId);

            foreach (var notification in listNotification)
            {
                var user = await _userService.GetUserById(notification.UserId);
                var book = _bookService.GetBook(bookId);
                var bookCardURL = url.Replace("UpdateBook", $"BookCard?bookId={book.Id}");
                await _requestClient.Create(new
                {
                    MailTo = user.Email,
                    Subject = "Интересующая Вас книга появилась в наличии Библиотеки ММТР",
                    Body = $"Уважаемый(ая) {user.SecondName} {user.FirstName}, книга {book.Title} {book.Author} появилась в библиотеке ММТР. Вы можете взять её по ссылке {bookCardURL}"

                }).GetResponse<IMailSent>();

                await _notificationService.Delete(notification);
            }

            if (result == null)
            {
                throw new BuisnessException("При возврате книги возникла ошибка");
            }
        }

        public async Task CreateNotification(Guid bookId, string userId)
        {
            NotificationModel notification = new NotificationModel()
            {
                BookId = bookId,
                UserId = userId
            };

            await _notificationService.Create(notification);                  
        }

        public ListBooksViewModel GetAllBook(string userId, SearchViewModel model)
        {
            ListBooksViewModel View = new ListBooksViewModel();
            List<BookModel> books;

            if (model != null)
            {
                books = _bookService.GetAllBooksBySearchModel(model).ToList();
            }
            else
            {
                books = _bookService.GetAllBooks().ToList();
            }

            View.Books = GetListBooks(books, userId);
            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            return View;
        }

        public ListBooksViewModel GetCurrentReadBooks(string userId, SearchViewModel model)
        {

            ListBooksViewModel View = new ListBooksViewModel();
            List<BookModel> books;

            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            if (model != null)
            {
                books = _bookService.GetBooksBySearchModel(View.HoldersList, model).ToList();
            }
            else
            {
                books = _bookService.GetBooks(View.HoldersList).ToList();
            }

            View.Books = GetListBooks(books, userId);

            return View;
        }

        public ListBooksViewModel GetPreviousReadBooks(string userId, SearchViewModel model)
        {
            ListBooksViewModel View = new ListBooksViewModel();
            List<BookModel> books;

            View.HoldersList = _holdersService.GetIdBooksByUser(userId);
            View.NotificationList = _notificationService.GetIdBooksByUser(userId);

            var booksId = _statusLogService.GetListByUserId(userId).ToList();

            if (model != null)
            {
                books = _bookService.GetBooksBySearchModel(booksId.Distinct(), model).ToList();
            }
            else
            {
                books = _bookService.GetBooks(booksId.Distinct()).ToList();
            }

            View.Books = GetListBooks(books, userId);

            return View;
        }

        public BookCardViewModel GetBookCard(Guid bookId, string userId)
        {
            BookCardViewModel View = new BookCardViewModel();

            var book = _bookService.GetBook(bookId);
            var raiting = _raitingBooksService.GetRaiting(bookId);

            View.Book = _mapper.Map<BookViewModel>(book);
            View.Book.Categories = GetNameCategories(book.CategoriesId);
            View.Book.KeyWordsName = _keyWordService.CheckWordById(book.KeyWordsId);
            View.ActiveHolder = _holdersService.CheckHolder(userId, bookId);
            View.Notification = _notificationService.Check(userId, bookId);
            View.Count = raiting.Count();

            if (raiting == null || raiting.Count() == 0)
            {
                View.AllRaiting = 0;
            }
            else
            {
                double averageRaiting = 0;

                foreach (var rait in raiting)
                {
                    averageRaiting += rait.Score;
                }

                View.AllRaiting = averageRaiting / raiting.Count();

                try
                {
                    View.ScoreRaiting = raiting.FirstOrDefault(rait => rait.UserId == userId).Score;
                }
                catch
                {
                    View.ScoreRaiting = 0;
                }
            }
            return View;
        }

        public async Task<ListViewModel<StatusLogViewModel>> GetLogsBook(Guid bookId, int count = 5, int countRequest = 0)
        {
            ListViewModel<StatusLogViewModel> logViews = new ListViewModel<StatusLogViewModel>
            {
                List = new List<StatusLogViewModel>()
            };
            var logsList = _statusLogService.GetListByBookId(bookId);

            if (logsList.Count() != 0)
            {
                var taked = count * countRequest;
                var difference = logsList.Count() - taked;

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
                    var user = await _userService.GetUserById(log.UserId);
                    StatusLogViewModel logView = _mapper.Map<StatusLogViewModel>(log);

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

        public async Task<ListViewModel<ActiveHolderViewModel>> GetHolderBook(Guid bookId, int count = 5, int countRequest = 0)
        {
            ListViewModel<ActiveHolderViewModel> holderViews = new ListViewModel<ActiveHolderViewModel>
            {
                List = new List<ActiveHolderViewModel>()
            };
            var holderList = _holdersService.GetAllHoldersBook(bookId);

            if (holderList.Count() != 0)
            {
                var taked = count * countRequest;
                var difference = holderList.Count() - taked;

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
                    var user = await _userService.GetUserById(holder.UserId);
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

        ICollection<BookViewModel> GetListBooks(IEnumerable<BookModel> books, string userId)
        {
            List<BookViewModel> listBooks = new List<BookViewModel>();

            foreach (var book in books)
            {
                BookViewModel bookView = _mapper.Map<BookViewModel>(book);
                bookView.Raiting = new RaitingBooksViewModel();

                bookView.Categories = GetNameCategories(book.CategoriesId);
                bookView.KeyWordsName = _keyWordService.CheckWordById(book.KeyWordsId);

                var raiting = _raitingBooksService.GetRaiting(book.Id);

                var raitingCurrentUser = raiting.FirstOrDefault(rait => rait.UserId == userId);

                if (raitingCurrentUser != null)
                {
                    bookView.RaitingUser = raitingCurrentUser.Score;
                }
                else
                {
                    bookView.RaitingUser = 0;
                }

                if (raiting == null || raiting.Count() == 0)
                {
                    bookView.Raiting.Score = 0;
                }
                else
                {
                    double averageRaiting = 0;

                    foreach (var rait in raiting)
                    {
                        averageRaiting += rait.Score;
                    }

                    bookView.Raiting.Score = averageRaiting / raiting.Count();
                }
                bookView.Raiting.Count = raiting.Count();

                listBooks.Add(bookView);
            }
            return listBooks;
        }

        /// <summary>
        /// Возвращает список названий категорий по списку их Id
        /// </summary>
        /// <param name="idList"> Список Id категорий </param>
        /// <returns> Список названий категорий </returns>
        ICollection<string> GetNameCategories(IEnumerable<int> idList)
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
