using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель письма пользователю
    /// </summary>
    public class SendModel
    {
        /// <summary>
        /// Электронная почта получателя письма
        /// </summary>
        public string MailTo { get; set; }

        /// <summary>
        /// Предмет письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма
        /// </summary>
        public string Body { get; set; }
    }
}
