using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.Repository;
using Library.Exceptions;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class RaitingBooksService : IRaitingBooksService
    {
        private readonly IRaitingBooksRepository _raitingBooksRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public RaitingBooksService(IRaitingBooksRepository raitingBooksRepository, IMapper mapper, ILogger logger)
        {
            _raitingBooksRepository = raitingBooksRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Create(RaitingBooksViewModel model, string userId)
        {
            if (model == null && userId == null)
            {
                throw new BuisnessException("Данные заполнены не полностью");
            }

            RaitingBooksModel createModel = _mapper.Map<RaitingBooksModel>(model);
            createModel.UserId = userId;

            var result = await _raitingBooksRepository.Create(createModel);

            if (result == null)
            {
                throw new BuisnessException("При добавлении оценки возникла ошибка");
            }
            _logger.Information($"New grade created: \n" + JsonSerializer.Serialize(result));
        }

        public async Task Update(RaitingBooksViewModel model, string userId)
        {
            if (model == null && userId == null)
            {
                throw new BuisnessException("Данный заполенны не полностью");
            }

            RaitingBooksModel updateModel = _mapper.Map<RaitingBooksModel>(model);
            updateModel.UserId = userId;

            var result = await _raitingBooksRepository.UpdateRaiting(updateModel);

            if (result == null)
            {
                throw new BuisnessException("При изменении оценки произошла ошибка");
            }
            _logger.Information($"Grade changed: \n" + JsonSerializer.Serialize(result));
        }

        public ICollection<RaitingBooksModel> GetRaiting(Guid bookId)
        {
            if (bookId == null)
            {
                throw new BuisnessException("Id книги не указан");
            }
            return _raitingBooksRepository.GetRaitingByBookId(bookId);
        }
    }
}
