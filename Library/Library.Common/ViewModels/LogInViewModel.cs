using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель авторизации пользователя
    /// </summary>
    public class LogInViewModel
    {

         
        // Логин
        public string Login { get; set; }

        // Пароль
        public string Password { get; set; }

        // Запомнить меня?
        public bool RememberMe { get; set; }

    }
}
