using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления рейтинга книги
    /// </summary>
    public class RaitingBooksViewModel
    {

        public Guid BookId { get; set; }

        public double Score { get; set; }

        public int Count { get; set; }
    }
}
