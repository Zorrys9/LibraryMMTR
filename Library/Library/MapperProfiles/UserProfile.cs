using AutoMapper;
using Library.Common.Models;
using Library.Data.EntityModels;

namespace Library.MapperProfiles
{
   /// <summary>
   ///  Профиль моедли пользователя
   /// </summary>
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserEntityModel, UserModel>().ReverseMap();
        }
    }

}
