using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель авторизации пользователя
    /// </summary>
    public class LogInViewModel
    {
        /// <summary>
        /// Логин пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Логин пользователя не указан")]
        [StringLength(20,MinimumLength = 3, ErrorMessage = "Длина логина не соответствует установлленной")]
        public string Login { get; set; }

        /// <summary>
        /// Пароль пользователя системы
        /// </summary>
        [Required(ErrorMessage = "Пароль пользователя не указан")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Длина пароля не соответствует установленной")]
        public string Password { get; set; }

        /// <summary>
        /// Флаг, устанавливающий запоминать ли пользователя при авторизации в системе
        /// </summary>
        [Required(ErrorMessage = "Флаг не установлен")]
        public bool RememberMe { get; set; }

    }
}
