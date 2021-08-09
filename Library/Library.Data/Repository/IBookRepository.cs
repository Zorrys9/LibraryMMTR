using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий по работе с таблицей книг
    /// </summary>
    public interface IBookRepository:IBaseRepository<BookEntityModel>
    {
        /// <summary>
        /// Добавление новой книги, если книга с такими данными уже существует (название, автор, язык и т.д.), то происходит обновление существующей книги
        /// </summary>
        /// <param name="model"> Модель новой книги </param>
        /// <returns> Модель книги </returns>
        Task<BookModel> CreateBook(BookModel model);

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> измененная модель книги </param>
        /// <returns> Модель книги </returns>
        Task<BookModel> UpdateBook(BookModel model);

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id"> Id книги</param>
        /// <returns> Модель книги </returns>
        Task<BookModel> DeleteBook(Guid id);

        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        Task<BookModel> ReturnBook(Guid bookId);

        /// <summary>
        /// Получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        Task<BookModel> ReceivingBook(Guid bookId);

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <returns> Список моделей всех книг </returns>
        ICollection<BookModel> GetBookList();

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех книг </returns>
        ICollection<BookModel> GetBookListBySearchModel(SearchViewModel model);

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех нужных книг </returns>
        ICollection<BookModel> GetBookList(IEnumerable<Guid> listBookId, SearchViewModel model);

        /// <summary>
        /// Возвращает список всех нужных книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <returns> Список моделей всех нужных книг </returns>
        ICollection<BookModel> GetBookListById(IEnumerable<Guid> listBookId);

        /// <summary>
        /// Возвращает книгу по заданному Id
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Модель книги </returns>
        BookModel GetBook(Guid id);
    }
}
