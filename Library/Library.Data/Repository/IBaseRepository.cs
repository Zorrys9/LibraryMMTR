using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий с основными операциями над таблицей в БД
    /// </summary>
    /// <typeparam name="T">Модель, которая обозначает таблицу в БД</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Выборка всех записей из таблицы 
        /// </summary>
        /// <returns>Запрос к БД</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Запрос на добавление новой записи в БД
        /// </summary>
        /// <param name="entity">Добавляемая в БД модель</param>
        /// <returns>Добавленная модель</returns>
        Task<T> InsertAsync(T entity);

        /// <summary>
        /// Запрос на изменение записи в БД
        /// </summary>
        /// <param name="entity">Изменяемая в БД модель</param>
        /// <returns>Измененная модель</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Запрос на удаление новой записи в БД
        /// </summary>
        /// <param name="entity">Удаляемая в БД модель</param>
        /// <returns>Удаленная модель</returns>
        Task<T> DeleteAsync(T entity);
    }
}
