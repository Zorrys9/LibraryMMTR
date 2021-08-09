using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Enums
{
    /// <summary>
    /// Категория книги
    /// </summary>
    public enum BookCategory
    {
        /// <summary>
        /// Все книги
        /// </summary>
        All = 0,

        /// <summary>
        /// Разработка
        /// </summary>
        Development = 1,

        /// <summary>
        /// Аналитика
        /// </summary>
        Analytics = 2,

        /// <summary>
        /// Тестирование
        /// </summary>
        Testing = 3,

        /// <summary>
        /// Сопровождение
        /// </summary>
        Maintenance = 4,

        /// <summary>
        /// Управление
        /// </summary>
        Management = 5
    }
}
