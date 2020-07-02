using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.EventBus
{
    public interface IMailSent
    {

        Guid EventId { get; }

        DateTime SentAtUtc { get; }

    }
}
