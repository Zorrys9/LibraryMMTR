using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;

namespace Library.MapperProfiles
{
    /// <summary>
    /// Профиль книги
    /// </summary>
    public class BookProfile : Profile
    {
        public BookProfile()
        {
            CreateMap<BookViewModel, BookModel>().ReverseMap();
            CreateMap<BookEntityModel, BookModel>().ReverseMap();
        }
    }
}
