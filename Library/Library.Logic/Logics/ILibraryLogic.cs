using Library.Common.ViewModels;
using Library.Services.Models;
using System;
using System.Threading.Tasks;

namespace Library.Logic.Logics
{
    public interface ILibraryLogic
    {

        /// <summary>
        /// Создание новой книги 
        /// </summary>
        /// <param name="model"> Модель представления новой книги </param>
        /// <returns> Модель новой кнгии </returns>
        BookModel Create(BookViewModel model);

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Измененная модель книги </param>
        /// <returns> Модель книги после изменения </returns>
        Task<BookModel> Update(BookViewModel model);

        /// <summary>
        /// Возвращает книгу по его Id
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель представления книги </returns>
        BookViewModel GetBook(Guid bookId);

        /// <summary>
        /// Получение книги пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель взятой книги </returns>
        Task<BookModel> Receiving(Guid bookId, string userId);

        /// <summary>
        /// Возврат книги пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель возвращенной книги </returns>
        Task<BookModel> Return(Guid bookId, string userId);

        /// <summary>
        /// Создание нового оповещения
        /// </summary>
        /// <param name="model">  </param>
        /// <returns></returns>
        Task<NotificationModel> CreateNotification(Guid bookId, string userId);

        /// <summary>
        /// Возвращает список книг для вывода на страницу
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель представления списка книг </returns>
        ListBooksViewModel GetAllBook(string userId, SearchViewModel model);

        /// <summary>
        /// Возвращает список книг в распоряжении у пользоватепля для вывода на страницу
        /// </summary>
        /// <param name="userId"> Id пользоваетеля</param>
        /// <param name="model"> Модель поиска</param>
        /// <returns> Модель представления списка книг</returns>
        ListBooksViewModel GetCurrentReadBooks(string userId, SearchViewModel model);

        /// <summary>
        /// Возвращает список прочитанных пользователем книг для вывода на страницу
        /// </summary>
        /// <param name="userId"> Id пользоваетеля</param>
        /// <param name="model"> Модель поиска</param>
        /// <returns> Модель представления списка книг</returns>
        ListBooksViewModel GetPreviousReadBooks(string userId, SearchViewModel model);

        /// <summary>
        /// Возвращает всю необходимую информацию о нужной книге
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель представления карточки книги </returns>
        BookCardViewModel GetBookCard(Guid bookId, string userId);

        /// <summary>
        /// Возвращает список книг для вывода на страницу, а так же список оповещений и текущий книг пользователя 
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель представления списка книг </returns>
        ListBooksViewModel GetMyBooks(string userId);


    }
}
