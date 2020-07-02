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
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }              // id пользователя, взявшего книгу

        [Required]
        public Guid BookId { get; set; }                // id книги

        [Required]
        public DateTime DateOfReceipt { get; set; }     // дата получения книги


        public UserEntityModel User { get; set; }
        public BookEntityModel Book { get; set; }
    }
}
