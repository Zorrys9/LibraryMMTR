using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface IEmailService
    {
        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="sendModel"> Модель письма </param>
        Task SendMail(SendModel send);

    }
}
