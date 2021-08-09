using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.Repository;
using Library.Exceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
namespace Library.Services.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly ILogger _logger;

        public BookService(IBookRepository bookRepository, ILogger logger)
        {
            _bookRepository = bookRepository;
            _logger = logger;
        }

        public async Task Create(BookModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель книги не указана");
            }

            model.CategoriesId.Add(0);
            model.Aviable = model.Count;

            var result = await _bookRepository.CreateBook(model);

            if (result == null)
            {
                throw new BuisnessException("При создании книги возникла ошибка");
            }
            _logger.Information($"New book created: \n" + JsonSerializer.Serialize(model));
        }

        public async Task Update(BookModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель книги не указана");
            }

            var result = await _bookRepository.UpdateBook(model);

            if (result == null)
            {
                throw new BuisnessException("При изменении книги произошла ошибка");
            }
            _logger.Information($"Book changed: \n" + JsonSerializer.Serialize(model));

        }

        public async Task Delete(Guid bookId)
        {
            var result = await _bookRepository.DeleteBook(bookId);

            if (result == null)
            {
                throw new BuisnessException("При удалении книги возникла ошибка");
            }
            _logger.Information($"Book deleted: \n" + JsonSerializer.Serialize(result));
        }

        public async Task<BookModel> ReceivingBook(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }

            return await _bookRepository.ReceivingBook(bookId);
        }

        public async Task<BookModel> ReturnBook(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }

            return await _bookRepository.ReturnBook(bookId);
        }

        public ICollection<BookModel> GetAllBooks()
        {
            return _bookRepository.GetBookList();
        }

        public ICollection<BookModel> GetBooks(IEnumerable<Guid> guidList)
        {
            if (guidList == null)
            {
                throw new BuisnessException("Список Id книг пуст");
            }
            return _bookRepository.GetBookListById(guidList);
        }

        public ICollection<BookModel> GetAllBooksBySearchModel(SearchViewModel model)
        {
            if (model != null)
            {
                throw new BuisnessException("Модель поиска пуста");
            }
            return _bookRepository.GetBookListBySearchModel(model);
        }

        public ICollection<BookModel> GetBooksBySearchModel(IEnumerable<Guid> guidList, SearchViewModel model)
        {
            if (guidList == null || model == null)
            {
                throw new BuisnessException("Данные не заполнены");
            }
            return _bookRepository.GetBookList(guidList, model);
        }

        public BookModel GetBook(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }
            return _bookRepository.GetBook(bookId);
        }
    }
}
