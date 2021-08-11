using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель текущего пользователя книги
    /// </summary>
    public class ActiveHolderModel
    {
        /// <summary>
        /// Идентификатор записи
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

        /// <summary>
        /// Дата взятия
        /// </summary>
        public DateTime DateOfReceipt { get; set; }
    }
}
