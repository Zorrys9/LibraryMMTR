using Library.Common.ViewModels;
using Library.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.Services
{
    public interface ILibraryService
    {
        List<BookViewModel> GetAllBooks(SearchViewModel model);
        BookModel CreateBook(BookViewModel model);
        BookModel UpdateBook(BookViewModel model);
        BookModel DeleteBook(Guid id);
        StatusLogModel ReceivingBook(Guid bookId, string userId);
        StatusLogModel ReturnBook(Guid bookId, string userId);
        Guid? CreaterNotification(string userId, Guid bookId);
        List<BookViewModel> GetListBooksByUser(string userId, SearchViewModel model);
        BookModel GetBook(Guid id);

    }
}
