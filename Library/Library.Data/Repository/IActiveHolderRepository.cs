using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    public interface IActiveHolderRepository:IBaseRepository<ActiveHolderEntityModel>
    {
        /// <summary>
        /// Создание нового активного держателя
        /// </summary>
        /// <param name="model"> Модель держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task<ActiveHolderEntityModel> CreateHolder(ActiveHolderEntityModel model);

        /// <summary>
        /// Удаление активного держателя
        /// </summary>
        /// <param name="model"> Модель держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task<ActiveHolderEntityModel> DeleteHolder(ActiveHolderEntityModel model);

        /// <summary>
        /// Получение списка Id книг пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список Id книг текущего пользователя </returns>
        List<ActiveHolderEntityModel> GetBooksByUser(string userId);

        /// <summary>
        /// Возвращает всех активных держателей нужной книги 
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей всех держателей </returns>
        List<ActiveHolderEntityModel> GetActiveHolders(Guid bookId);

        /// <summary>
        /// Проверка на наличие активного держателя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> true - существует, false - отсутствует </returns>
        bool CheckHolder(string userId, Guid bookId);
    }
}
