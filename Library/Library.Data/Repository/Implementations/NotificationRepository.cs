using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class NotificationRepository:BaseRepository<NotificationEntityModel>,INotificationRepository
    {

        public NotificationRepository(LibraryContext context)
                                        :base(context){}

        /// <summary>
        /// Добавление нового оповещения
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нового оповещения </returns>
        public NotificationEntityModel CreateNotification(NotificationEntityModel model)
        {

            var result = CheckNotification(model);

            if (result == null)
            {

                Insert(model);
                return model;
            }
            else return result;
        }
        /// <summary>
        /// Удаление оповещения
        /// </summary>
        /// <param name="model"> Модель оповещения</param>
        /// <returns> Модель оповещения </returns>
        public NotificationEntityModel DeleteNotification(NotificationEntityModel model)
        {

            var result = CheckNotification(model);

            if (result == null)
            {

                Delete(model);
                return model;
            }
            else return result;
        }
        /// <summary>
        /// Получение списка оповещений для данной книги
        /// </summary>
        /// <param name="BookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        public List<NotificationEntityModel> GetListNotification(Guid BookId)
        {
            var result = GetQuery().Where(notific => notific.BookId == BookId).ToList();

            if (result != null)
            {
                return result;
            }
            else return null;
        }
        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель найденного оповещения </returns>
        public NotificationEntityModel CheckNotification(NotificationEntityModel model)
        {
            var result = GetQuery().FirstOrDefault(notific => notific.UserId == model.UserId && notific.BookId == model.BookId);

            if(result != null)
            {
                return result;
            }
            else return null;
        }

    }
}
