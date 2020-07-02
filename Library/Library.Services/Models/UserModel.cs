using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserModel
    {

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }



        public static implicit operator UserModel(UserEntityModel model)
        {

            if (model != null)
            {

                return new UserModel
                {

                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Patronymic = model.Patronymic,
                    Email = model.Email

                };

            }
            else
            {

                return null;

            }

        }
    }
}
