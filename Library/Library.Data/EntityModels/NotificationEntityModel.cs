using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// В этой таблице хранятся id пользователя, которого необходимо уведомить, если появится в наличии книга с соответствующим id ( после уведомления запись удаляется )
    /// </summary>
    [Table("Notification")]
    public class NotificationEntityModel
    {
        /// <summary>
        /// Идентификатор оповещения
        /// </summary>
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
        /// Связь с таблицей пользователей
        /// </summary>
        public UserEntityModel User { get; set; }

        /// <summary>
        /// Связь с таблицей книг
        /// </summary>
        public BookEntityModel Book { get; set; }

    }
}
