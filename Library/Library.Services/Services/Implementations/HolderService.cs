using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class HolderService:IHolderService
    {
        private readonly IActiveHolderRepository _holdersRepository;
        public HolderService(IActiveHolderRepository holdersRepository)
        {
            _holdersRepository = holdersRepository;
        }



        /// <summary>
        /// Создание нового активного держателя
        /// </summary>
        /// <param name="model"> Модель активного держателя </param>
        /// <returns> Модель активного держателя </returns>
        public async Task<ActiveHolderModel> Create(ActiveHolderModel model)
        {
            var result = await _holdersRepository.CreateHolder(model);

            return result;
        }

        /// <summary>
        /// Удаление активного держателя 
        /// </summary>
        /// <param name="model"> Модель активного держателя </param>
        /// <returns> Модель активного держателя </returns>
        public async Task<ActiveHolderModel> Delete(ActiveHolderModel model)
        {
            var result = await _holdersRepository.DeleteHolder(model);

            return result;
        }

        /// <summary>
        /// Получение списка Id всех книг, находящихся у 1 пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список Id книг </returns>
        public List<Guid> GetIdBooksByUser(string userId)
        {
            var holdersList = _holdersRepository.GetBooksByUser(userId);
            List<Guid> IdList = new List<Guid>();

            foreach(var holder in holdersList)
            {
                IdList.Add(holder.BookId);
            }

            return IdList;
        }

        /// <summary>
        /// Получение списка всех активных держателей нужной книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список активных держателей </returns>
        public List<ActiveHolderModel> GetAllHoldersBook(Guid bookId)
        {
            var holdersList = _holdersRepository.GetActiveHolders(bookId);
            List<ActiveHolderModel> result = new List<ActiveHolderModel>();

            foreach(var holder in holdersList)
            {
                result.Add(holder);
            }

            return result;
        }

        /// <summary>
        /// Проверка есть ли на руках у пользователя данная книга
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат проверки </returns>
        public bool CheckHolder(string userId, Guid bookId)
        {
            var result = _holdersRepository.CheckHolder(userId, bookId);

            return result;
        }
    }
}
