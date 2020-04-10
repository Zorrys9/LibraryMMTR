using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Library.Data.Repository.Implementations
{
    public class BookRepository : BaseRepository<BookEntityModel>, IBookRepository
    {

        public BookRepository(LibraryContext context)
        : base(context) { }



        /// <summary>
        /// Добавление новой книги, если книга с такими данными уже существует (название, автор, язык и т.д.), то происходит обновление существующей книги
        /// </summary>
        /// <param name="model"> Модель новой книги </param>
        /// <returns> Модель книги </returns>
        public BookEntityModel CreateBook(BookEntityModel model)
        {
            if (!CheckBook(model))
            {
                model.Categories.Add(0);

                Insert(model);

                return model;

            }
            else return null;
        }

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> измененная модель книги </param>
        /// <returns> Модель книги </returns>
        public BookEntityModel UpdateBook(BookEntityModel model)
        {
            Update(model);

            return model;
        }

        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id"> Id книги</param>
        /// <returns> Модель книги </returns>
        public BookEntityModel DeleteBook(Guid id)
        {
            var model = CheckBook(id);

            Delete(model);

            return model;
        }

        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        public BookEntityModel ReturnBook(Guid id)
        {
            var result = CheckBook(id);

            result.Count++;
            Update(result);

            return result;
        }

        // <summary>
        /// Получение книги (уменьшение общего количества книг на 1)
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        public BookEntityModel ReceivingBook(Guid id)
        {
            var result = CheckBook(id);

            result.Count--;
            Update(result);

            return result;

        }
        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <returns> Список моделей всех книг </returns>
        public List<BookEntityModel> GetBookList()
        {
            return GetAll().ToList();
        }

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех книг </returns>
        public List<BookEntityModel> GetBookList(SearchViewModel model)
        {

            List<BookEntityModel> result = new List<BookEntityModel>();

            if(model.Name != null)
            {

                result = GetAll().Where(book => book.Title.Contains(model.Name) && book.Categories.Contains((int)model.Category)).ToList();

            }
            else
            {

                result = GetAll().Where(book => book.Categories.Contains((int)model.Category)).ToList();

            }

            return result;
        }

        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех нужных книг </returns>
        public List<BookEntityModel> GetBookList(List<Guid> listBookId, SearchViewModel model)
        {
            List<BookEntityModel> bookList = new List<BookEntityModel>();

            foreach (var bookId in listBookId)
            {

                var book = GetBook(bookId);

                if (book.Categories.Contains(int.Parse(model.Category.ToString())) &&
                   (book.Title.Contains(model.Name)) || (book.Author.Contains(model.Name)))
                {

                    bookList.Add(book);


                }

            }

            return bookList;
        }

        /// <summary>
        /// Возвращает список всех нужных книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <returns> Список моделей всех нужных книг </returns>
        public List<BookEntityModel> GetBookList(List<Guid> listBookId)
        {
            List<BookEntityModel> bookList = new List<BookEntityModel>();

            foreach (var bookId in listBookId)
            {

                var book = GetBook(bookId);

                bookList.Add(book);

            }

            return bookList;

        }

        /// <summary>
        /// Возвращает книгу по заданному Id
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Модель книги </returns>
        public BookEntityModel GetBook(Guid id)
        {
            var result = GetQuery().FirstOrDefault(book => book.Id == id);

            if (result != null)
            {

                return result;

            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Проверка наличия данной книги в БД
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Результат проверки </returns>
        bool CheckBook(BookEntityModel model)
        {
            var result = GetQuery().FirstOrDefault(book =>
               book.Title == model.Title &&
               book.Author == model.Author &&
               book.YearOfPublication == model.YearOfPublication &&
               book.Language == model.Language);

            if (result != null)
            {

                return true;

            }
            else
            {

                return false;

            }
        }

        /// <summary>
        /// Проверка наличия данной книги в БД
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Модель книги </returns>
        BookEntityModel CheckBook(Guid id)
        {
            var result = GetQuery().FirstOrDefault(book => book.Id == id);

            return result;
        }
    }
}
