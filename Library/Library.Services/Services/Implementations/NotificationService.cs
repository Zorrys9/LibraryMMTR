using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }




        public List<Guid> GetIdBooksByUser(string userId)
        {
            if (userId != null)
            {

                var notificationList = _notificationRepository.GetListNotification(userId);
                List<Guid> IdList = new List<Guid>();

                foreach (var notification in notificationList)
                {

                    IdList.Add(notification.BookId);

                }

                return IdList;

            }
            else
            {

                throw new Exception("Id пользователя не указан");

            }
        }

        public async Task<NotificationModel> Create(NotificationModel model)
        {
            if (model != null)
            {

                var result = await _notificationRepository.CreateNotification(model);

                return result;

            }
            else
            {

                throw new Exception("Модель оповещения не указана");

            }
        }

        public async Task<NotificationModel> Delete(NotificationModel model)
        {
            if (model != null)
            {

                var result = await _notificationRepository.DeleteNotification(model);

                return result;

            }
            else
            {

                throw new Exception("Модель оповещения не указана");

            }
        }

        public List<NotificationModel> GetList(Guid bookId)
        {
            if (bookId != null)
            {

                List<NotificationModel> result = new List<NotificationModel>();
                var notificationList = _notificationRepository.GetListNotification(bookId);

                foreach (var notification in notificationList)
                {

                    result.Add(notification);

                }

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }
        }

        public bool Check(string userId, Guid bookId)
        {
            if (userId != null && bookId != null)
            {

                NotificationModel notification = new NotificationModel()
                {

                    UserId = userId,
                    BookId = bookId

                };

                var result = _notificationRepository.CheckNotification(notification);

                return result;

            }
            else
            {

                throw new Exception("Id пользователя или Id книги не указаны");

            }
        }
    }
}
