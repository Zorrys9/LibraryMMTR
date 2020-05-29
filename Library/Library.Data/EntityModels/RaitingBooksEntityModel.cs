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
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid BookId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public double Score { get; set; }


        public BookEntityModel Book { get; set; }
        public UserEntityModel User { get; set; }

    }
}
