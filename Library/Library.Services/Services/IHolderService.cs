using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface IHolderService
    {

        /// <summary>
        /// Создание нового активного держателя
        /// </summary>
        /// <param name="model"> Модель активного держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task<ActiveHolderModel> Create(ActiveHolderModel model);

        /// <summary>
        /// Удаление активного держателя 
        /// </summary>
        /// <param name="model"> Модель активного держателя </param>
        /// <returns> Модель активного держателя </returns>
        Task<ActiveHolderModel> Delete(ActiveHolderModel model);

        /// <summary>
        /// Получение списка Id всех книг, находящихся у 1 пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список Id книг </returns>
        List<Guid> GetIdBooksByUser(string userId);

        /// <summary>
        /// Получение списка всех активных держателей нужной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список активных держателей </returns>
        List<ActiveHolderModel> GetAllHoldersBook(Guid bookId);

        /// <summary>
        /// Проверка есть ли на руках у пользователя данная книга
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат проверки </returns>
        bool CheckHolder(string userId, Guid bookId);
    }
}
