using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public interface IBookRepository
    {

        BooksEntityModel CreateBook(BooksEntityModel model);
        BooksEntityModel UpdateBook(BooksEntityModel model);
        BooksEntityModel DeleteBook(Guid id);
        BooksEntityModel ReturnBook(Guid bookId);
        BooksEntityModel ReceivingBook(Guid bookId);
        List<BooksEntityModel> GetBookList();
        List<BooksEntityModel> GetBookList(SearchViewModel model);
        List<BooksEntityModel> GetBookList(List<Guid> listBookId, SearchViewModel model);
        List<BooksEntityModel> GetBookList(List<Guid> listBookId);
        BooksEntityModel GetBook(Guid id);


    }
}
