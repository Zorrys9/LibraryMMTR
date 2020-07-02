using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Models
{
    /// <summary>
    /// Модель письма пользователю
    /// </summary>
    public class SendModel
    {

        public string MailTo { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }


    }
}
