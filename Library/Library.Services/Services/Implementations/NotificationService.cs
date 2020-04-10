using Library.Data.EntityModels;
using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class NotificationService :INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }



        /// <summary>
        /// Возвращает список Id всех книг, по которым необходимо оповестить пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список id книг </returns>
        public List<Guid> GetIdBooksByUser(string userId)
        {
            var notificationList = _notificationRepository.GetListNotification(userId);
            List<Guid> IdList = new List<Guid>();

            foreach(var notification in notificationList)
            {

                IdList.Add(notification.BookId);

            }

            return IdList;
        }

        /// <summary>
        /// Создание оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель оповещения </returns>
        public async Task<NotificationModel> Create(NotificationModel model)
        {
            var result = await _notificationRepository.CreateNotification(model);

            return result;
        }

        /// <summary>
        /// Удаления оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель удаленного оповещения </returns>
        public async Task<NotificationModel> Delete(NotificationModel model)
        {
            var result = await _notificationRepository.DeleteNotification(model);

            return result;
        }

        /// <summary>
        /// Возвращает список всех моделей оповещений для одной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        public List<NotificationModel> GetList(Guid bookId)
        {
            List<NotificationModel> result = new List<NotificationModel>();
            var notificationList = _notificationRepository.GetListNotification(bookId);

            foreach(var notification in notificationList)
            {

                result.Add(notification);

            }

            return result;

        }

        /// <summary>
        /// Возвращает список всех моделей оповещений одного пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей оповещений </returns>
        public List<NotificationModel> GetList(string userId)
        {
            List<NotificationModel> result = new List<NotificationModel>();
            var notificationList = _notificationRepository.GetListNotification(userId);

            foreach (var notification in notificationList)
            {

                result.Add(notification);

            }

            return result;

        }

        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Результат проверки </returns>
        public bool Check(string userId, Guid bookId)
        {
            NotificationModel notification = new NotificationModel()
            {
                UserId = userId,
                BookId = bookId
            };

            var result = _notificationRepository.CheckNotification(notification);

            return result;
        }
    }
}
