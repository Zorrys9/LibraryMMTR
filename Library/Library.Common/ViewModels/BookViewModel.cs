using Library.Common.ValidationAttributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления книги
    /// </summary>
    public class BookViewModel
    {
        /// <summary>
        /// Идентификатор книги
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название книги
        /// </summary>
        [Required(ErrorMessage = "Не указано название книги")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина названия книги")]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        /// <summary>
        /// Автор книги
        /// </summary>
        [Required(ErrorMessage = "Не указан автор книги")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Недопустимая длина имени автора книги")]
        public string Author { get; set; }

        /// <summary>
        /// Год издания книги
        /// </summary>
        [Required(ErrorMessage = "Не указан год публикации книги")]
        [YearOfPublication(ErrorMessage = "Год публикации указан некорректно")]
        public int YearOfPublication { get; set; }

        /// <summary>
        /// Язык книги
        /// </summary>
        [Required(ErrorMessage = "Не указан язык книги")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Недопустимая длина названия языка книги")]
        public string Language { get; set; }

        /// <summary>
        /// Общее количество книг
        /// </summary>
        [Required(ErrorMessage = "Не указано количество книг")]
        public int Count { get; set; }

        /// <summary>
        /// Количество книг в наличии
        /// </summary>
        public int Aviable { get; set; }

        /// <summary>
        /// Предыдущее количество книг
        /// </summary>
        public int PrevCount { get; set; }

        /// <summary>
        /// Количество страниц в книге
        /// </summary>
        [Required(ErrorMessage = "Не указано количество страниц в книге")]
        public int CountPages { get; set; }

        /// <summary>
        /// Названия категорий книги
        /// </summary>
        public ICollection<string> Categories { get; set; }

        /// <summary>
        /// Список идентификаторов категорий книги
        /// </summary>
        public ICollection<int> IdCategories { get; set; }

        /// <summary>
        /// Список с названием ключевых слов книги
        /// </summary>
        [Required(ErrorMessage = "Ключевые слова книг не указаны")]
        public ICollection<string> KeyWordsName { get; set; }

        /// <summary>
        /// Описание книги
        /// </summary>
        [StringLength(300, MinimumLength = 0, ErrorMessage = "Недопустимая длина описания книги")]
        public string Description { get; set; }

        /// <summary>
        /// Ссылка на электронный источник с книгой
        /// </summary>
        [Url(ErrorMessage = "Указанная строка не является ссылкой")]
        public string URL { get; set; }

        /// <summary>
        /// Файл с обложкой книги
        /// </summary>
        public IFormFile Cover { get; set; }

        /// <summary>
        /// Обложка книги (в байтах)
        /// </summary>
        public byte[] CoverBytes { get; set; }

        /// <summary>
        /// Модель представления общего рейтинга книги
        /// </summary>
        public RaitingBooksViewModel Raiting { get; set; }

        /// <summary>
        /// Рейтинг книги, поставленный текущим пользователем
        /// </summary>
        public double RaitingUser { get; set; }

    }
}
