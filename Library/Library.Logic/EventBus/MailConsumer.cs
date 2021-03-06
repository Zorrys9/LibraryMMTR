﻿
using Library.Services.Models;
using Library.Services.Services;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace Library.Logic.EventBus
{
    public class MailConsumer : IConsumer<IMailSend>
    {
        private readonly IEmailService _emailService;
        public MailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }


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
