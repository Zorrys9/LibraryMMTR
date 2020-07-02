using Library.Common.ViewModels;
using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }



        public BookModel Create(BookModel model)
        {
            if (model != null)
            {

                var result = _bookRepository.CreateBook(model);

                return result;

            }
            else
            {

                throw new Exception("Модель книги не указана");

            }
        }

        public async Task<BookModel> Update(BookModel model)
        {
            if (model != null)
            {

                var result = await _bookRepository.UpdateBook(model);

                return result;

            }
            else
            {

                throw new Exception("Модель книги не указана");

            }

        }

        public BookModel Delete(Guid bookId)
        {
            if (bookId != null)
            {

                var result = _bookRepository.DeleteBook(bookId);

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }
        }

        public BookModel ReceivingBook(Guid bookId)
        {
            if (bookId != null)
            {

                var result = _bookRepository.ReceivingBook(bookId);

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }
        }

        public BookModel ReturnBook(Guid bookId)
        {
            if (bookId != null)
            {

                var result = _bookRepository.ReturnBook(bookId);

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }

        }

        public List<BookModel> GetAllBooks()
        {
            var bookList = _bookRepository.GetBookList();
            List<BookModel> result = new List<BookModel>();

            foreach (var book in bookList)
            {

                result.Add(book);

            }

            return result;
        }

        public List<BookModel> GetBooks(List<Guid> guidList)
        {
            if (guidList != null)
            {

                var bookList = _bookRepository.GetBookList(guidList);
                List<BookModel> result = new List<BookModel>();

                foreach (var book in bookList)
                {

                    result.Add(book);

                }

                return result;

            }
            else
            {

                throw new Exception("Список Id книг пуст");

            }
        }

        public List<BookModel> GetAllBooks(SearchViewModel model)
        {
            if (model != null)
            {

                var bookList = _bookRepository.GetBookList(model);
                List<BookModel> result = new List<BookModel>();

                foreach (var book in bookList)
                {

                    result.Add(book);

                }

                return result;

            }
            else
            {

                throw new Exception("Модель поиска пуста");

            }
        }

        public List<BookModel> GetBooks(List<Guid> guidList, SearchViewModel model)
        {
            if (guidList != null)
            {

                if (model != null)
                {

                    var bookList = _bookRepository.GetBookList(guidList, model);
                    List<BookModel> result = new List<BookModel>();

                    foreach (var book in bookList)
                    {

                        result.Add(book);

                    }

                    return result;

                }
                else
                {

                    throw new Exception("Модель поиска пуста");

                }
            }
            else
            {

                throw new Exception("Список Id книг пуст");

            }

        }

        public BookModel GetBook(Guid bookId)
        {
            if (bookId != null)
            {

                var result = _bookRepository.GetBook(bookId);

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }
        }

    }
}
