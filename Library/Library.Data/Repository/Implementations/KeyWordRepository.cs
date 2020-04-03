using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class KeyWordRepository:BaseRepository<KeyWordEntityModel>,IKeyWordRepository
    {

        public KeyWordRepository(LibraryContext context)
                                 :base(context){}



        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="nameList"> Список названий ключевых слов </param>
        /// <returns> Список Id ключевых слов </returns>
        public List<Guid> ChekKeyWords(List<string> nameList)
        {
            var keyWordList = GetAll();
            List<Guid> resultList = new List<Guid>();

            foreach (string keyword in nameList)
            {
                var WordModel = keyWordList.FirstOrDefault(word => word.Name == keyword);

                if (WordModel == null)
                {

                    CreateKeyWord(keyword);
                    resultList.Add(GetAll().FirstOrDefault(word => word.Name == keyword).Id);

                }
                else
                {

                    resultList.Add(WordModel.Id);

                }
            }

            return resultList;
        }

        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="idList"> Список Id ключевых слов </param>
        /// <returns> Список названий ключевых слов </returns>
        public List<string> ChekKeyWords(List<Guid> idList)
        {
            var keyWordList = GetAll();
            List<string> resultList = new List<string>();

            foreach (Guid keyword in idList)
            {
                var WordModel = keyWordList.FirstOrDefault(word => word.Id == keyword);

                resultList.Add(WordModel.Name);
            }

            return resultList;
        }

        /// <summary>
        /// Добавление нового ключевого слова
        /// </summary>
        /// <param name="name"> Название ключевого слова </param>
        /// <returns> Модель ключевого слова </returns>
        KeyWordEntityModel CreateKeyWord(string name)
        {
            KeyWordEntityModel model = new KeyWordEntityModel() { Name = name };

            Insert(model);

            return model;
        }

    }
}
