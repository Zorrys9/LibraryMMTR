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

        public DateTime Date { get; set; }

        public string User { get; set;}

        public string Operation { get; set; }


    }
}
