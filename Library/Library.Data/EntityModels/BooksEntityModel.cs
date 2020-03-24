using Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    [Table("Books")]
    public class BooksEntityModel
    {
        [Required]
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }           // Название книги
        [Required]
        public string Author { get; set; }          // Автор книги
        [Required]
        public int YearOfPublication { get; set; }  // Год издания
        [Required]
        public string Language { get; set; }        // Язык
        [Required]
        public int Count { get; set; }              // Количество 
        [Required]
        public int CountPages { get; set; }         // Количество страниц
        [Required]
        public List<BookCategory> Categories { get; set; }  // Категории книги
        [Required]
        public List<Guid> KeyWordsId { get; set; } // Ключевые слова
        [Required]
        public string Description { get; set; }     // Описание
        public string URL { get; set; }             // Ссылка на электронную версию
     //   [Required]
        public byte[] Cover { get; set; }           // Обложка




        public List<ActiveHoldersEntityModel> ActiveHolders { get; set; }
        public List<StatusLogsEntityModel> StatusLogs { get; set; }
        public List<NotificationEntityModel> Notifications { get; set; }

    }
}
