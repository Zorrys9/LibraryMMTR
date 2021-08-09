using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string SecondName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string Patronymic { get; set; }

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string Email { get; set; }

    }
}
