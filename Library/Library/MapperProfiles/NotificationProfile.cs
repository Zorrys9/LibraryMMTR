using AutoMapper;
using Library.Common.Models;
using Library.Data.EntityModels;

namespace Library.MapperProfiles
{
    /// <summary>
    /// Профиль оповещения
    /// </summary>
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            CreateMap<NotificationEntityModel, NotificationModel>().ReverseMap();
        }
    }
}
