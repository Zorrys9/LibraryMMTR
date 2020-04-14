using Library.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Common.ViewModels
{
    public class BookViewModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }                       // Название книги
        [Required]
        public string Author { get; set; }                      // Автор книги
        [Required]
        public int YearOfPublication { get; set; }              // Год издания
        [Required]
        public string Language { get; set; }                    // Язык
        [Required]
        public int Count { get; set; }                          // Количество 
        public int PrevCount { get; set; }                      // Количество 
        [Required]
        public int CountPages { get; set; }                     // Количество страниц
        public List<string> Categories { get; set; }            // Категория книги
        [Required]
        public List<int> IdCategories { get; set; }             // Категория книги
        [Required]
        public List<string> KeyWordsName { get; set; }          // Ключевые слова
        [Required]
        public string Description { get; set; }                 // Описание
        public string URL { get; set; }                         // Ссылка на электронную версию
        //[Required]
        public IFormFile Cover { get; set; }                    // Обложка 
        public byte[] CoverBytes { get; set; }                  // Обложка (массив байтов)

    }
}
