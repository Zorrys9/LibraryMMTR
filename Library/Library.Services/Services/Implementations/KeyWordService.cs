using Library.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Services.Implementations
{
    public class KeyWordService : IKeyWordService
    {

        private readonly IKeyWordRepository _keyWordRepository;

        public KeyWordService(IKeyWordRepository keyWordRepository)
        {
            _keyWordRepository = keyWordRepository;
        }


        /// <summary>
        /// Проверка ключевых слов
        /// </summary>
        /// <param name="nameList"> Список названий ключевых слов </param>
        /// <returns> Список Id ключевых слов </returns>
        public List<Guid> CheckWord(List<string> nameList)
        {

            if(nameList != null)
            {

                return _keyWordRepository.ChekKeyWords(nameList);

            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Возвращает список названий всех ключевых слов
        /// </summary>
        /// <returns> Список названий всех ключевых слов </returns>
        public List<string> GetAll()
        {
            var result = _keyWordRepository.GetListWords();

            return result;
        }

        /// <summary>
        /// Проверка ключевых слов книги
        /// </summary>
        /// <param name="idList"> Список Id ключевых слов </param>
        /// <returns> Список названий ключевых слов </returns>
        public List<string> CheckWord(List<Guid> idList)
        {

            if(idList != null)
            {

                return _keyWordRepository.ChekKeyWords(idList);

            }
            else
            {

                return null;

            }

        }

    }
}
