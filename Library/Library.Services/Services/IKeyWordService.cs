using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления ключевыми словами книг сервиса
    /// </summary>
    public interface IKeyWordService
    {
        /// <summary>
        /// Проверка ключевых слов
        /// </summary>
        /// <param name="nameList"> Список названий ключевых слов </param>
        /// <returns> Список Id ключевых слов </returns>
        ICollection<Guid> CheckWordByNames(IEnumerable<string> nameList);

        /// <summary>
        /// Проверка ключевых слов книги
        /// </summary>
        /// <param name="idList"> Список Id ключевых слов </param>
        /// <returns> Список названий ключевых слов </returns>
        ICollection<string> CheckWordById(IEnumerable<Guid> idList);

        /// <summary>
        /// Возвращает список названий всех ключевых слов
        /// </summary>
        /// <returns> Список названий всех ключевых слов </returns>
        ICollection<string> GetAll();
    }
}
