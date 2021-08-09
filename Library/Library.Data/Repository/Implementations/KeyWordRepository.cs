using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Repository.Implementations
{
    public class KeyWordRepository:BaseRepository<KeyWordEntityModel>,IKeyWordRepository
    {
        public KeyWordRepository(LibraryContext context)
                                 :base(context){}

        public ICollection<Guid> ChekKeyWords(IEnumerable<string> nameList)
        {
            var keyWordList = GetAll();
            List<Guid> resultList = new List<Guid>();

            foreach (string keyword in nameList)
            {
                var wordModel = keyWordList.FirstOrDefault(word => word.Name == keyword);

                if (wordModel == null)
                {
                    var id = CreateKeyWord(keyword).Result.Id;
                    resultList.Add(Guid.Parse(id.ToString()));
                }
                else
                {
                    resultList.Add(wordModel.Id);
                }
            }
            return resultList;
        }

        public ICollection<string> GetListWords()
        {
          return GetAll().Select(keyword => keyword.Name).ToList();
        }

        public ICollection<string> ChekKeyWords(IEnumerable<Guid> idList)
        {
            return GetQuery().Where(key => idList.Contains(key.Id)).Select(k=>k.Name).ToList();
        }

        /// <summary>
        /// Добавление нового ключевого слова
        /// </summary>
        /// <param name="name"> Название ключевого слова </param>
        /// <returns> Модель ключевого слова </returns>
        async Task<KeyWordEntityModel> CreateKeyWord(string name)
        {
            KeyWordEntityModel model = new KeyWordEntityModel() { Name = name};

            return await InsertAsync(model);
        }
    }
}
