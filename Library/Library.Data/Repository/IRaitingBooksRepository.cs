using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий управления рейтингом книг
    /// </summary>
    public interface IRaitingBooksRepository :IBaseRepository<RaitingBooksEntityModel>
    {

        /// <summary>
        /// Добавление новой оценки книги в таблицу с рейтингом книг
        /// </summary>
        /// <param name="model"> Модель новой оценки </param>
        /// <returns> Модель добавленной оценки </returns>
        RaitingBooksEntityModel Create(RaitingBooksEntityModel model);

        /// <summary>
        /// Изменение оценки книги
        /// </summary>
        /// <param name="model"> Измененная модель оценки </param>
        /// <returns> Измененная модель оценки </returns>
        new RaitingBooksEntityModel UpdateRaiting(RaitingBooksEntityModel model);

        /// <summary>
        /// Получение всех оценок книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список всех моделей оценок одной книги </returns>
        List<RaitingBooksEntityModel> GetRaiting(Guid bookId);

        /// <summary>
        /// Получение оценки, поставленной указанным пользоветелем определенной книге
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Модель оценки книги указанным пользователем </returns>
        RaitingBooksEntityModel GetRaiting(Guid bookId, string userId);
    }
}
