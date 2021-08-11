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
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly ILogger _logger;
        public NotificationService(INotificationRepository notificationRepository, ILogger logger)
        {
            _notificationRepository = notificationRepository;
            _logger = logger;
        }

        public ICollection<Guid> GetIdBooksByUser(string userId)
        {
            if (userId == null)
            {
                throw new BuisnessException("Id пользователя не указан");
            }
            return _notificationRepository.GetListNotificationByUserId(userId);
        }

        public async Task Create(NotificationModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель оповещения не указана");
            }
            var result = await _notificationRepository.CreateNotification(model);
            
            if (result == null)
            {
                throw new BuisnessException("При создании уведомления возникла ошибкa ");
            }
            _logger.Information($"Grade changed: \n" + JsonSerializer.Serialize(result));
        }

        public async Task Delete(NotificationModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель оповещения не указана");
            }

            var result = await _notificationRepository.DeleteNotification(model);
            if(result == null)
            {
                throw new BuisnessException("При удалении записи об оповещении возникла ошибка");
            }
            _logger.Information($"Grade deleted: \n" + JsonSerializer.Serialize(result));
        }

        public ICollection<NotificationModel> GetList(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }
            return _notificationRepository.GetListNotificationByBookId(bookId);
        }

        public bool Check(string userId, Guid bookId)
        {
            if (userId == null && bookId == null)
            {
                throw new BuisnessException("Id пользователя или Id книги не указаны");
            }

            return _notificationRepository.CheckNotification(new NotificationModel
            {
                UserId = userId,
                BookId = bookId
            });
        }
    }
}
