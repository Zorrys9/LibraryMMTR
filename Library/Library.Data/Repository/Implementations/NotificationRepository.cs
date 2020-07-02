using Library.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class NotificationRepository : BaseRepository<NotificationEntityModel>, INotificationRepository
    {

        public NotificationRepository(LibraryContext context)
                                        : base(context) { }



        public async Task<NotificationEntityModel> CreateNotification(NotificationEntityModel model)
        {

            if (!CheckNotification(model))
            {

                var result = await InsertAsync(model);

                return result;

            }
            else
            {

                throw new Exception("Запись с такими данными уже существует");

            }

        }

        public async Task<NotificationEntityModel> DeleteNotification(NotificationEntityModel model)
        {
            var result = await DeleteAsync(model);

            return result;
        }

        public List<NotificationEntityModel> GetListNotification(Guid bookId)
        {
            var result = GetQuery().AsNoTracking().Where(notific => notific.BookId == bookId).ToList();

            return result;
        }

        public List<NotificationEntityModel> GetListNotification(string userId)
        {
            var result = GetQuery().AsNoTracking().Where(notific => notific.UserId == userId).ToList();

            return result;
        }

        public bool CheckNotification(NotificationEntityModel model)
        {
            var result = GetQuery().FirstOrDefault(notific => notific.UserId == model.UserId && notific.BookId == model.BookId);

            if (result != null)
            {

                return true;

            }
            else
            {

                return false;

            }
        }

    }
}
