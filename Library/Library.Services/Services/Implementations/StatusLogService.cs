using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class StatusLogService : IStatusLogService
    {

        private readonly IStatusLogRepository _statusLogRepository;

        public StatusLogService(IStatusLogRepository statusLogRepository)
        {
            _statusLogRepository = statusLogRepository;
        }




        /// <summary>
        /// Создание новой операции с книгой
        /// </summary>
        /// <param name="model"> Модель операции </param>
        /// <returns> Модель операции </returns>
        public async Task<StatusLogModel> Create(StatusLogModel model)
        {
            if (model != null)
            {
                var result = await _statusLogRepository.CreateStatusLog(model);

                return result;
            }
            else
            {
                throw new Exception("Модель операции с книгой была равна нулю");
            }
        }

        /// <summary>
        /// Возвращает список всех операций с данной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список всех операций </returns>
        public List<StatusLogModel> GetList(Guid bookId)
        {
            if (bookId != null)
            {
                List<StatusLogModel> result = new List<StatusLogModel>();
                var logsList = _statusLogRepository.GetListStatusLogs(bookId);

                foreach (var log in logsList)
                {

                    result.Add(log);

                }

                return result;
            }
            else
            {
                throw new Exception("Id книги не указан");
            }

        }

        /// <summary>
        /// Вывод всех событий, которые совершил данный пользователь
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделей событий </returns>
        public List<StatusLogModel> GetList(string userId)
        {
            if (userId != null)
            {
                List<StatusLogModel> result = new List<StatusLogModel>();
                var logsList = _statusLogRepository.GetListStatusLogs(userId);

                foreach (var log in logsList)
                {

                    result.Add(log);

                }

                return result;
            }
            else
            {
                throw new Exception("Id пользователя не указан");
            }
        }
    }
}
