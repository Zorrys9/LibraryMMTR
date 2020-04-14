using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    public interface IStatusLogRepository:IBaseRepository<StatusLogEntityModel>
    {

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="model"> Модель события </param>
        /// <returns> Модель события </returns>
        Task<StatusLogEntityModel> CreateStatusLog(StatusLogEntityModel model);

        /// <summary>
        /// Вывод всех событий связанных с нужной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей событий </returns>
        List<StatusLogEntityModel> GetListStatusLogs(Guid bookId);

        /// <summary>
        /// Вывод всех событий, которые совершил данный пользователь
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей событий </returns>
        List<StatusLogEntityModel> GetListStatusLogs(string userId);

    }
}
