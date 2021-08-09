using Library.Data.Repository;
using Library.Exceptions;
using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;
using System.Text.Json;

namespace Library.Services.Services.Implementations
{
    public class StatusLogService : IStatusLogService
    {
        private readonly IStatusLogRepository _statusLogRepository;
        private readonly ILogger _logger;

        public StatusLogService(IStatusLogRepository statusLogRepository, ILogger logger)
        {
            _statusLogRepository = statusLogRepository;
            _logger = logger;
        }

        public async Task Create(StatusLogModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель операции с книгой была равна нулю");
            }
            var result = await _statusLogRepository.CreateStatusLog(model);

            if(result == null)
            {
                throw new Exception("При создании лога операции возникла ошибка");
            }
            _logger.Information($"New status log created: \n" + JsonSerializer.Serialize(model));
        }

        public ICollection<StatusLogModel> GetListByBookId(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }
            return _statusLogRepository.GetListStatusLogsByBookId(bookId);
        }

        public ICollection<Guid> GetListByUserId(string userId)
        {
            if (userId == null)
            {
                throw new BuisnessException("Id пользователя не указан");
            }
         
            return _statusLogRepository.GetListStatusLogsByUserId(userId);
        }
    }
}
