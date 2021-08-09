using Library.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Logic.Logics
{
    public class ImageLogic : IImageLogic
    {
        public byte[] ToBytes(IFormFile image)
        {
            if (image == null)
            {
                throw new BuisnessException("Файл не указан");
            }
            var binaryReader = new BinaryReader(image.OpenReadStream());

            return binaryReader.ReadBytes((int)image.Length);
        }
    }
}
