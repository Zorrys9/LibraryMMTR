using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class ActiveHoldersRepository:BaseRepository<ActiveHoldersEntityModel>, IActiveHoldersRepository
    {

        public ActiveHoldersRepository(LibraryContext context)
                                    :base(context){}

        /// <summary>
        /// Создание нового активного держателя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель активного держателя </returns>
        public ActiveHoldersEntityModel CreateHolder(string userId, Guid bookId)
        {
            ActiveHoldersEntityModel newHolder = new ActiveHoldersEntityModel()
            {
                UserId = userId,
                BookId = bookId,
                DateOfReceipt = DateTime.Today
            };

            Insert(newHolder);
            return newHolder;
        }
        /// <summary>
        /// Удаление активного держателя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Модель активного держателя </returns>
        public ActiveHoldersEntityModel DeleteHolder(string userId, Guid bookId)
        {
            if (CheckHolder(userId, bookId))
            {
                var result = GetQuery().FirstOrDefault(holder => holder.UserId == userId && holder.BookId == bookId);
                Delete(result);
                return result;
            }
            else return null;
        }
        /// <summary>
        /// Проверка на наличие активного держателя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <param name="bookId"> Id книги </param>
        /// <returns> true - существует, false - отсутствует </returns>
        public bool CheckHolder(string userId, Guid bookId)
        {
            var result = GetQuery().FirstOrDefault(holder => holder.UserId == userId && holder.BookId == bookId);

            if (result != null)
                return true;
            else return false;
        }
        /// <summary>
        /// Получение списка Id книг пользователя
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список Id книг текущего пользователя </returns>
        public List<Guid> GetBooksByUser(string userId)
        {
            List<Guid> listBooks = new List<Guid>();

            var result = GetQuery().Where(book => book.UserId == userId).ToList();
            foreach(var holder in result)
            {
                listBooks.Add(holder.BookId);
            }
            return listBooks;
        }

    }
}
