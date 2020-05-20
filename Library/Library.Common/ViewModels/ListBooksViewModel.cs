using System;
using System.Collections.Generic;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления списка книг
    /// </summary>
    public class ListBooksViewModel
    {
        // Модель представления поиска книги
        public SearchViewModel Search { get; set; }
        // Список моделей представления книг
        public List<BookViewModel> Books { get; set; }
        // Список Id книг, о появлении которых надо уведомить данного пользователя
        public List<Guid> NotificationList { get; set; }
        // Список Id книг, которые находятся у данного пользователя
        public List<Guid> HoldersList { get; set; }
        // Вид страницы (плитка, список, расширенная плитка)
        public int PageView { get; set; }

    }
}
