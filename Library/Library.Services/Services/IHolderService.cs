using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления активными пользователями книг сервиса
    /// </summary>
    public interface IHolderService
    {

        /// <summary>
        /// Создание нового активного держателя
        /// </summary>
        /// <param name="model"> Модель активного держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task Create(ActiveHolderModel model);

        /// <summary>
        /// Удаление активного держателя 
        /// </summary>
        /// <param name="model"> Модель активного держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task Delete(ActiveHolderModel model);

        /// <summary>
        /// Получение списка Id всех книг, находящихся у 1 пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список Id книг </returns>
        ICollection<Guid> GetIdBooksByUser(string userId);

        /// <summary>
        /// Получение списка всех активных держателей нужной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список активных держателей </returns>
        ICollection<ActiveHolderModel> GetAllHoldersBook(Guid bookId);

        /// <summary>
        /// Проверка есть ли на руках у пользователя данная книга
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат проверки </returns>
        bool CheckHolder(string userId, Guid bookId);
    }
}
