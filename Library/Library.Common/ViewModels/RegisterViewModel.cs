using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Common.ViewModels
{
    public class RegisterViewModel
    {
        // Фамилия
        [Required]
        [MaxLength(50)]
        public string SecondName { get; set; }
        // Имя
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        // Отчество
        [Required]
        [MaxLength(50)]
        public string Patronymic { get; set; }
        // Роль
        [Required]
        public string Role { get; set; }
        // Номер телефона
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        // Email
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        // Логин
        [Required]
        public string Login { get; set; }
        // Пароль
        [Required]
        public string Password { get; set; }
        // Повтор пароля
        [Compare("Password", ErrorMessage ="Пароли не совпадают")]
        public string ConfirmPassword { get; set; } 

    }
}
