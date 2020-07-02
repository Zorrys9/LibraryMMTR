using Library.Data.EntityModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class RaitingBooksRepository:BaseRepository<RaitingBooksEntityModel>, IRaitingBooksRepository
    {

        public RaitingBooksRepository(LibraryContext context)
            :base(context)  {   }




        public RaitingBooksEntityModel Create(RaitingBooksEntityModel model)
        {
            if (GetRaiting(model.BookId, model.UserId) == null)
            {

                Insert(model);

                return model;

            }
            else
            {

                throw new Exception("Этот пользователь уже ставил оценку данной книге");

            }
        }

        public RaitingBooksEntityModel UpdateRaiting(RaitingBooksEntityModel model)
        {
            var raiting = GetRaiting(model.BookId, model.UserId);

            raiting.Score = model.Score;

            Update(raiting);

            return raiting;
        }


        public List<RaitingBooksEntityModel> GetRaiting(Guid bookId)
        {

            return GetQuery().AsNoTracking().Where(raiting => raiting.BookId == bookId).ToList();

        }

        public RaitingBooksEntityModel GetRaiting(Guid bookId, string UserId)
        {

            return GetQuery().AsNoTracking().FirstOrDefault(raiting => raiting.BookId == bookId && raiting.UserId == UserId);

        }

    }
}
