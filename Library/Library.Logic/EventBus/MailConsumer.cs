
using Library.Common.Models;
using Library.Services.Services;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Library.Logic.EventBus
{
    /// <summary>
    /// Потребитель сообщений
    /// </summary>
    public class MailConsumer : IConsumer<IMailSend>
    {
        private readonly IEmailService _emailService;

        public MailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Метод использования полученного сообщения
        /// </summary>
        /// <param name="context">Контекст потребителя сообщений</param>
        public async Task Consume(ConsumeContext<IMailSend> context)
        {
            SendModel send = new SendModel
            {
                MailTo = context.Message.MailTo,
                Subject = context.Message.Subject,
                Body = context.Message.Body
            };

            await _emailService.SendMail(send);

            await context.RespondAsync<IMailSent>(new { EventId = Guid.NewGuid(), SentAtUtc = DateTime.UtcNow });
        }
    }
}
