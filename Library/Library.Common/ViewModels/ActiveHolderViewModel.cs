using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления пользователя, читающего книгу
    /// </summary>
    public class ActiveHolderViewModel
    {
        /// <summary>
        /// Дата взятия книги
        /// </summary>
        public DateTime DateOfReceipt { get; set; }

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string User { get; set; }
    }
}
