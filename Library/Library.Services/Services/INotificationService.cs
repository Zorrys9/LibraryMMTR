using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления оповещениями сервиса
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Создание оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель оповещения </returns>
        ICollection<Guid> GetIdBooksByUser(string userId);

        /// <summary>
        /// Создание оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель оповещения </returns>
        Task Create(NotificationModel model);

        /// <summary>
        /// Удаления оповещения 
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Модель удаленного оповещения </returns>
        Task Delete(NotificationModel model);

        /// <summary>
        /// Возвращает список всех моделей оповещений для одной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        ICollection<NotificationModel> GetList(Guid bookId);

        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Результат проверки </returns>
        bool Check(string userId, Guid bookId);
    }
}
