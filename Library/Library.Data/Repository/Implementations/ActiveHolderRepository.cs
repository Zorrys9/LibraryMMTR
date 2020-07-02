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

        public ActiveHolderRepository(LibraryContext context)
                                    :base(context){}





        public async Task<ActiveHolderEntityModel> CreateHolder(ActiveHolderEntityModel model)
        {
            var result = await InsertAsync(model);

            return result;
        }

        public async Task<ActiveHolderEntityModel> DeleteHolder(ActiveHolderEntityModel model)
        {
            model = GetQuery().FirstOrDefault(holder => holder.BookId == model.BookId && holder.UserId == model.UserId);

            var result = await DeleteAsync(model);

            return result;
        }

        public List<ActiveHolderEntityModel> GetBooksByUser(string userId)
        {
            var result = GetQuery().Where(book => book.UserId == userId).ToList();

            return result;
        }

        public List<ActiveHolderEntityModel> GetActiveHolders(Guid bookId)
        {
            var result = GetQuery().Where(holder => holder.BookId == bookId).OrderByDescending(holder=>holder.DateOfReceipt).ToList();

            return result;
        }

        public bool CheckHolder(string userId, Guid bookId)
        {
            var result = GetQuery().FirstOrDefault(holder => holder.UserId == userId && holder.BookId == bookId);

            if (result != null)
            {

                return true;

            }
            else
            {

                return false;

            }
        }
    }
}
