using Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// Таблица с операциями пользователей над книгами
    /// </summary>
    [Table("StatusLogs")]
    public class StatusLogEntityModel
    {
        /// <summary>
        /// Идентификатор операции
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required]
        public string UserId { get; set; } 

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        [Required]
        public Guid BookId { get; set; }   

        /// <summary>
        /// Дата операции
        /// </summary>
        [Required]
        public DateTime Date { get; set; } 

        /// <summary>
        /// Вид операции (Взял/Вернул)
        /// </summary>
        [Required]
        public Operations Operation { get; set; }     

        /// <summary>
        /// Связь с таблицей пользователей
        /// </summary>
        public UserEntityModel User { get; set; }

        /// <summary>
        /// Связь с таблицей книг 
        /// </summary>
        public BookEntityModel Book { get; set; }

    }
}
