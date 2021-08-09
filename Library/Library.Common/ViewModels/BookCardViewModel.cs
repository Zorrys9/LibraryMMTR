using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления карточки книги
    /// </summary>
    public class BookCardViewModel
    {
        /// <summary>
        /// Модель представления книги
        /// </summary>
        public BookViewModel Book { get; set; }

        /// <summary>
        /// Есть ли на руках у текущего пользователя эта книга
        /// </summary>
        public bool ActiveHolder { get; set; }

        /// <summary>
        /// Оставлена ли заявка на оповещение при появлении в наличии этой книги
        /// </summary>
        public bool Notification { get; set; }

        /// <summary>
        /// Поставленная текущим пользователем оценка книге
        /// </summary>
        public double ScoreRaiting { get; set; }

        /// <summary>
        /// Общий рейтниг книги
        /// </summary>
        public double AllRaiting { get; set; }

        /// <summary>
        /// Количество книг
        /// </summary>
        public int Count { get; set; }
    }
}
