using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class BookService :IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }



        /// <summary>
        /// Добавляет в БД новую книгу
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель добавленной книги </returns>
        public BookModel Create(BookModel model)
        {
            var result = _bookRepository.CreateBook(model);

            return result;
        }

        /// <summary>
        /// Изменение книги
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Модель измененной книги </returns>
        public BookModel Update(BookModel model)
        {
            var result = _bookRepository.UpdateBook(model);

            return result;
        }

        /// <summary>
        /// Удаление книги из БД
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель удаленной книги </returns> 
        public BookModel Delete(Guid bookId)
        {
            var result = _bookRepository.DeleteBook(bookId);

            return result;
        }

        /// <summary>
        /// Получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель полученной книги </returns>
        public BookModel ReceivingBook(Guid bookId)
        {
            var result = _bookRepository.ReceivingBook(bookId);

            return result;
        }

        /// <summary>
        /// Возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель возвращаемой книги </returns>
        public BookModel ReturnBook(Guid bookId)
        {
            var result = _bookRepository.ReturnBook(bookId);

            return result;
        }

        /// <summary>
        /// Выборка всех книг
        /// </summary>
        /// <returns> Список всех книг </returns>
        public List<BookModel> GetAllBooks()
        {
            var bookList = _bookRepository.GetBookList();
            List<BookModel> result = new List<BookModel>();

            foreach(var book in bookList)
            {
                result.Add(book);
            }

            return result;
        }

        /// <summary>
        /// Выборка книг по списку их Id 
        /// </summary>
        /// <param name="guidList"> Список Id книг </param>
        /// <returns>Список книг </returns>
        public List<BookModel> GetBooks(List<Guid> guidList)
        {
            var bookList = _bookRepository.GetBookList(guidList);
            List<BookModel> result = new List<BookModel>();

            foreach(var book in bookList)
            {
                result.Add(book);
            }

            return result;
        }

        /// <summary>
        /// Выборка всех книг подходящих под условия поиска
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список всех подходящих книг </returns>
        public List<BookModel> GetAllBooks(SearchViewModel model)
        {
            var bookList = _bookRepository.GetBookList(model);
            List<BookModel> result = new List<BookModel>();

            foreach (var book in bookList)
            {
                result.Add(book);
            }

            return result;
        }

        /// <summary>
        /// Выборка книг по данным Id и подходящих условиям поиска
        /// </summary>
        /// <param name="guidList"> Список Id книг </param>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Список книг </returns>
        public List<BookModel> GetBooks(List<Guid> guidList, SearchViewModel model)
        {
            var bookList = _bookRepository.GetBookList(guidList, model);
            List<BookModel> result = new List<BookModel>();

            foreach (var book in bookList)
            {
                result.Add(book);
            }

            return result;
        }

        /// <summary>
        /// Выборка нужной книги 
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нужной книги </returns>
        public BookModel GetBook(Guid bookId)
        {
            var result = _bookRepository.GetBook(bookId);

            return result;
        }

        /// <summary>
        /// Получение количество страниц данной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Количество страниц </returns>
        public int CountBooks(Guid bookId)
        {
            var result = _bookRepository.GetBook(bookId);

            return result.Count;
        }
    }
}
