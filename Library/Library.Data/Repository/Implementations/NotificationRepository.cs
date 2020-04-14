using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class NotificationRepository : BaseRepository<NotificationEntityModel>, INotificationRepository
    {

        public NotificationRepository(LibraryContext context)
                                        : base(context) { }




        /// <summary>
        /// Добавление нового оповещения
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель нового оповещения </returns>
        public async Task<NotificationEntityModel> CreateNotification(NotificationEntityModel model)
        {

            if (!CheckNotification(model))
            {
                var result = await InsertAsync(model);

                return result;
            }
            else
            {
                throw new Exception("Запись с такими данными уже существует");
            }

        }

        /// <summary>
        /// Удаление оповещения
        /// </summary>
        /// <param name="model"> Модель оповещения</param>
        /// <returns> Модель оповещения </returns>
        public async Task<NotificationEntityModel> DeleteNotification(NotificationEntityModel model)
        {
            if (!CheckNotification(model))
            {
                var result = await DeleteAsync(model);

                return result;
            }
            else
            {
                throw new Exception("Запись с такими данными уже существует");
            }
        }

        /// <summary>
        /// Получение списка оповещений для данной книги
        /// </summary>
        /// <param name="BookId"> Id книги </param>
        /// <returns> Список моделей оповещений </returns>
        public List<NotificationEntityModel> GetListNotification(Guid bookId)
        {
            var result = GetQuery().Where(notific => notific.BookId == bookId).ToList();

            return result;
        }

        /// <summary>
        /// Получение списка оповещений текущего пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список моделеей оповещений </returns>
        public List<NotificationEntityModel> GetListNotification(string userId)
        {
            var result = GetQuery().Where(notific => notific.UserId == userId).ToList();

            return result;
        }

        /// <summary>
        /// Проверка содержится ли в БД запись с такими данными
        /// </summary>
        /// <param name="model"> Модель оповещения </param>
        /// <returns> Результат проверки </returns>
        public bool CheckNotification(NotificationEntityModel model)
        {
            var result = GetQuery().FirstOrDefault(notific => notific.UserId == model.UserId && notific.BookId == model.BookId);

            if (result != null)
            {

                return true;

            }
            else
            {

                return false;

            }
        }

    }
}
