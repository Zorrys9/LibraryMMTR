using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.Logics
{
    public interface IImageLogic
    {

        /// <summary>
        /// Преобразование из файла в байты
        /// </summary>
        /// <param name="image"> файл </param>
        /// <returns> Массив байтов </returns>
        byte[] ToBytes(IFormFile image);

    }
}
