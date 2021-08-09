using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{

    /// <summary>
    /// В данной таблице хранятся данные об активных держателях книги ( при сдаче книги запись удаляется )
    /// </summary>
    [Table("ActiveHolders")]
    public class ActiveHolderEntityModel
    {
        /// <summary>
        /// Идентификатор записи
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
        /// Дата взятия книги
        /// </summary>
        [Required]
        public DateTime DateOfReceipt { get; set; }     


        /// <summary>
        /// Связь с таблицей пользователей системы
        /// </summary>
        public UserEntityModel User { get; set; }

        /// <summary>
        /// Связь с таблицей книг системы
        /// </summary>
        public BookEntityModel Book { get; set; }
    }
}
