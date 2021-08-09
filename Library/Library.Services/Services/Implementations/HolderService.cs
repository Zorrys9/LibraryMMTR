using Library.Data.Repository;
using Library.Exceptions;
using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json;
using Serilog;

namespace Library.Services.Services.Implementations
{
    public class HolderService : IHolderService
    {
        private readonly IActiveHolderRepository _holdersRepository;
        private readonly ILogger _logger;
        public HolderService(IActiveHolderRepository holdersRepository, ILogger logger)
        {
            _holdersRepository = holdersRepository;
            _logger = logger;
        }
        public async Task Create(ActiveHolderModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель активного держателя указана не верно");
            }

            var result = await _holdersRepository.CreateHolder(model);

            if(result == null)
            {
                throw new BuisnessException("При создании нового пользователя книги возникла ошибка");
            }
            _logger.Information($"New active holder created: \n" + JsonSerializer.Serialize(model));
        }

        public async Task Delete(ActiveHolderModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Модель активного держателя указана не верно");
            }

            var result = await _holdersRepository.DeleteHolder(model);

            if (result == null)
            {
                throw new BuisnessException("При удалении пользователя книги возникла ошибка");
            }
            _logger.Information($"Active holder deleted: \n" + JsonSerializer.Serialize(model));
        }

        public ICollection<Guid> GetIdBooksByUser(string userId)
        {
            if (userId == null)
            {
                throw new BuisnessException("Id пользователя не указан");
            }
            return _holdersRepository.GetBooksByUser(userId);
        }

        public ICollection<ActiveHolderModel> GetAllHoldersBook(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }
            return _holdersRepository.GetActiveHolders(bookId);
        }

        public bool CheckHolder(string userId, Guid bookId)
        {
            if (userId == null)
            {
                throw new BuisnessException("Id пользователя или Id книги не указан");
            }
            return _holdersRepository.CheckHolder(userId, bookId);
        }
    }
}
