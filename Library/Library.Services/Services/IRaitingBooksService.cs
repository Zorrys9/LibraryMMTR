using Library.Common.ViewModels;
using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления рейтингом книг
    /// </summary>
    public interface IRaitingBooksService
    {
        /// <summary>
        /// Добавление новой оценки
        /// </summary>
        /// <param name="model"> Модель представления оценки </param>
        /// <param name="userId"> Id пользователя, поставившего оценку </param>
        /// <returns> Созданная модель оценки </returns>
        Task Create(RaitingBooksViewModel model, string userId);

        /// <summary>
        /// Изменение оценки 
        /// </summary>
        /// <param name="model"> Модель представления оценки </param>
        /// <param name="userId"> Id пользователя </param>
        /// <returns> Измененная модель оценки </returns>
        Task Update(RaitingBooksViewModel model, string userId);

        /// <summary>
        /// Возвращает список моделей оценок книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Список моделей оценок книги </returns>
        ICollection<RaitingBooksModel> GetRaiting(Guid bookId);
    }
}
