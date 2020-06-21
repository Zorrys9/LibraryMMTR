using Library.Common.ViewModels;
using Library.Services.Models;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.IO;
using System.Text.Json;
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
                SettingsViewModel settings = new SettingsViewModel();

                using (FileStream fs = new FileStream("mailingsettings.json", FileMode.OpenOrCreate))
                {

                    settings = await JsonSerializer.DeserializeAsync<SettingsViewModel>(fs);

                }

                emailMessage.From.Add(new MailboxAddress("Администрация сайта", settings.Email));
                emailMessage.To.Add(new MailboxAddress("Пользователь сайта", sendModel.MailTo));
                emailMessage.Subject = sendModel.Subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = sendModel.Body
                };



                try
                {

                    using (var client = new SmtpClient())
                    {

                        await client.ConnectAsync(settings.SMPThost, int.Parse(settings.SMPTport), bool.Parse(settings.SSL));
                        await client.AuthenticateAsync(settings.Email, settings.Password);
                        await client.SendAsync(emailMessage);

                        await client.DisconnectAsync(true);

                    }

                }
                catch(Exception ex)
                {

                    throw new Exception("При отправке уведомлений пользователям возникла ошбика");

                }

                }
            else
            {
                throw new Exception("Модель письма указана не верно");
            }
        }
    }
}
