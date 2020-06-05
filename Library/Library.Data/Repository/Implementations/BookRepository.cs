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

            if (model.Name != null)
            {

                result = GetAll().Where(book => book.Title.ToLower().Contains(model.Name.ToLower()) && book.Categories.Contains((int)model.Category)).ToList();

            }
            else
            {

                result = GetAll().Where(book => book.Categories.Contains((int)model.Category)).ToList();

            }

            return result;
        }

        public List<BookEntityModel> GetBookList(List<Guid> listBookId, SearchViewModel model)
        {
            List<BookEntityModel> bookList = new List<BookEntityModel>();

            foreach (var bookId in listBookId)
            {

                var book = GetBook(bookId);

                if (model.Name != null)
                {
                    if (book.Title.Contains(model.Name) && book.Categories.Contains((int)model.Category))
                    {
                        bookList.Add(book);
                    }


                }
                else
                {

                    if (book.Categories.Contains((int)model.Category))
                    {
                        bookList.Add(book);
                    }

                }

            }

            return bookList;
        }

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

        public int CountBook(Guid bookId)
        {
            var count = GetQuery().FirstOrDefault(book => book.Id == bookId).Count;

            return count;
        }

        public bool CheckBook(BookEntityModel model)
        {
            var result = GetQuery().FirstOrDefault(book =>
               book.Title.ToLower().Replace(" ", "") == model.Title.ToLower().Replace(" ", "") &&
               book.Author.ToLower().Replace(" ", "") == model.Author.ToLower().Replace(" ", "") &&
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
