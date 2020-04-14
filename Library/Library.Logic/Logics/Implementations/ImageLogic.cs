using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Logic.Logics
{
    public class ImageLogic : IImageLogic
    {
        public ImageLogic()
        {

        }

        /// <summary>
        /// Преобразование из файла в байты
        /// </summary>
        /// <param name="image"> файл </param>
        /// <returns> Массив байтов </returns>
        public byte[] ToBytes(IFormFile image)
        {
            if (image != null)
            {
                var binaryReader = new BinaryReader(image.OpenReadStream());

                var result = binaryReader.ReadBytes((int)image.Length);

                return result;
            }
            else
            {
                throw new Exception("Файл не указан");
            }
        }

    }
}
