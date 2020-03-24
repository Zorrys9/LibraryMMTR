using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Data.EntityModels
{
    public class UsersEntityModel : IdentityUser 
    {
        [Required]
        public string SecondName { get; set; }  // Фамилия
        [Required]
        public string FirstName { get; set; }   // Имя
        [Required]
        public string Patronymic { get; set; }  // Отчество


        public List<ActiveHoldersEntityModel> ActiveHolders { get; set; }
        public List<StatusLogsEntityModel> StatusLogs { get; set; }
        public List<NotificationEntityModel> Notifications { get; set; }


    }
}
