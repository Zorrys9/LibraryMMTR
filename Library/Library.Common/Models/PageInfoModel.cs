using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель информации о странице
    /// </summary>
    public class PageInfoModel
    {
        /// <summary>
        /// Количество книг, показывающихся на странице
        /// </summary>
        public int PageItems { get; set; }

        /// <summary>
        /// Текущая страница
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Используемый метод
        /// </summary>
        public string ActionName { get; set; }

    }
}
