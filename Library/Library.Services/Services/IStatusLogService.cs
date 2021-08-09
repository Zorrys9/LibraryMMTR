using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления операциями сервиса
    /// </summary>
    public interface IStatusLogService
    {
        /// <summary>
        /// Создание новой операции с книгой
        /// </summary>
        /// <param name="model"> Модель операции </param>
        /// <returns> Модель операции </returns>
        Task Create(StatusLogModel model);

        /// <summary>
        /// Возвращает список всех операций с данной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список всех операций </returns>
        ICollection<StatusLogModel> GetListByBookId(Guid bookId);

        /// <summary>
        /// Вывод всех событий, которые совершил данный пользователь
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список идентификаторов событий </returns>
        ICollection<Guid> GetListByUserId(string userId);
    }
}
