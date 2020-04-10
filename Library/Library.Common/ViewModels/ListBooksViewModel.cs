using System;
using System.Collections.Generic;

namespace Library.Common.ViewModels
{
    public class ListBooksViewModel
    {

        public SearchViewModel Search { get; set; }
        public List<BookViewModel> Books { get; set; }
        public List<Guid> NotificationList { get; set; }
        public List<Guid> HoldersList { get; set; }
        public int PageView { get; set; }

    }
}
