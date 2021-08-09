using Library.Data.Repository;
using Library.Exceptions;
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

        public ICollection<Guid> CheckWordByNames(IEnumerable<string> nameList)
        {
            if (nameList == null)
            {
                throw new BuisnessException("Список названий ключевых слов был пуст");
            }
            return _keyWordRepository.ChekKeyWords(nameList);
        }

        public ICollection<string> GetAll()
        {
            return _keyWordRepository.GetListWords();
        }

        public ICollection<string> CheckWordById(IEnumerable<Guid> idList)
        {
            if (idList == null)
            {
                throw new BuisnessException("Список Id книг был пуст");
            }
            return _keyWordRepository.ChekKeyWords(idList);
        }
    }
}
