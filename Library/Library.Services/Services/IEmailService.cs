using Library.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления рассылкой почты
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="send"> Модель письма </param>
        Task SendMail(SendModel send);
    }
}
