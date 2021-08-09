using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;

namespace Library.MapperProfiles
{
    /// <summary>
    /// Профиль операций над книгой
    /// </summary>
    public class StatusLogProfile : Profile
    {
        public StatusLogProfile()
        {
            CreateMap<StatusLogEntityModel, StatusLogModel>().ReverseMap();
            CreateMap<StatusLogViewModel, StatusLogModel>().ReverseMap();
        }
    }
}
