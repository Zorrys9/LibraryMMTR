using Library.Common.ViewModels;
using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления книгами сервиса
    /// </summary>
    public interface IBookService
    {

        /// <summary>
        /// Добавляет в БД новую книгу
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель добавленной книги </returns>
        Task Create(BookModel model);

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель измененной книги </returns>
        Task Update(BookModel model);

        /// <summary>
        /// Удаление книги из БД
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель удаленной книги </returns> 
        Task Delete(Guid bookId);

        /// <summary>
        /// Получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель полученной книги </returns>
        Task<BookModel> ReceivingBook(Guid bookId);

        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель возвращаемой книги </returns>
        Task<BookModel> ReturnBook(Guid bookId);

        /// <summary>
        /// Выборка всех книг
        /// </summary>
        /// <returns> Список всех книг </returns>
        ICollection<BookModel> GetAllBooks();

        /// <summary>
        /// Выборка книг по списку их Id 
        /// </summary>
        /// <param name="guidList"> Список Id книг </param>
        /// <returns>Список книг </returns>
        ICollection<BookModel> GetBooks(IEnumerable<Guid> guidList);

        /// <summary>
        /// Выборка всех книг подходящих под условия поиска
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список всех подходящих книг </returns>
        ICollection<BookModel> GetAllBooksBySearchModel(SearchViewModel model);

        /// <summary>
        /// Выборка книг по данным Id и подходящих условиям поиска
        /// </summary>
        /// <param name="guidList"> Список Id книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список книг </returns>
        ICollection<BookModel> GetBooksBySearchModel(IEnumerable<Guid> guidList, SearchViewModel model);

        /// <summary>
        /// Выборка нужной книги 
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нужной книги </returns>
        BookModel GetBook(Guid bookId);
    }
}
