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
        /// <summary>
        /// Название книги
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Категория книги
        /// </summary>
        public BookCategory Category { get; set; }
    }
}
