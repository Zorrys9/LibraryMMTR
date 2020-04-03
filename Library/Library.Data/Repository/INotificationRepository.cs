using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    public interface INotificationRepository:IBaseRepository<NotificationEntityModel>
    {
        /// <summary>
        /// Добавление нового оповещения
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нового оповещения </returns>
        Task<NotificationEntityModel> CreateNotification(NotificationEntityModel model);

        /// <summary>
        /// Получение списка оповещений для данной книги
        /// </summary>
        /// <param name="BookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        List<NotificationEntityModel> GetListNotification(Guid bookId);

        /// <summary>
        /// Получение списка оповещений текущего пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделеей оповещений </returns>
        List<NotificationEntityModel> GetListNotification(string userId);

        /// <summary>
        /// Удаление оповещения
        /// </summary>
        /// <param name="model"> Модель оповещения</param>
        /// <returns> Модель оповещения </returns>
        Task<NotificationEntityModel> DeleteNotification(NotificationEntityModel model);

    }
}
