using Library.Services.Models;
using Library.Services.Properties;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class EmailService : IEmailService
    {

        /// <summary>
        /// Отправка письма
        /// </summary>
        /// <param name="sendModel"> Модель письма </param>
        public async Task SendMail(SendModel sendModel)
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

                    await client.ConnectAsync(Resources.SMPT_host, int.Parse(Resources.SMPT_port), true);
                    await client.AuthenticateAsync(Resources.Email, Resources.Password);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);

                }
                }
            else
            {
                throw new Exception("Модель письма указана не верно");
            }
        }
    }
}
