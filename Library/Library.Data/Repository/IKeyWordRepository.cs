using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    /// <summary>
    /// Репозиторий по работе с таблицей ключевых слов книг
    /// </summary>
    public interface IKeyWordRepository:IBaseRepository<KeyWordEntityModel>
    {
        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="nameList"> Список названий ключевых слов </param>
        /// <returns> Список Id ключевых слов </returns>
        ICollection<Guid> ChekKeyWords(IEnumerable<string> nameList);

        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="IdList"> Список Id ключевых слов </param>
        /// <returns> Список названий ключевых слов </returns>
        ICollection<string> ChekKeyWords(IEnumerable<Guid> guidList);

        /// <summary>
        /// Возвращает список названий всех ключевых слов
        /// </summary>
        /// <returns> Список названий всех ключевых слов </returns>
        ICollection<string> GetListWords();
    }
}
