using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Common.ValidationAttributes
{
    /// <summary>
    /// Аттрибут для провеки года публацикации на валидность
    /// </summary>
    public class YearOfPublicationAttribute : ValidationAttribute
    {

        public override bool IsValid(object value)
        {
            if(value != null)
            {
                int year = (int)value;

                if(year >= 1 && year <= DateTime.Now.Year)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
