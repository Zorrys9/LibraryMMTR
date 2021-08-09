using AutoMapper;
using Library.Common.Models;
using Library.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class RaitingBooksRepository : BaseRepository<RaitingBooksEntityModel>, IRaitingBooksRepository
    {
        private readonly IMapper _mapper;
        public RaitingBooksRepository(LibraryContext context, IMapper mapper)
            : base(context)
        {
            _mapper = mapper;
        }

        public async Task<RaitingBooksModel> Create(RaitingBooksModel model)
        {
            var rating = GetRaitingByUserId(model.BookId, model.UserId);
            var newModel = _mapper.Map<RaitingBooksEntityModel>(model);
            var result = rating == null ? await InsertAsync(newModel) : rating;

            return _mapper.Map<RaitingBooksModel>(result);
        }

        public async Task<RaitingBooksModel> UpdateRaiting(RaitingBooksModel model)
        {
            var raiting = GetRaitingByUserId(model.BookId, model.UserId);

            raiting.Score = model.Score;
            await UpdateAsync(raiting);

            return model;
        }

        public ICollection<RaitingBooksModel> GetRaitingByBookId(Guid bookId)
        {
            var result = GetQuery().AsNoTracking().Where(raiting => raiting.BookId == bookId).ToList();

            return _mapper.Map<List<RaitingBooksModel>>(result);
        }

        RaitingBooksEntityModel GetRaitingByUserId(Guid bookId, string UserId)
        {
            return GetQuery().AsNoTracking().FirstOrDefault(raiting => raiting.BookId == bookId && raiting.UserId == UserId);
        }
    }
}
