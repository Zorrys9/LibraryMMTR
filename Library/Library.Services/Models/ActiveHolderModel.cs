using Library.Common.ViewModels;
using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    public class ActiveHolderModel
    {

        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string UserId { get; set; }
        public DateTime DateOfReceipt { get; set; }


        public static implicit operator ActiveHolderModel(ActiveHolderEntityModel model)
        {
            if(model == null)
            {

                return null;

            }
            else
            {

                return new ActiveHolderModel()
                {

                    Id = model.Id,
                    BookId = model.BookId,
                    UserId = model.UserId,
                    DateOfReceipt = model.DateOfReceipt

                };

            }
        }

        public static implicit operator ActiveHolderEntityModel(ActiveHolderModel model)
        {
            if (model == null)
            {

                return null;

            }
            else
            {

                return new ActiveHolderEntityModel()
                {

                    Id = model.Id,
                    BookId = model.BookId,
                    UserId = model.UserId,
                    DateOfReceipt = model.DateOfReceipt

                };

            }
        }

        public static implicit operator ActiveHolderViewModel(ActiveHolderModel model)
        {
            if (model == null)
            {

                return null;

            }
            else
            {

                return new ActiveHolderViewModel()
                {

                    DateOfReceipt = model.DateOfReceipt
                    

                };

            }
        }
    }
}
