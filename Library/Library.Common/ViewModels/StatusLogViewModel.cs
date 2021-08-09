using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления операций над книгами
    /// </summary>
    public class StatusLogViewModel
    {
        /// <summary>
        /// Дата операции над книгой
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// ФИО пользователя
        /// </summary>
        public string User { get; set;}

        /// <summary>
        /// Название операции
        /// </summary>
        public string Operation { get; set; }
    }
}
