using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Data.EntityModels
{
    /// <summary>
    /// Таблица пользователей системы
    /// </summary>
    public class UserEntityModel : IdentityUser 
    {
        /// <summary>
        /// Фамилия пользователя системы
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string SecondName { get; set; }

        /// <summary>
        /// Имя пользователя системы
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } 

        /// <summary>
        /// Отчество пользователя системы
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Patronymic { get; set; }

        /// <summary>
        /// Связь с таблицей текущих пользователей книги
        /// </summary>
        public List<ActiveHolderEntityModel> ActiveHolders { get; set; }

        /// <summary>
        /// Связь с таблицей операций над книгой пользователем
        /// </summary>
        public List<StatusLogEntityModel> StatusLogs { get; set; }
        
        /// <summary>
        /// Связь с таблицей оповещений пользователей
        /// </summary>
        public List<NotificationEntityModel> Notifications { get; set; }
        
        /// <summary>
        /// Связь с таблицей рейтинга книг
        /// </summary>
        public List<RaitingBooksEntityModel> RaitingBooks { get; set; }
    }
}
