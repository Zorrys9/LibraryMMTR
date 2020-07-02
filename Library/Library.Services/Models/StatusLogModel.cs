using Library.Common.Enums;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    /// <summary>
    /// Модель действий над книгами
    /// </summary>
    public class StatusLogModel
    {

        public string UserId { get; set; }   
        
        public Guid BookId { get; set; }      
        
        public DateTime Date { get; set; }         
        
        public Operations Operation { get; set; }      



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
