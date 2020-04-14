using Library.Common.ViewModels;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface IBookService
    {

        /// <summary>
        /// Добавляет в БД новую книгу
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель добавленной книги </returns>
        BookModel Create(BookModel model);

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель измененной книги </returns>
        Task<BookModel> Update(BookModel model);

        /// <summary>
        /// Удаление книги из БД
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель удаленной книги </returns> 
        BookModel Delete(Guid bookId);

        /// <summary>
        /// Получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель полученной книги </returns>
        BookModel ReceivingBook(Guid bookId);

        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель возвращаемой книги </returns>
        BookModel ReturnBook(Guid bookId);

        /// <summary>
        /// Выборка всех книг
        /// </summary>
        /// <returns> Список всех книг </returns>
        List<BookModel> GetAllBooks();

        /// <summary>
        /// Выборка книг по списку их Id 
        /// </summary>
        /// <param name="guidList"> Список Id книг </param>
        /// <returns>Список книг </returns>
        List<BookModel> GetBooks(List<Guid> guidList);

        /// <summary>
        /// Выборка всех книг подходящих под условия поиска
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список всех подходящих книг </returns>
        List<BookModel> GetAllBooks(SearchViewModel model);

        /// <summary>
        /// Выборка книг по данным Id и подходящих условиям поиска
        /// </summary>
        /// <param name="guidList"> Список Id книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список книг </returns>
        List<BookModel> GetBooks(List<Guid> guidList, SearchViewModel model);

        /// <summary>
        /// Выборка нужной книги 
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нужной книги </returns>
        BookModel GetBook(Guid bookId);

        /// <summary>
        /// Получение количество страниц данной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Количество страниц </returns>
        int CountBook(Guid bookId);
    }
}
