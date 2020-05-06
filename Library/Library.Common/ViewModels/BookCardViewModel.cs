using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    public class BookCardViewModel
    {

        public BookViewModel Book { get; set; }
        public bool ActiveHolder { get; set; }
        public bool Notification { get; set; }

    }
}
