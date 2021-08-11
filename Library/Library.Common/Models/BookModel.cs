using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель книги
    /// </summary>
    public class BookModel
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        public string Title { get; set; }           

        /// <summary>
        /// Автор книги
        /// </summary>
        public string Author { get; set; }         

        /// <summary>
        /// Год публикации книги
        /// </summary>
        public int YearOfPublication { get; set; }  

        /// <summary>
        /// Язык книги
        /// </summary>
        public string Language { get; set; }        

        /// <summary>
        /// Общее количество книг
        /// </summary>
        public int Count { get; set; }              

        /// <summary>
        /// Книг в наличии
        /// </summary>
        [JsonIgnore]
        public int Aviable { get; set; }            

        /// <summary>
        /// Количество страниц книги
        /// </summary>
        public int CountPages { get; set; }         

        /// <summary>
        /// Список идентификаторов категорий книги
        /// </summary>
        public ICollection<int> CategoriesId { get; set; } 

        /// <summary>
        /// Список идентификаторов ключевых слов книги
        /// </summary>
        public ICollection<Guid> KeyWordsId { get; set; }       

        /// <summary>
        /// Описание книги
        /// </summary>
        [JsonIgnore]
        public string Description { get; set; }    

        /// <summary>
        /// Ссылка на электронный источник
        /// </summary>
        [JsonIgnore]
        public string URL { get; set; }             

        /// <summary>
        /// Обложка книги (в байтах)
        /// </summary>
        [JsonIgnore]
        public byte[] CoverBytes { get; set; }           
    }
}
