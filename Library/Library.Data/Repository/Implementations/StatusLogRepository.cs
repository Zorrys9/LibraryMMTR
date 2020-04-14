using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class StatusLogRepository:BaseRepository<StatusLogEntityModel>, IStatusLogRepository
    {

        public StatusLogRepository(LibraryContext context)
                                :base(context){}



        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="model"> Модель события </param>
        /// <returns> Модель события </returns>
        public async Task<StatusLogEntityModel> CreateStatusLog(StatusLogEntityModel model)
        {
            var result = await InsertAsync(model);

            return result;
        }

        /// <summary>
        /// Вывод всех событий связанных с нужной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей событий </returns>
        public List<StatusLogEntityModel> GetListStatusLogs(Guid bookId)
        {
            return GetAll().Where(log => log.BookId == bookId).ToList();
        }

        /// <summary>
        /// Вывод всех событий, которые совершил данный пользователь
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей событий </returns>
        public List<StatusLogEntityModel> GetListStatusLogs(string userId)
        {
            return GetAll().Where(log => log.UserId == userId).ToList();
        }
    }
}
