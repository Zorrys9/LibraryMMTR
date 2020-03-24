﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class BaseRepository<T>: IBaseRepository<T> where T: class, new()
    {
        private readonly LibraryContext context;
        public BaseRepository(LibraryContext context)
        {
            this.context = context;
        }
        public void Save()
        {
            context.SaveChanges();
        }
        public IQueryable<T> GetQuery()
        {
            return context.Set<T>();
        }
        public void Insert(T entity)
        {
            context.Set<T>().Add(entity);
            Save();
        }
        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
            Save();
        }
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            Save();
        }
        public IEnumerable<T> GetAll()
        {
            return GetQuery();
        }
    }
}
