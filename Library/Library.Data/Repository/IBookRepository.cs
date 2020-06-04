using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    public interface IBookRepository:IBaseRepository<BookEntityModel>
    {
        /// <summary>
        /// Добавление новой книги, если книга с такими данными уже существует (название, автор, язык и т.д.), то происходит обновление существующей книги
        /// </summary>
        /// <param name="model"> Модель новой книги </param>
        /// <returns> Модель книги </returns>
        BookEntityModel CreateBook(BookEntityModel model);

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> измененная модель книги </param>
        /// <returns> Модель книги </returns>
        Task<BookEntityModel> UpdateBook(BookEntityModel model);

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id"> Id книги</param>
        /// <returns> Модель книги </returns>
        BookEntityModel DeleteBook(Guid id);

        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        BookEntityModel ReturnBook(Guid bookId);

        /// <summary>
        /// Получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        BookEntityModel ReceivingBook(Guid bookId);

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <returns> Список моделей всех книг </returns>
        List<BookEntityModel> GetBookList();

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех книг </returns>
        List<BookEntityModel> GetBookList(SearchViewModel model);

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех нужных книг </returns>
        List<BookEntityModel> GetBookList(List<Guid> listBookId, SearchViewModel model);

        /// <summary>
        /// Возвращает список всех нужных книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <returns> Список моделей всех нужных книг </returns>
        List<BookEntityModel> GetBookList(List<Guid> listBookId);

        /// <summary>
        /// Возвращает книгу по заданному Id
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Модель книги </returns>
        BookEntityModel GetBook(Guid id);

        /// <summary>
        /// Получение количество страниц данной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Количество страниц </returns>
        int CountBook(Guid bookId);

        /// <summary>
        /// Проверка содержится ли в базе данных книга с такими данными
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Результат проверка (true = содержится, false = не содержится) </returns>
        bool CheckBook(BookEntityModel model);
    }
}
