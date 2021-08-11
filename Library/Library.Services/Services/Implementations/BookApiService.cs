using AutoMapper;
using Google.Apis.Books.v1;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Exceptions;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class BookApiService : IBookApiService
    {
        private readonly BooksService _googleService;
        private readonly IMapper _mapper;

        public BookApiService(BooksService googleService, IMapper mapper)
        {
            _googleService = googleService;
            _mapper = mapper;
        }

        public async Task<BookViewModel> GetBookByISBN(string isbn)
        {
            if (string.IsNullOrEmpty(isbn))
            {
                throw new BuisnessException("ISBN не указан");
            }

            isbn = isbn.Replace("-", "");

            var result = await GetBookFromGoogle(isbn);

            if (result == null)
            {
                result = await GetBookFromOpenLibrary(isbn);
            }

            return result;
        }

        private async Task<BookViewModel> GetBookFromGoogle(string isbn)
        {
            var result = await _googleService.Volumes.List(isbn).ExecuteAsync();
            if (result == null || result.Items.Count == 0)
            {
                return null;
            }

            var searchBook = result.Items?.FirstOrDefault().VolumeInfo;
            BookViewModel book = null;

            if (searchBook != null)
            {
                book = _mapper.Map<BookViewModel>(searchBook);
            }

            return book;
        }

        private async Task<BookViewModel> GetBookFromOpenLibrary(string isbn)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"https://openlibrary.org/api/books?bibkeys=ISBN:{isbn}&jscmd=details&format=json");
            if (!response.IsSuccessStatusCode) 
            {
                return null; 
            }
            
            var jsonResult = await response.Content.ReadAsStringAsync();
            jsonResult = jsonResult.Replace($":{isbn}", "");
            var model = JsonConvert.DeserializeObject<BookFromQueryModel>(jsonResult);
            var details = model.AllInfo.Details;

            BookViewModel book = _mapper.Map<BookViewModel>(details);

            return book;
        }
    }
}
