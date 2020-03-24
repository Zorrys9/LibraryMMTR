using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public interface IActiveHoldersRepository
    {

        ActiveHoldersEntityModel CreateHolder(string userId, Guid bookId);
        ActiveHoldersEntityModel DeleteHolder(string userId, Guid bookId);
        bool CheckHolder(string userId, Guid bookId);
        List<Guid> GetBooksByUser(string userId);

    }
}
