using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository
{
    public interface IBaseRepository<T>
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll();
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();


    }
}
