using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления рейтинга книги
    /// </summary>
    public class RaitingBooksViewModel
    {
        /// <summary>
        /// Идентификатор рейтинга книги
        /// </summary>
        [Required(ErrorMessage = "Идентификатор книги не указан")]
        public Guid BookId { get; set; }

        /// <summary>
        /// Оценка поставленная пользователем/Общая оценка книги
        /// </summary>
        [Required(ErrorMessage = "Оценка не указана")]
        [Range(0, 5, ErrorMessage ="Оценка указана не корректно")]
        public double Score { get; set; }

        /// <summary>
        /// Количество оценок книги 
        /// </summary>
        public int Count { get; set; }
    }
}
