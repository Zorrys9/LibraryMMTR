using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    public class BookModel
    {

        public Guid Id { get; set; }
        public string Title { get; set; }           // Название книги
        public string Author { get; set; }          // Автор книги
        public int YearOfPublication { get; set; }  // Год издания
        public string Language { get; set; }        // Язык
        public int Count { get; set; }              // Количество 
        public int CountPages { get; set; }         // Количество страниц
        public List<int> Categories { get; set; }  // Категория книги
        public List<Guid> KeyWordsId { get; set; }       // Ключевые слова
        public string Description { get; set; }     // Описание
        public string URL { get; set; }             // Ссылка на электронную версию
        public byte[] Cover { get; set; }           // Обложка



        public static implicit operator BookModel(BookEntityModel model)
        {
            if (model == null)
            {

                return null;

            }
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

        public static implicit operator BookEntityModel(BookModel model)
        {
            if (model == null)
            {

                return null;

            }
            else return new BookEntityModel
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
            {

                return null;

            }
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
                CountPages = model.CountPages

            };
        }

        public static implicit operator BookViewModel(BookModel model)
        {
            if (model == null)
            {

                return null;

            }
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
                CoverBytes = model.Cover

            };
        }

    }
}
