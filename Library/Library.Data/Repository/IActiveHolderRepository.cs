using Library.Common.Models;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий по работе с таблицей активных пользователей книги
    /// </summary>
    public interface IActiveHolderRepository:IBaseRepository<ActiveHolderEntityModel>
    {
        /// <summary>
        /// Создание нового активного держателя
        /// </summary>
        /// <param name="model"> Модель держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task<ActiveHolderModel> CreateHolder(ActiveHolderModel model);

        /// <summary>
        /// Удаление активного держателя
        /// </summary>
        /// <param name="model"> Модель держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task<ActiveHolderModel> DeleteHolder(ActiveHolderModel model);

        /// <summary>
        /// Получение списка Id книг пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список Id книг текущего пользователя </returns>
        ICollection<Guid> GetBooksByUser(string userId);

        /// <summary>
        /// Возвращает всех активных держателей нужной книги 
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей всех держателей </returns>
        ICollection<ActiveHolderModel> GetActiveHolders(Guid bookId);

        /// <summary>
        /// Проверка на наличие активного держателя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> true - существует, false - отсутствует </returns>
        bool CheckHolder(string userId, Guid bookId);
    }
}
