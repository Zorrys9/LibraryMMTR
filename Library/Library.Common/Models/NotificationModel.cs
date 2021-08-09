using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель оповещения
    /// </summary>
    public class NotificationModel
    {

        /// <summary>
        /// Идентификатор оповещения
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid BookId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserId { get; set; }
    }
}
