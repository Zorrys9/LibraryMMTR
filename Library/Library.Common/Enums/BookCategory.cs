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
        // Все
        All = 0,
        // Разработка
        Development = 1, 
        // Аналитика
        Analytics = 2, 
        // Тестирование
        Testing = 3, 
        // Сопровождение
        Maintenance = 4,
        // Управление
        Management = 5
    }
}
