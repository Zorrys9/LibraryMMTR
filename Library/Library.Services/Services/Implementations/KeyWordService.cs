using Library.Data.Repository;
using System;
using System.Collections.Generic;

namespace Library.Services.Services.Implementations
{
    public class KeyWordService : IKeyWordService
    {

        private readonly IKeyWordRepository _keyWordRepository;

        public KeyWordService(IKeyWordRepository keyWordRepository)
        {
            _keyWordRepository = keyWordRepository;
        }




        public List<Guid> CheckWord(List<string> nameList)
        {
            if (nameList != null)
            {

                return _keyWordRepository.ChekKeyWords(nameList);

            }
            else
            {

                throw new Exception("Список названий ключевых слов был пуст");

            }
        }

        public List<string> GetAll()
        {
            var result = _keyWordRepository.GetListWords();

            return result;
        }

        public List<string> CheckWord(List<Guid> idList)
        {
            if (idList != null)
            {

                return _keyWordRepository.ChekKeyWords(idList);

            }
            else
            {

                throw new Exception("Список Id книг был пуст");

            }
        }

    }
}
