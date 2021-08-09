using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления списка
    /// </summary>
    /// <typeparam name="T"> Какой-либо класс системы </typeparam>
    public class ListViewModel<T> where T:class
    {
        /// <summary>
        /// Список с указанным классом T
        /// </summary>
        public ICollection<T> List { get; set; }

        /// <summary>
        /// Создан ли список 
        /// </summary>
        public bool NextExists { get; set; }
    }
}
