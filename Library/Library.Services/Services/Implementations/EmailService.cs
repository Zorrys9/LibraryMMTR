using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Exceptions;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public async Task SendMail(SendModel sendModel)
        {
            if (sendModel == null)
            {
                throw new BuisnessException("Модель отправки письма не заполнена");
            }

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

            using (var client = new SmtpClient())
            {

                await client.ConnectAsync(settings.SMTPhost, int.Parse(settings.SMTPport), bool.Parse(settings.SSL));
                await client.AuthenticateAsync(settings.Email, settings.Password);
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);

            }

        }
    }
}
