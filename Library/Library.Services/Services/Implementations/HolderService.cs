using Library.Data.Repository;
using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class HolderService : IHolderService
    {
        private readonly IActiveHolderRepository _holdersRepository;
        public HolderService(IActiveHolderRepository holdersRepository)
        {
            _holdersRepository = holdersRepository;
        }



        public async Task<ActiveHolderModel> Create(ActiveHolderModel model)
        {
            if (model != null)
            {

                var result = await _holdersRepository.CreateHolder(model);

                return result;

            }
            else
            {

                throw new Exception("Модель активного держателя указана не верно");

            }
        }

        public async Task<ActiveHolderModel> Delete(ActiveHolderModel model)
        {
            if (model != null)
            {

                var result = await _holdersRepository.DeleteHolder(model);

                return result;

            }
            else
            {

                throw new Exception("Модель активного держателя указана не верно");

            }
        }

        public List<Guid> GetIdBooksByUser(string userId)
        {
            if (userId != null)
            {

                var holdersList = _holdersRepository.GetBooksByUser(userId);
                List<Guid> IdList = new List<Guid>();

                foreach (var holder in holdersList)
                {

                    IdList.Add(holder.BookId);

                }

                return IdList;

            }
            else
            {

                throw new Exception("Id пользователя не указан");

            }
        }

        public List<ActiveHolderModel> GetAllHoldersBook(Guid bookId)
        {
            if (bookId != null)
            {

                var holdersList = _holdersRepository.GetActiveHolders(bookId);
                List<ActiveHolderModel> result = new List<ActiveHolderModel>();

                foreach (var holder in holdersList)
                {

                    result.Add(holder);

                }

                return result;

            }
            else
            {

                throw new Exception("Id книги не указан");

            }
        }

        public bool CheckHolder(string userId, Guid bookId)
        {
            if (userId != null)
            {

                var result = _holdersRepository.CheckHolder(userId, bookId);

                return result;

            }
            else
            {

                throw new Exception("Id пользователя или Id книги не указан");

            }
        }
    }
}
