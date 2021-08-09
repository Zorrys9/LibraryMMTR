using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Exceptions
{
    /// <summary>
    /// Исключение для ошибок в бизнес-логике системы
    /// </summary>
    public class BuisnessException :Exception
    {
        public BuisnessException(string message)
                             :base(message){}


    }
}
