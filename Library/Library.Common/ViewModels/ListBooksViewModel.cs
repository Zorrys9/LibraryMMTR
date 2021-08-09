using System;
using System.Collections.Generic;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления списка книг
    /// </summary>
    public class ListBooksViewModel
    {
        /// <summary>
        /// Модель представления поиска книги
        /// </summary>
        public SearchViewModel Search { get; set; }

        /// <summary>
        /// Список моделей представления книг
        /// </summary>
        public ICollection<BookViewModel> Books { get; set; }

        /// <summary>
        /// Список Id книг, о появлении которых надо уведомить данного пользователя
        /// </summary>
        public ICollection<Guid> NotificationList { get; set; }

        /// <summary>
        /// Список Id книг, которые находятся у данного пользователя
        /// </summary>
        public ICollection<Guid> HoldersList { get; set; }

        /// <summary>
        /// Вид страницы (плитка, список, расширенная плитка)
        /// </summary>
        public int PageView { get; set; }
    }
}
