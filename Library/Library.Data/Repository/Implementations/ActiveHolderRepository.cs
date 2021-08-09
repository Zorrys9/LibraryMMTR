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
    public class ActiveHolderRepository:BaseRepository<ActiveHolderEntityModel>, IActiveHolderRepository
    {
        private readonly IMapper _mapper;

        public ActiveHolderRepository(LibraryContext context, IMapper mapper)
                                    : base(context)
        {
            _mapper = mapper;
        }

        public async Task<ActiveHolderModel> CreateHolder(ActiveHolderModel model)
        {
            var newModel = _mapper.Map<ActiveHolderEntityModel>(model);
            await InsertAsync(newModel);

            return model;
        }

        public async Task<ActiveHolderModel> DeleteHolder(ActiveHolderModel model)
        {
            var newModel = _mapper.Map<ActiveHolderEntityModel>(model);

            newModel = GetQuery().FirstOrDefault(holder => holder.BookId == model.BookId && holder.UserId == model.UserId);
            await DeleteAsync(newModel);

            return model;
        }

        public ICollection<Guid> GetBooksByUser(string userId)
        {
            return GetQuery().Where(book => book.UserId == userId).Select(b => b.BookId).ToList();
        }

        public ICollection<ActiveHolderModel> GetActiveHolders(Guid bookId)
        {
            var result = GetQuery().Where(holder => holder.BookId == bookId).OrderByDescending(holder => holder.DateOfReceipt).ToList();

            return _mapper.Map<List<ActiveHolderModel>>(result);
        }

        public bool CheckHolder(string userId, Guid bookId)
        {
            var result = GetQuery().FirstOrDefault(holder => holder.UserId == userId && holder.BookId == bookId);

            return result != null; 
        }
    }
}
