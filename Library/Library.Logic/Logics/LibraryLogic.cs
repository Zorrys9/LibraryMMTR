using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Logic.EventBus;
using Library.Logic.Logics;
using Library.Services.Models;
using Library.Services.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.LogicModels
{
    public class LibraryLogic
    {

        private readonly IBookService _bookService;
        private readonly IHolderService _holdersService;
        private readonly INotificationService _notificationService;
        private readonly IKeyWordService _keyWordService;
        private readonly IStatusLogService _statusLogService;
        private readonly IUserService _userService;
        private readonly IRequestClient<IMailSend> _requestClient;

        public LibraryLogic(IBookService bookService, IHolderService holdersService, INotificationService notificationService, IKeyWordService keyWordService, IStatusLogService statusLogService, IUserService userService, IRequestClient<IMailSend> requestClient)
        {

            _bookService = bookService;
            _holdersService = holdersService;
            _notificationService = notificationService;
            _keyWordService = keyWordService;
            _statusLogService = statusLogService;
            _userService = userService;
            _requestClient = requestClient;

        }


        /// <summary>
        /// Создание новой книги 
        /// </summary>
        /// <param name="model"> Модель представления новой книги </param>
        /// <returns> Модель новой кнгии </returns>
        public BookModel Create(BookViewModel model)
        {
            BookModel book = model;

            book.Cover = ((model.Cover == null) ? ImageLogic.ToBytes(model.Cover) : null);
            book.KeyWordsId = _keyWordService.CheckWord(model.KeyWordsName);
            book.Categories = model.IdCategories;

            var result = _bookService.Create(book);
            return result;
        }

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Измененная модель книги </param>
        /// <returns> Модель книги после изменения </returns>
        public async Task<BookModel> Update(BookViewModel model)
        {

            BookModel book = model;

            book.Cover = ImageLogic.ToBytes(model.Cover);
            book.KeyWordsId = _keyWordService.CheckWord(model.KeyWordsName);

            if (book.Count > _bookService.CountBooks(model.Id))
            {

                var listNotification = _notificationService.GetList(model.Id);

                foreach(var notification in listNotification)
                {
                    var user = _userService.GetUserById(notification.UserId);

                    _requestClient.Create(new
                    {
                        // Добавить ссылку на книгу
                        MailTo = user.Email,
                        Subject = "Интересующая Вас книга появилась в наличии ММТР Библиотеки",
                        Body = $"Уважаемый(ая) {user.SecondName} {user.Patronymic}, книга {book.Title} {book.Author} появилась в библиотеке ММТР. Вы можете взять её"

                    });

                   await _notificationService.Delete(notification);
                }

            }

            var result = _bookService.Update(book);

            return result;
        }

        /// <summary>
        /// Получение книги пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель взятой книги </returns>
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

            var createHolder = await _holdersService.Create(newHolder);
            var createLog = await _statusLogService.Create(newLog);

            if(createHolder != null && createLog != null)
            {

                var result = _bookService.ReceivingBook(bookId);

                return result;

            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Возврат книги пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель возвращенной книги </returns>
        public async Task<BookModel> Return(Guid bookId, string userId)
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

           
            var deleteHolder = await _holdersService.Delete(holder);
            var createLog = await _statusLogService.Create(newLog);

            if (deleteHolder != null && createLog != null)
            {

                var result = _bookService.ReturnBook(bookId);

                return result;

            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Создание нового оповещения
        /// </summary>
        /// <param name="model">  </param>
        /// <returns></returns>
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

        /// <summary>
        /// Возвращает список книг для вывода на страницу, а так же список оповещений и текущий книг пользователя 
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель представления списка книг </returns>
        public ListBooksViewModel GetAllBook(string userId, SearchViewModel model)
        {

            ListBooksViewModel View = new ListBooksViewModel();
            List<BookViewModel> listBooks = new List<BookViewModel>();
            List<BookModel> books = new List<BookModel>();

            if(model != null)
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

        /// <summary>
        /// Возвращает всю необходимую информацию о нужной книге
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель представления карточки книги </returns>
        public BookCardViewModel GetBookCard(Guid bookId, string userId)
        {
            BookCardViewModel View = new BookCardViewModel();
            List<ActiveHolderViewModel> holderViews = new List<ActiveHolderViewModel>();
            List<StatusLogViewModel> logViews = new List<StatusLogViewModel>();

            var book = _bookService.GetBook(bookId);

            View.Book = book;
            View.Book.Categories = GetNameCategories(book.Categories);
            View.Book.KeyWordsName = _keyWordService.CheckWord(book.KeyWordsId);

                



            View.ActiveHolder = _holdersService.CheckHolder(userId, bookId);
            View.Notification = _notificationService.Check(userId, bookId);

            var holderList = _holdersService.GetAllHoldersBook(bookId);
            var logsList = _statusLogService.GetList(bookId);


            foreach (var holder in holderList)
            {
                var user = _userService.GetUserById(holder.UserId);
                ActiveHolderViewModel holderView = new ActiveHolderViewModel();

                holderView.User = user.SecondName + " " + user.FirstName + " " + user.Patronymic;
                holderView.DateOfReceipt = holder.DateOfReceipt;

                holderViews.Add(holderView);

            }



                foreach (var log in logsList)
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

                    logViews.Add(logView);
                }


            View.Holders = holderViews;
            View.Logs = logViews;

            return View;
        }

        /// <summary>
        /// Возвращает список книг для вывода на страницу, а так же список оповещений и текущий книг пользователя 
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель представления списка книг </returns>
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

        List<string> GetNameCategories(List<int> idList)
        {
            List<string> nameList = new List<string>();

            foreach(var id in idList)
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
