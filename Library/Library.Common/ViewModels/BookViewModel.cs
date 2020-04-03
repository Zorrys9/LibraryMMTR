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
        public Guid Id { get; set; }
        public string Title { get; set; }           // Название книги
        public string Author { get; set; }          // Автор книги
        public int YearOfPublication { get; set; }  // Год издания
        public string Language { get; set; }        // Язык
        public int Count { get; set; }              // Количество 
        public int CountPages { get; set; }         // Количество страниц
        public List<string> Categories { get; set; }  // Категория книги
        public List<int> IdCategories { get; set; }  // Категория книги
        public List<string> KeyWordsName { get; set; }       // Ключевые слова
        public string Description { get; set; }     // Описание
        public string URL { get; set; }             // Ссылка на электронную версию
        public IFormFile Cover { get; set; }    // Обложка 
        public byte[] CoverBytes { get; set; }      // Обложка (массив байтов)

    }
}
