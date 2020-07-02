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

        public List<T> List { get; set; }


        public bool NextExists { get; set; }
    }
}
