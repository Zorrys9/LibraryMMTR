using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class StatusLogRepository:BaseRepository<StatusLogsEntityModel>, IStatusLogRepository
    {

        public StatusLogRepository(LibraryContext context)
                                :base(context){}
        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="model"> Модель события </param>
        /// <returns> Модель события </returns>
        public StatusLogsEntityModel CreateStatusLog(StatusLogsEntityModel model)
        {
            Insert(model);
            return model;
        }
        /// <summary>
        /// Вывод всех событий
        /// </summary>
        /// <returns> Список моделей событий </returns>
        public List<StatusLogsEntityModel> GetListStatusLogs()
        {
            return GetAll().ToList();
        }
        /// <summary>
        /// Вывод всех событий связанных с нужной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей событий </returns>
        public List<StatusLogsEntityModel> GetListStatusLogs(Guid bookId)
        {
            return GetAll().Where(log => log.BookId == bookId).ToList();
        }
    }
}
