using AutoMapper;
using Library.Common.Models;
using Library.Data.EntityModels;
using Library.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class NotificationRepository : BaseRepository<NotificationEntityModel>, INotificationRepository
    {
        private readonly IMapper _mapper;
        public NotificationRepository(LibraryContext context, IMapper mapper)
                                        : base(context)
        {
            _mapper = mapper;
        }

        public async Task<NotificationModel> CreateNotification(NotificationModel model)
        {
            var newModel = _mapper.Map<NotificationEntityModel>(model);

            if (CheckNotification(model))
            {
                throw new BuisnessException("Запись с такими данными уже существует");
            }
            await InsertAsync(newModel);

            return model;
        }

        public async Task<NotificationModel> DeleteNotification(NotificationModel model)
        {
            var newModel = _mapper.Map<NotificationEntityModel>(model);

            await DeleteAsync(newModel);

            return model;
        }

        public ICollection<NotificationModel> GetListNotificationByBookId(Guid bookId)
        {
            var result = GetQuery().AsNoTracking().Where(notific => notific.BookId == bookId).ToList();

            return _mapper.Map<List<NotificationModel>>(result);
        }

        public ICollection<Guid> GetListNotificationByUserId(string userId)
        {
            return GetQuery().AsNoTracking().Where(notific => notific.UserId == userId).Select(model => model.Id).ToList();
        }

        public bool CheckNotification(NotificationModel model)
        {
            var result = GetQuery().FirstOrDefault(notific => notific.UserId == model.UserId && notific.BookId == model.BookId);

            return result != null;
        }

    }
}
