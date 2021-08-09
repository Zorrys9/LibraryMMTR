using Library.Common.Models;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий по работе с таблицей оповещений пользователей
    /// </summary>
    public interface INotificationRepository:IBaseRepository<NotificationEntityModel>
    {
        /// <summary>
        /// Добавление нового оповещения
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нового оповещения </returns>
        Task<NotificationModel> CreateNotification(NotificationModel model);

        /// <summary>
        /// Получение списка оповещений для данной книги
        /// </summary>
        /// <param name="BookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        ICollection<NotificationModel> GetListNotificationByBookId(Guid bookId);

        /// <summary>
        /// Получение списка оповещений текущего пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список идентификаторов оповещений </returns>
        ICollection<Guid> GetListNotificationByUserId(string userId);

        /// <summary>
        /// Удаление оповещения
        /// </summary>
        /// <param name="model"> Модель оповещения</param>
        /// <returns> Модель оповещения </returns>
        Task<NotificationModel> DeleteNotification(NotificationModel model);

        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Результат проверки </returns>
        bool CheckNotification(NotificationModel model);
    }
}
