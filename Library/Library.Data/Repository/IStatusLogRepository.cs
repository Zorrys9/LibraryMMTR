using Library.Common.Models;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий для работы с таблицей с операциями пользователей над книгами
    /// </summary>
    public interface IStatusLogRepository:IBaseRepository<StatusLogEntityModel>
    {
        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="model"> Модель события </param>
        /// <returns> Модель события </returns>
        Task<StatusLogModel> CreateStatusLog(StatusLogModel model);

        /// <summary>
        /// Вывод всех событий связанных с нужной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей событий </returns>
        ICollection<StatusLogModel> GetListStatusLogsByBookId(Guid bookId);

        /// <summary>
        /// Вывод всех событий, которые совершил данный пользователь
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей событий </returns>
        ICollection<Guid> GetListStatusLogsByUserId(string userId);
    }
}
