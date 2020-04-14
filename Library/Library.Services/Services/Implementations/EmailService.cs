using Library.Services.Models;
using Library.Services.Properties;
using MailKit.Net.Smtp;
using MimeKit;
using System;

namespace Library.Services.Services.Implementations
{
    public class EmailService : IEmailService
    {

        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="sendModel"> Модель письма </param>
        public void SendMail(SendModel sendModel)
        {
            if (sendModel != null)
            {
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Администрация сайта", Resources.Email));
                emailMessage.To.Add(new MailboxAddress("Пользователь сайта", sendModel.MailTo));
                emailMessage.Subject = sendModel.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = sendModel.Body
                };

                using (var client = new SmtpClient())
                {
                    client.Connect(Resources.SMPT_host, int.Parse(Resources.SMPT_port), false);

                    client.Authenticate(Resources.Email, Resources.Password);
                    client.Send(emailMessage);

                    client.Disconnect(true);
                }
            }
            else
            {
                throw new Exception("Модель письма указана не верно");
            }
        }
    }
}
