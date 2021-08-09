using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.EventBus
{
   /// <summary>
   /// Модель письма для рассылки
   /// </summary>
    public interface IMailSend
    {
        /// <summary>
        /// Электронная почта пользователя системы, которому предназначается письмо
        /// </summary>
        string MailTo { get;}

        /// <summary>
        /// Предмет письма
        /// </summary>
        string Subject { get; }

        /// <summary>
        /// Тело письма
        /// </summary>
        string Body { get;}
    }
}
