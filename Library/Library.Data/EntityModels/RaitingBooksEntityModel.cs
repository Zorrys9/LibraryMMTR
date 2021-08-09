using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// Модель рейтинга книг
    /// </summary>
    [Table("RaitingBooks")]
    public class RaitingBooksEntityModel
    {
        /// <summary>
        /// Идентификатор оценки книги
        /// </summary>
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        [Required]
        public Guid BookId { get; set; }   

        /// <summary>
        /// Идентификатор пользователя 
        /// </summary>
        [Required]
        public string UserId { get; set; } 

        /// <summary>
        /// Оценка книги
        /// </summary>
        [Required]
        [Range(0,5)]
        public double Score { get; set; }  

        /// <summary>
        /// Связь с таблицей книг
        /// </summary>
        public BookEntityModel Book { get; set; }
        
        /// <summary>
        /// Связь с таблицей пользователей
        /// </summary>
        public UserEntityModel User { get; set; }

    }
}
