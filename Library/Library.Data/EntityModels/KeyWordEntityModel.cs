using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// Таблица с ключевыми словами книг
    /// </summary>
    [Table("KeyWords")]
    public class KeyWordEntityModel
    {
        /// <summary>
        /// Идентификатор ключевого слова
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }

        /// <summary>
        /// Название ключевого слова
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
