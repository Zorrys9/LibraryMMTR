using Library.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Data.Repository
{
    public interface IKeyWordsRepository
    {

        KeyWordsEntityModel CreateKeyWord(string name);
        List<Guid> ChekKeyWords(List<string> nameList);

    }
}
