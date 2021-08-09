using Library.Common.Enums;
using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// Таблица  с книгами системы
    /// </summary>
    [Table("Books")]
    public class BookEntityModel
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        [Required]
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }          

        /// <summary>
        /// Автор книги
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Author { get; set; }         

        /// <summary>
        /// Год публикации книги
        /// </summary>
        [Required]
        public int YearOfPublication { get; set; } 

        /// <summary>
        /// Язык книги
        /// </summary>
        [Required]
        public string Language { get; set; }       

        /// <summary>
        /// Количество книг
        /// </summary>
        [Required]
        public int Count { get; set; }             

        /// <summary>
        /// В наличии
        /// </summary>
        [Required]
        public int Aviable { get; set; }           

        /// <summary>
        /// Количество страниц в книге
        /// </summary>
        [Required]
        public int CountPages { get; set; }        

        /// <summary>
        /// Список идентификаторов категорий
        /// </summary>
        [Required]
        public List<int> CategoriesId { get; set; }

        /// <summary>
        /// Список идентификаторов ключевых слов
        /// </summary>
        [Required]
        public List<Guid> KeyWordsId { get; set; } 

        /// <summary>
        /// Описание книги
        /// </summary>
        [Required]
        public string Description { get; set; }    

        /// <summary>
        /// Ссылка на электронный источник
        /// </summary>
        public string URL { get; set; }            

        /// <summary>
        /// Обложка книги (в байтах)
        /// </summary>
        [Required]
        public byte[] CoverBytes { get; set; }     

        /// <summary>
        /// Связь с таблицей текущий пользователей книги
        /// </summary>
        public List<ActiveHolderEntityModel> ActiveHolders { get; set; }

        /// <summary>
        /// Связь с таблицей операций над книгами
        /// </summary>
        public List<StatusLogEntityModel> StatusLogs { get; set; }
        
        /// <summary>
        /// Связь с таблицей оповещений
        /// </summary>
        public List<NotificationEntityModel> Notifications { get; set; }

        /// <summary>
        /// Связь с таблицей рейтинга книг
        /// </summary>
        public List<RaitingBooksEntityModel> RaitingBooks { get; set; }
    }
}
