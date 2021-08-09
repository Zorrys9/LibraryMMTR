using Library.Common.ValidationAttributes;
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
        /// <summary>
        /// Фамилия нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указана фамилия нового пользователя системы")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина фамилии нового пользователя")]
        public string SecondName { get; set; }

        /// <summary>
        /// Имя нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указано имя нового пользователя системы")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени нового пользователя")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указано отчество нового пользователя системы")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина отчества нового пользователя")]
        public string Patronymic { get; set; }

        /// <summary>
        /// Роль нового пользователя системы (по умолчанию должна быть User)
        /// </summary>
        [Required(ErrorMessage = "Не указана роль нового пользователя системы")]
        [Role]
        public string Role { get; set; }
        
        /// <summary>
        /// Номер телефона нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указан номер телефона нового пользователя")]
        [Phone(ErrorMessage = "Введенный номер телефона не корректен")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Электронная почта нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указана электронная почта нового пользователя")]
        [EmailAddress(ErrorMessage = "Введенная электронная почта не корректна")]
        public string Email { get; set; }

        /// <summary>
        /// Логин нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указан логин нового пользователя")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Длина логина не соответствует установлленной")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль нового пользователя")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Пароль нового пользователя не соответствует допустимой длине")]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля нового пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Не указан пароль для подтверждения")]
        [Compare("Password", ErrorMessage = "Введенные пароли не совпадают")]
        public string ConfirmPassword { get; set; } 

    }
}
