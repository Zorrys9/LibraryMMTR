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

        public string Title { get; set; }           

        public string Author { get; set; }         

        public int YearOfPublication { get; set; }  

        public string Language { get; set; }        

        public int Count { get; set; }              

        public int Aviable { get; set; }            

        public int CountPages { get; set; }         

        public List<int> Categories { get; set; } 

        public List<Guid> KeyWordsId { get; set; }       

        public string Description { get; set; }    

        public string URL { get; set; }             

        public byte[] Cover { get; set; }           




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
                Aviable = model.Aviable,
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
                Aviable = model.Aviable,
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
                Aviable = model.Aviable,
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
                Aviable = model.Aviable,
                Description = model.Description,
                URL = model.URL,
                CountPages = model.CountPages,
                CoverBytes = model.Cover

            };
        }

    }
}
