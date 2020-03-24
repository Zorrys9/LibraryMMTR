using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.Data.Repository.Implementations
{
    public class KeyWordsRepository:BaseRepository<KeyWordsEntityModel>,IKeyWordsRepository
    {
        public KeyWordsRepository(LibraryContext context)
                                 :base(context){}
        /// <summary>
        /// Добавление нового ключевого слова
        /// </summary>
        /// <param name="name"> Название ключевого слова </param>
        /// <returns> Модель ключевого слова </returns>
        public KeyWordsEntityModel CreateKeyWord(string name)
        {
            var checkWord = GetQuery().FirstOrDefault(word => word.Name == name);

            if (checkWord == null)
            {
                KeyWordsEntityModel model = new KeyWordsEntityModel()
                {
                    Name = name
                };
                Insert(model);
                return model;
            }
            else return null;
        }
        /// <summary>
        /// Проверка ключевых слов книги (если ключевых слов нет в БД, то они добавляются)
        /// </summary>
        /// <param name="nameList"> Список названий ключевых слов </param>
        /// <returns> Список Id ключевых слов </returns>
        public List<Guid> ChekKeyWords(List<string> nameList)
        {
            var keyWordList = GetAll();
            List<Guid> resultList = new List<Guid>();
            foreach(string keyword in nameList)
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

    }
}
