using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class BookRepository : BaseRepository<BookEntityModel>, IBookRepository
    {

        public BookRepository(LibraryContext context)
        : base(context) { }


        public BookEntityModel CreateBook(BookEntityModel model)
        {
            if (!CheckBook(model))
            {

                model.Categories.Add(0);
                model.Aviable = model.Count;

                Insert(model);

                return model;

            }
            else
            {

                throw new Exception("Такая книга уже существует");

            }
        }

        public async Task<BookEntityModel> UpdateBook(BookEntityModel model)
        {
            model.Categories.Add(0);

            if (model.Cover == null)
            {

                model.Cover = GetQuery().AsNoTracking().FirstOrDefault(book => book.Id == model.Id).Cover;

            }

            await UpdateAsync(model);

            return model;
        }

        public BookEntityModel DeleteBook(Guid id)
        {
            var model = CheckBook(id);

            Delete(model);

            return model;
        }

        public BookEntityModel ReturnBook(Guid id)
        {
            var result = CheckBook(id);


            result.Aviable++;
            Update(result);

            return result;
        }

        public BookEntityModel ReceivingBook(Guid id)
        {
            var result = CheckBook(id);

            if (result.Aviable != 0)
            {

                result.Aviable--;
                Update(result);

                return result;

            }
            else
            {

                throw new Exception("Данной книги нет в наличии");

            }


        }

        public List<BookEntityModel> GetBookList()
        {
            return GetAll().ToList();
        }

        public List<BookEntityModel> GetBookList(SearchViewModel model)
        {

            List<BookEntityModel> result = new List<BookEntityModel>();

            result = GetAll().Where(book => book.Categories.Contains((int)model.Category)).ToList();

            if (model.Name != null && model.Name.Trim() != "")
            {

                result = result.Where(book => book.Title.ToLower().Contains(model.Name.ToLower())).ToList();

            }

            return result;
        }

        public List<BookEntityModel> GetBookList(List<Guid> listBookId, SearchViewModel model)
        {
            List<BookEntityModel> bookList = new List<BookEntityModel>();

            foreach (var bookId in listBookId)
            {

                var book = GetBook(bookId);

                if (book.Categories.Contains((int)model.Category) && (model.Name != null && model.Name.ToLower().Trim() != ""))
                {

                    if (book.Title.ToLower().Trim().Contains(model.Name.ToLower().Trim()))
                    {

                        bookList.Add(book);

                    }

                }
                else if(model.Name == null || model.Name.Trim() == "")
                {

                    bookList.Add(book);

                }
                
            }

            return bookList;
        }

            public List<BookEntityModel> GetBookList(List<Guid> listBookId)
            {
                List<BookEntityModel> bookList = new List<BookEntityModel>();

                foreach (var bookId in listBookId)
                {

                    bookList.Add(GetBook(bookId));

                }

                return bookList;

            }

            public BookEntityModel GetBook(Guid id)
            {
                var result = GetQuery().FirstOrDefault(book => book.Id == id);

                if (result != null)
                {

                    return result;

                }
                else
                {

                    throw new Exception("Книга не найдена");

                }
            }

            public bool CheckBook(BookEntityModel model)
            {
                var result = GetQuery().FirstOrDefault(book =>
                   book.Title.ToLower().Trim() == model.Title.ToLower().Trim() &&
                   book.Author.ToLower().Trim() == model.Author.ToLower().Trim() &&
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

            BookEntityModel CheckBook(Guid id)
            {
                var result = GetQuery().FirstOrDefault(book => book.Id == id);

                return result;
            }
        }
    }
