using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Logic.Models
{
    public class BookModel
    {
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
        public List<BookCategory> Categories { get; set; }  // Категория книги
        [Required]
        public List<Guid> KeyWordsId { get; set; }       // Ключевые слова
        [Required]
        public string Description { get; set; }     // Описание
        public string URL { get; set; }             // Ссылка на электронную версию
     //   [Required]
        public byte[] Cover { get; set; }           // Обложка

        public static implicit operator BookModel(BooksEntityModel model)
        {
            if (model == null)
                return null;
            else return new BookModel
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                YearOfPublication = model.YearOfPublication,
                Language = model.Language,
                Count = model.Count,
                KeyWordsId = model.KeyWordsId,
                Description = model.Description,
                URL = model.URL,
                Cover = model.Cover,
                CountPages = model.CountPages,
                Categories = model.Categories
            };
        }

        public static implicit operator BooksEntityModel(BookModel model)
        {
            if (model == null)
                return null;
            else return new BooksEntityModel
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                YearOfPublication = model.YearOfPublication,
                Language = model.Language,
                Count = model.Count,
                KeyWordsId = model.KeyWordsId,
                Description = model.Description,
                URL = model.URL,
                Cover = model.Cover,
                CountPages = model.CountPages,
                Categories = model.Categories
            };
        }
        public static implicit operator BookModel(BookViewModel model)
        {
            if (model == null)
                return null;
            else return new BookModel
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                YearOfPublication = model.YearOfPublication,
                Language = model.Language,
                Count = model.Count,
                Description = model.Description,
                URL = model.URL,
                CountPages = model.CountPages,
                Categories = model.Categories
            };
        }

        public static implicit operator BookViewModel(BookModel model)
        {
            if (model == null)
                return null;
            else return new BookViewModel
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                YearOfPublication = model.YearOfPublication,
                Language = model.Language,
                Count = model.Count,
                Description = model.Description,
                URL = model.URL,
                CountPages = model.CountPages,
                Categories = model.Categories
            };
        }
    }
}
