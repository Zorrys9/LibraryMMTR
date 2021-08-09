using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;

namespace Library.MapperProfiles
{
    /// <summary>
    /// Профиль рейтинга книги
    /// </summary>
    public class RaitingBooksProfile : Profile
    {
        public RaitingBooksProfile()
        {
            CreateMap<RaitingBooksEntityModel, RaitingBooksModel>().ReverseMap();
            CreateMap<RaitingBooksViewModel, RaitingBooksModel>().ReverseMap();
        }
    }
}
