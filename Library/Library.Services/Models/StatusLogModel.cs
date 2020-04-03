using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    public class StatusLogModel
    {

        public string UserId { get; set; }              // id пользователя, взявшего книгу
        public Guid BookId { get; set; }                // id книги
        public DateTime Date { get; set; }     // дата операции
        public Operations Operation { get; set; }       // Действие (взял, вернул)



        public static implicit operator StatusLogModel(StatusLogEntityModel model)
        {
            if (model == null)
            {

                return null;

            }
            else return new StatusLogModel
            {

                UserId = model.UserId,
                BookId = model.BookId,
                Date = model.Date,
                Operation = model.Operation

            };
        }

        public static implicit operator StatusLogEntityModel(StatusLogModel model)
        {
            if (model == null)
            {

                return null;

            }
            else return new StatusLogEntityModel
            {

                UserId = model.UserId,
                BookId = model.BookId,
                Date = model.Date,
                Operation = model.Operation

            };
        }

        public static implicit operator StatusLogViewModel(StatusLogModel model)
        {
            if (model == null)
            {

                return null;

            }
            else return new StatusLogViewModel
            {

                Date = model.Date,

            };
        }
    }
}
