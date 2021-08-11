using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System.Linq;
using static Google.Apis.Books.v1.Data.Volume;

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

            CreateMap<VolumeInfoData, BookViewModel>()
                .ForMember(book => book.Title, book => book.MapFrom(volume => volume.Title))
                .ForMember(book => book.Author, book => book.MapFrom(volume => string.Join(", ", volume.Authors)))
                .ForMember(book => book.YearOfPublication, book => book.MapFrom(volume => volume.PublishedDate.Substring(0, 4)))
                .ForMember(book => book.Description, book => book.MapFrom(volume => volume.Description))
                .ForMember(book => book.CountPages, book => book.MapFrom(volume => volume.PageCount))
                .ForMember(book => book.Language, book => book.MapFrom(volume => volume.Language))
                .ForMember(book => book.URL, book => book.MapFrom(volume => volume.InfoLink))
                .ReverseMap();

            CreateMap<InfoBook, BookViewModel>()
                .ForMember(book => book.Title, book => book.MapFrom(model => model.Title))
                .ForMember(book => book.Author, book => book.MapFrom(model => string.Join(", ", model.Authors.Select(x => x.Name))))
                .ForMember(book => book.YearOfPublication, book => book.MapFrom(model => int.Parse(model.Date.Substring(model.Date.Length - 4))))
                .ReverseMap();
        }
    }
}
