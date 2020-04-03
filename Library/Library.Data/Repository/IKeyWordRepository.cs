using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public interface IKeyWordRepository:IBaseRepository<KeyWordEntityModel>
    {
        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="nameList"> Список названий ключевых слов </param>
        /// <returns> Список Id ключевых слов </returns>
        List<Guid> ChekKeyWords(List<string> nameList);

        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="IdList"> Список Id ключевых слов </param>
        /// <returns> Список названий ключевых слов </returns>
        List<string> ChekKeyWords(List<Guid> guidList);

    }
}
