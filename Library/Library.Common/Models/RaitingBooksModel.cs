using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель рейтинга книги
    /// </summary>
    public class RaitingBooksModel
    {
        /// <summary>
        /// Идентификатор оценки книги
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid BookId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Поставленная оценка
        /// </summary>
        public double Score { get; set; }
   }
}
