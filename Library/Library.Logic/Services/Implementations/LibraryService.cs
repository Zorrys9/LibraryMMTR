using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Logic.EventBus;
using Library.Logic.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Logic.Services.Implementations
{
    public class LibraryService:ILibraryService
    {

        private readonly IBookRepository _bookRepository;
        private readonly INotificationRepository _notificationRepository;
        private readonly IStatusLogRepository _statusLogRepository;
        private readonly IActiveHoldersRepository _activeHoldersRepository;
        private readonly IKeyWordsRepository _keyWordsRepository;
        private readonly IUsersService _usersService;
        private readonly IRequestClient<IMailSend> _requestClient;
        public LibraryService(IBookRepository bookRepository, INotificationRepository notificationRepository, IStatusLogRepository statusLogRepository, IActiveHoldersRepository activeHoldersRepository, IKeyWordsRepository keyWordsRepository, IUsersService usersService, IRequestClient<IMailSend> requestClient)
        {
            _activeHoldersRepository = activeHoldersRepository;
            _bookRepository = bookRepository;
            _notificationRepository = notificationRepository;
            _statusLogRepository = statusLogRepository;
            _keyWordsRepository = keyWordsRepository;
            _usersService = usersService;
            _requestClient = requestClient;
        }
        /// <summary>
        /// Вывод списка всех книг 
        /// </summary>
        /// <param name="model"> Модель поиска (может быть null) </param>
        /// <returns> Список всех книг </returns>
        public List<BookViewModel> GetAllBooks(SearchViewModel model)
        {
            List<BooksEntityModel> result;
            if (model != null)
                result = _bookRepository.GetBookList(model);
            else result = _bookRepository.GetBookList();


            List<BookViewModel> books = new List<BookViewModel>();
            foreach(var book in result)
            {
                BookModel bookModel = book;
                books.Add(bookModel);
            }
            return books;
        }

        /// <summary>
        /// Создание новой книги
        /// </summary>
        /// <param name="model"> Модель представления книги </param>
        /// <returns> Модель книги </returns>
        public BookModel CreateBook(BookViewModel model)
        {
            if (model != null)
            {
                BookModel newBook = model;
                newBook.KeyWordsId = _keyWordsRepository.ChekKeyWords(model.KeyWordsName);
                newBook.Cover = ByteFromFile(model.Cover);

                var result = _bookRepository.CreateBook(newBook);
                return result;
            }
            else return model;
        }

        /// <summary>
        /// Возвращает список книг текущего пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей книг </returns>
        public List<BookViewModel> GetListBooksByUser(string userId, SearchViewModel model)
        {
            if (userId != null)
            {
                var listId = _activeHoldersRepository.GetBooksByUser(userId);
                List<BooksEntityModel> bookList;
                if (model != null)
                    bookList = _bookRepository.GetBookList(listId);
                else bookList = _bookRepository.GetBookList(listId, model);
                List<BookViewModel> listBooks = new List<BookViewModel>();
                foreach(var book in bookList)
                {
                    BookModel bk = book;
                    listBooks.Add(bk);
                }
                return listBooks;
            }
            else return null;
        }
        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Модель представления книги </param>
        /// <returns> Модель книги </returns>
        public BookModel UpdateBook(BookViewModel model)
        {
            if (model != null)
            {
                BookModel updateModel = model;
                updateModel.KeyWordsId = _keyWordsRepository.ChekKeyWords(model.KeyWordsName);
                updateModel.Cover = ByteFromFile(model.Cover);
                var result = _bookRepository.UpdateBook(updateModel);

                if (result != null)
                {
                    var listNotification = _notificationRepository.GetListNotification(model.Id);

                    foreach (var notification in listNotification)
                    {
                        var user = _usersService.GetUserById(notification.UserId);

                        SendMail(user, result);

                        _notificationRepository.DeleteNotification(notification);
                    }

                    return result;
                }
                else return result;
            }
            else return model;
        }
        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id"> Id книги</param>
        /// <returns> Модель удаленной книги </returns>
        public BookModel DeleteBook(Guid id)
        {
            var result = _bookRepository.DeleteBook(id);
            return result;
        }
        /// <summary>
        /// Возвращает книгу по его Id
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Модель книги </returns>
        public BookModel GetBook(Guid id)
        {
            var result = _bookRepository.GetBook(id);
            return result;
        }

        /// <summary>
        /// Получение книги пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя</param>
        /// <returns> Модель произведенной операции с книгой </returns>
        public StatusLogModel ReceivingBook(Guid bookId, string userId)
        {
            var book = _bookRepository.ReceivingBook(bookId);
            if (book != null)
            {
                _activeHoldersRepository.CreateHolder(userId, bookId);

                StatusLogModel statuslog = new StatusLogModel()
                {
                    BookId = bookId,
                    UserId = userId,
                    DateOfReceipt = DateTime.Today,
                    Operation = Operations.Take
                };

                var result = _statusLogRepository.CreateStatusLog(statuslog);
                return result;
            }
            else return null;
        }
        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя</param>
        /// <returns> Модель произведенной операции с книгой </returns>
        public StatusLogModel ReturnBook(Guid bookId, string userId)
        {
            var book = _bookRepository.ReturnBook(bookId);
            if (book != null)
            {

                _activeHoldersRepository.DeleteHolder(userId, bookId);

                StatusLogModel statuslog = new StatusLogModel()
                {
                    BookId = bookId,
                    UserId = userId,
                    DateOfReceipt = DateTime.Today,
                    Operation = Operations.Returned
                };
                var result = _statusLogRepository.CreateStatusLog(statuslog);
                return result;

            }
            else return null;
        }



        /// <summary>
        /// Создание нового оповещения
        /// </summary>
        /// <param name="userId"> Id текущего пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Id книги /returns>
        public Guid? CreaterNotification(string userId, Guid bookId)
        {
            if (userId != null)
            {
                NotificationEntityModel model = new NotificationEntityModel()
                {
                    BookId = bookId,
                    UserId = userId
                };

                var result = _notificationRepository.CreateNotification(model);
                return result.BookId;

            }
            else return null;
        }

        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="user"> Модель пользователя </param>
        /// <param name="book"> Модель книги </param>
        void SendMail(UserModel user, BookModel book)
        {

            _requestClient.Create(new
            {
                // Добавить ссылку на книгу
                MailTo=user.Email,
                Subject = "Интересующая Вас книга появилась в наличии ММТР Библиотеки",
                Body = $"Уважаемый(ая) {user.SecondName} {user.Patronymic}, книга {book.Title} {book.Author} появилась в библиотеке ММТР. Вы можете взять её"

            });
        }
        /// <summary>
        /// Преобразование в массив байтов из картинки
        /// </summary>
        /// <param name="file"> Файл </param>
        /// <returns> Массив байтов </returns>
        byte[] ByteFromFile(IFormFile file)
        {
            if (file != null)
            {

                byte[] result = null;

                var binaryReader = new BinaryReader(file.OpenReadStream());
                result = binaryReader.ReadBytes((int)file.Length);

                return result;
            }
            else return null;
        }
    }
}
