using Library.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления поиска книг
    /// </summary>
    public class SearchViewModel
    {

        public string Name { get; set; }

        public BookCategory Category { get; set; }

    }
}
