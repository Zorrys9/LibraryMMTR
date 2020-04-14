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




        /// <summary>
        /// Возвращает список Id всех книг, по которым необходимо оповестить пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список id книг </returns>
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

        /// <summary>
        /// Создание оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель оповещения </returns>
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

        /// <summary>
        /// Удаления оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель удаленного оповещения </returns>
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

        /// <summary>
        /// Возвращает список всех моделей оповещений для одной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
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

        /// <summary>
        /// Возвращает список всех моделей оповещений одного пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей оповещений </returns>
        public List<NotificationModel> GetList(string userId)
        {
            if (userId != null)
            {
                List<NotificationModel> result = new List<NotificationModel>();
                var notificationList = _notificationRepository.GetListNotification(userId);

                foreach (var notification in notificationList)
                {

                    result.Add(notification);

                }

                return result;
            }
            else
            {
                throw new Exception("Id пользователя не указан");
            }
        }

        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Результат проверки </returns>
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
