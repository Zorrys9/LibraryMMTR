using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    /// <summary>
    /// Модель рейтинга книги
    /// </summary>
    public class RaitingBooksModel
    {

        public Guid Id { get; set; }

        public Guid BookId { get; set; }

        public string UserId { get; set; }

        public double Score { get; set; }





        public static implicit operator RaitingBooksModel(RaitingBooksEntityModel model)
        {
            if(model != null)
            {

                return new RaitingBooksModel
                {

                    Id = model.Id,
                    BookId = model.BookId,
                    UserId = model.UserId,
                    Score = model.Score

                };

            }
            else
            {

                return model;

            }
        }

        public static implicit operator RaitingBooksEntityModel(RaitingBooksModel model)
        {
            if (model != null)
            {

                return new RaitingBooksEntityModel
                {

                    Id = model.Id,
                    BookId = model.BookId,
                    UserId = model.UserId,
                    Score = model.Score

                };

            }
            else
            {

                return model;

            }
        }
        
        public static implicit operator RaitingBooksViewModel(RaitingBooksModel model)
        {
            if(model != null)
            {

                return new RaitingBooksViewModel
                {

                    BookId = model.BookId,
                    Score = model.Score

                };

            }
            else
            {

                return model;

            }
        }


        public static implicit operator RaitingBooksModel(RaitingBooksViewModel model)
        {
            if (model != null)
            {

                return new RaitingBooksModel
                {

                    BookId = model.BookId,
                    Score = model.Score

                };

            }
            else
            {

                return model;

            }
        }
    }
}
