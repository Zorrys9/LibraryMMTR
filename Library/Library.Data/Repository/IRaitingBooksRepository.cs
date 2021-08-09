using Library.Common.Models;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий управления таблицей с рейтингом книг
    /// </summary>
    public interface IRaitingBooksRepository :IBaseRepository<RaitingBooksEntityModel>
    {
        /// <summary>
        /// Добавление новой оценки книги в таблицу с рейтингом книг
        /// </summary>
        /// <param name="model"> Модель новой оценки </param>
        /// <returns> Модель добавленной оценки </returns>
        Task<RaitingBooksModel> Create(RaitingBooksModel model);

        /// <summary>
        /// Изменение оценки книги
        /// </summary>
        /// <param name="model"> Измененная модель оценки </param>
        /// <returns> Измененная модель оценки </returns>
        Task<RaitingBooksModel> UpdateRaiting(RaitingBooksModel model);

        /// <summary>
        /// Получение всех оценок книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список всех моделей оценок одной книги </returns>
        ICollection<RaitingBooksModel> GetRaitingByBookId(Guid bookId);
    }
}
