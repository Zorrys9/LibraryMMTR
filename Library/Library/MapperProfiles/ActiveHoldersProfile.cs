using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;

namespace Library.MapperProfiles
{
    /// <summary>
    /// Профиль текущих пользователей книги
    /// </summary>
    public class ActiveHoldersProfile : Profile
    {
        public ActiveHoldersProfile()
        {
            CreateMap<ActiveHolderViewModel, ActiveHolderModel>().ReverseMap();
            CreateMap<ActiveHolderEntityModel, ActiveHolderModel>().ReverseMap();
        }
    }
}
