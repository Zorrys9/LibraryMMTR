using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    public class NotificationModel
    {

        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string UserId { get; set; }


        public static implicit operator NotificationModel(NotificationEntityModel model)
        {

            if(model == null)
            {

                return null;

            }
            else
            {

                return new NotificationModel()
                {

                    Id = model.Id,
                    BookId = model.BookId,
                    UserId = model.UserId

                };

            }

        }

        public static implicit operator NotificationEntityModel(NotificationModel model)
        {

            if (model == null)
            {

                return null;

            }
            else
            {

                return new NotificationEntityModel()
                {

                    Id = model.Id,
                    BookId = model.BookId,
                    UserId = model.UserId

                };

            }

        }
    }
}
