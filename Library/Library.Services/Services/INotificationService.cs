using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface INotificationService
    {
        /// <summary>
        /// Создание оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель оповещения </returns>
        List<Guid> GetIdBooksByUser(string userId);

        /// <summary>
        /// Создание оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель оповещения </returns>
        Task<NotificationModel> Create(NotificationModel model);

        /// <summary>
        /// Удаления оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель удаленного оповещения </returns>
        Task<NotificationModel> Delete(NotificationModel model);

        /// <summary>
        /// Возвращает список всех моделей оповещений для одной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        List<NotificationModel> GetList(Guid bookId);

        /// <summary>
        /// Возвращает список всех моделей оповещений одного пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей оповещений </returns>
        List<NotificationModel> GetList(string userId);

        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Результат проверки </returns>
        bool Check(string userId, Guid bookId);
    }
}
