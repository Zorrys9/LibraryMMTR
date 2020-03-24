using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class BookRepository:BaseRepository<BooksEntityModel>, IBookRepository
    {

        public BookRepository(LibraryContext context)
        :base(context){}
        /// <summary>
        /// Добавление новой книги, если книга с такими данными уже существует (название, автор, язык и т.д.), то происходит обновление существующей книги
        /// </summary>
        /// <param name="model"> Модель новой книги </param>
        /// <returns> Модель книги </returns>
        public BooksEntityModel CreateBook(BooksEntityModel model)
        {
            var check = CheckBook(model);

            if (check == null)
            {
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
        public BooksEntityModel UpdateBook(BooksEntityModel model)
        {
            Update(model);
            return model;
        }
        /// <summary>
        /// Удаление книги
        /// </summary>
        /// <param name="id"> Id книги</param>
        /// <returns> Модель книги </returns>
        public BooksEntityModel DeleteBook(Guid id)
        {
            var model = CheckBook(id);
            Delete(model);
            return model;
        }
        /// <summary>
        /// Получение книги (уменьшение общего количества книг на 1)
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        public BooksEntityModel ReceivingBook(Guid id)
        {
            var result = CheckBook(id);

            if (result != null)
            {
                result.Count--;
                Update(result);
                return result;
            }
            else return null;
        }
        /// <summary>
        /// Возврат книги (увеличение общего количества книг на 2)
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель книги </returns>
        public BooksEntityModel ReturnBook(Guid id)
        {
            var result = CheckBook(id);

            if (result != null)
            {
                result.Count++;
                Update(result);
                return result;
            }
            else return null;
        }
        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <returns> Список моделей всех книг </returns>
        public List<BooksEntityModel> GetBookList()
        {
            return GetAll().ToList();
        }
        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех книг </returns>
        public List<BooksEntityModel> GetBookList(SearchViewModel model)
        {
            var result = GetAll();

                if (model.Category != BookCategory.All)
                {
                    result.Where(book => book.Categories.Contains(model.Category));
                }
                if (model.Name != null)
                {
                    result.Where(book => book.Title.Contains(model.Name) || book.Author.Contains(model.Name));
                }
                return result.ToList();
        }
        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список моделей всех нужных книг </returns>
        public List<BooksEntityModel> GetBookList(List<Guid> listBookId, SearchViewModel model)
        {
            List<BooksEntityModel> bookList = new List<BooksEntityModel>();

            foreach(var bookId in listBookId)
            {
                var book = GetBook(bookId);
                    if (book.Categories.Contains(model.Category) &&
                        (book.Title.Contains(model.Name)) || (book.Author.Contains(model.Name)))
                        bookList.Add(book);
            }
            return bookList;

        }
        /// <summary>
        /// Возвращает список всех книг
        /// </summary>
        /// <param name="listBookId"> Список Id всех нужных книг </param>
        /// <returns> Список моделей всех нужных книг </returns>
        public List<BooksEntityModel> GetBookList(List<Guid> listBookId)
        {
            List<BooksEntityModel> bookList = new List<BooksEntityModel>();

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
        public BooksEntityModel GetBook(Guid id)
        {
            var result = GetQuery().FirstOrDefault(book => book.Id == id);
            if (result != null)
                return result;
            else return null;
        }
        /// <summary>
        /// Проверка ключевых слов двух моделей, если во второй модели есть новые ключевые слова, то они добавляются в текущую
        /// </summary>
        /// <param name="NewModel"> модель новой книги </param>
        /// <param name="CurrentModel"> модель книги из БД </param>
        /// <returns> Модель книги </returns>
        //BooksEntityModel CheckKeyWords(BooksEntityModel NewModel, BooksEntityModel CurrentModel)
        //{

        //    var newKeyWords = NewModel.KeyWordsId;
        //    var currentKeyWords = CurrentModel.KeyWordsId;
        //    foreach(var keyword in newKeyWords)
        //    {
        //        if (!currentKeyWords.Contains(keyword))
        //            CurrentModel.KeyWordsId.Add(keyword);
        //    }
        //    return CurrentModel;
        //}
        /// <summary>
        /// Проверка наличия данной книги в БД
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель книги </returns>
        BooksEntityModel CheckBook(BooksEntityModel model)
        {
            return GetQuery().FirstOrDefault(book =>
                book.Title == model.Title &&
                book.Author == model.Author &&
                book.YearOfPublication == model.YearOfPublication &&
                book.Language == model.Language);
        }
        /// <summary>
        /// Проверка наличия данной книги в БД
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Модель книги </returns>
        BooksEntityModel CheckBook(Guid id)
        {
            return GetQuery().FirstOrDefault(book=> book.Id == id);
        }
    }
}
