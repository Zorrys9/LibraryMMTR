using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class BookRepository : BaseRepository<BookEntityModel>, IBookRepository
    {
        private readonly IMapper _mapper;

        public BookRepository(LibraryContext context, IMapper mapper)
        : base(context)
        {
            _mapper = mapper;
        }

        public async Task<BookModel> CreateBook(BookModel model)
        {
            var newModel = _mapper.Map<BookEntityModel>(model);

            if (CheckBookByModel(newModel))
            {
                throw new BuisnessException("Такая книга уже существует");
            }
            await InsertAsync(newModel);

            return model;
        }

        public async Task<BookModel> UpdateBook(BookModel model)
        {
            model.CategoriesId.Add(0);

            if (model.CoverBytes == null)
            {
                model.CoverBytes = GetQuery().AsNoTracking().FirstOrDefault(book => book.Id == model.Id).CoverBytes;
            }
            var newModel = _mapper.Map<BookEntityModel>(model);
            await UpdateAsync(newModel);

            return model;
        }

        public async Task<BookModel> DeleteBook(Guid id)
        {
            var model = CheckBookById(id);

            await DeleteAsync(model);

            return _mapper.Map<BookModel>(model);
        }

        public async Task<BookModel> ReturnBook(Guid id)
        {
            var result = CheckBookById(id);

            result.Aviable++;
            await UpdateAsync(result);

            return _mapper.Map<BookModel>(result);
        }

        public async Task<BookModel> ReceivingBook(Guid id)
        {
            var result = CheckBookById(id);

            if (result.Aviable == 0)
            {
                throw new BuisnessException("Данной книги нет в наличии");
            }
            result.Aviable--;
            await UpdateAsync(result);

            return _mapper.Map<BookModel>(result);
        }

        public ICollection<BookModel> GetBookList()
        {
            var listBooks = GetAll().ToList();

            return _mapper.Map<List<BookModel>>(listBooks);
        }

        public ICollection<BookModel> GetBookListBySearchModel(SearchViewModel model)
        {
            List<BookEntityModel> result = new List<BookEntityModel>();

            result = GetAll().Where(book => book.CategoriesId.Contains((int)model.Category)).ToList();
            if (model.Name != null && model.Name.Trim() != "")
            {
                result = result.Where(book => book.Title.ToLower().Contains(model.Name.ToLower())).ToList();
            }

            return _mapper.Map<List<BookModel>>(result);
        }

        public ICollection<BookModel> GetBookList(IEnumerable<Guid> listBookId, SearchViewModel model)
        {
            List<BookModel> bookList = new List<BookModel>();

            foreach (var bookId in listBookId)
            {
                var book = GetBook(bookId);

                if (book.CategoriesId.Contains((int)model.Category) && (model.Name != null && model.Name.ToLower().Trim() != ""))
                {
                    if (book.Title.ToLower().Trim().Contains(model.Name.ToLower().Trim()))
                    {
                        bookList.Add(book);
                    }
                }
                else if (model.Name == null || model.Name.Trim() == "")
                {
                    bookList.Add(book);
                }
            }
            return bookList;
        }

        public ICollection<BookModel> GetBookListById(IEnumerable<Guid> listBookId)
        {
            List<BookModel> bookList = new List<BookModel>();

            foreach (var bookId in listBookId)
            {
                bookList.Add(GetBook(bookId));
            }

            return bookList;
        }

        public BookModel GetBook(Guid id)
        {
            var result = GetQuery().FirstOrDefault(book => book.Id == id);

            if (result == null)
            {
                throw new BuisnessException("Книга не найдена");
            }

            return _mapper.Map<BookModel>(result);
        }

        bool CheckBookByModel(BookEntityModel model)
        {
            var result = GetQuery().FirstOrDefault(book =>
               book.Title.ToLower().Trim() == model.Title.ToLower().Trim() &&
               book.Author.ToLower().Trim() == model.Author.ToLower().Trim() &&
               book.YearOfPublication == model.YearOfPublication &&
               book.Language == model.Language);

            return result != null;
        }

        BookEntityModel CheckBookById(Guid id)
        {
            return GetQuery().FirstOrDefault(book => book.Id == id);
        }
    }
}
