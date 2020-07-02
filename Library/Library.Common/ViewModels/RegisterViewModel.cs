using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления регистрации пользователя
    /// </summary>
    public class RegisterViewModel
    {

        public string SecondName { get; set; }

        public string FirstName { get; set; }

        public string Patronymic { get; set; }

        public string Role { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; } 

    }
}
