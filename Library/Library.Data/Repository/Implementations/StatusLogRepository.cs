using AutoMapper;
using Library.Common.Models;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class StatusLogRepository:BaseRepository<StatusLogEntityModel>, IStatusLogRepository
    {
        private readonly IMapper _mapper;

        public StatusLogRepository(LibraryContext context, IMapper mapper)
                                : base(context)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// Создание нового события
        /// </summary>
        /// <param name="model"> Модель события </param>
        /// <returns> Модель события </returns>
        public async Task<StatusLogModel> CreateStatusLog(StatusLogModel model)
        {
            var newModel = _mapper.Map<StatusLogEntityModel>(model);

            await InsertAsync(newModel);
            
            return model;
        }

        /// <summary>
        /// Вывод всех событий связанных с нужной книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей событий </returns>
        public ICollection<StatusLogModel> GetListStatusLogsByBookId(Guid bookId)
        {
            var result = GetAll().Where(log => log.BookId == bookId).OrderByDescending(log => log.Date).ToList();

            return _mapper.Map<List<StatusLogModel>>(result);
        }

        /// <summary>
        /// Вывод всех событий, которые совершил данный пользователь
        /// </summary>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Список идентификаторов событий </returns>
        public ICollection<Guid> GetListStatusLogsByUserId(string userId)
        {
            return GetAll().Where(log => log.UserId == userId).Select(model => model.Id).ToList();
        }
    }
}
