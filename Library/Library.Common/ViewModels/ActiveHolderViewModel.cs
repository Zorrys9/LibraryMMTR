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

        public DateTime DateOfReceipt { get; set; }

        public string User { get; set; }

    }
}
