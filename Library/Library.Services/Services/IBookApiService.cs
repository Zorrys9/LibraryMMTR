using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface IBookApiService
    {
        Task<BookViewModel> GetBookByISBN(string isbn);
    }
}
