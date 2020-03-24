using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    [Table("ActiveHolders")]
    // В данной таблице хранятся данные об активных держателях книги ( при сдаче книги запись удаляется )
    public class ActiveHoldersEntityModel
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


        public UsersEntityModel User { get; set; }
        public BooksEntityModel Book { get; set; }
    }
}
