using Library.Services.Models;
using Library.Services.Properties;
using MimeKit;
using MailKit.Net.Smtp;

namespace Library.Services.Services.Implementations
{
    public class EmailService : IEmailService
    {
        public void SendMail(SendModel sendModel)
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


    }
}
