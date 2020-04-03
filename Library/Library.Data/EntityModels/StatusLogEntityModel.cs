using Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    [Table("StatusLogs")]
    // Данная таблица хранит все операции с книгами всех пользователей
    public class StatusLogEntityModel
    {

        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string UserId { get; set; }              // id пользователя, взявшего книгу
        [Required]
        public Guid BookId { get; set; }                // id книги
        [Required]
        public DateTime Date { get; set; }     // дата операции
        [Required]
        public Operations Operation { get; set; }       // Действие (взял, вернул)


        public UserEntityModel User { get; set; }
        public BookEntityModel Book { get; set; }

    }
}
