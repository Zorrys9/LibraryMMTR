using Library.Common.Models;
using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Logic.Logics
{
    /// <summary>
    /// Логика работы библиотеки
    /// </summary>
    public interface ILibraryLogic
    {

        /// <summary>
        /// Создание новой книги 
        /// </summary>
        /// <param name="model"> Модель представления новой книги </param>
        /// <returns> Модель новой кнгии </returns>
        Task Create(BookViewModel model);

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Измененная модель книги </param>
        /// <returns> Модель книги после изменения </returns>
        Task Update(BookViewModel model, string url);

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
        Task Receiving(Guid bookId, string userId);

        /// <summary>
        /// Возврат книги пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель возвращенной книги </returns>
        Task Return(Guid bookId, string userId, string url);

        /// <summary>
        /// Создание нового оповещения
        /// </summary>
        /// <param name="model">  </param>
        /// <returns></returns>
        Task CreateNotification(Guid bookId, string userId);

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
        /// Проверка используется ли книга пользователями
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат проверки (true - используется, false - не используется) </returns>
        void CheckBook(Guid bookId);

        /// <summary>
        /// Возвращает список всех пользователей, которые использую книгу
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="count"> Количество возвращаемых записей </param>
        /// <param name="countRequest"> Количество предыдущих запросов </param>
        /// <returns> Список пользователей </returns>
        Task<ListViewModel<ActiveHolderViewModel>> GetHolderBook(Guid bookId, int count = 5, int countRequest = 0);


        /// <summary>
        /// Возвращает список всех действий, которые совершались с книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="count"> Количество возвращаемых записей </param>
        /// <param name="countRequest"> Количество предыдущих запросов </param>
        /// <returns> Список действий </returns>
        Task<ListViewModel<StatusLogViewModel>> GetLogsBook(Guid bookId, int count = 5, int countRequest = 0);

     
    }
}
