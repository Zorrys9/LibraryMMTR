using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.EventBus
{
    /// <summary>
    /// Модель события отправки рассылки
    /// </summary>
    public interface IMailSent
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        Guid EventId { get; }
        /// <summary>
        /// Дата события
        /// </summary>
        DateTime SentAtUtc { get; }
    }
}
