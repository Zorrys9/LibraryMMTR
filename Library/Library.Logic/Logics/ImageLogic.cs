using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Library.Logic.Logics
{
    public class ImageLogic
    {

        public static byte[] ToBytes(IFormFile image)
        {

            if (image != null)
            {

                byte[] result = null;

                var binaryReader = new BinaryReader(image.OpenReadStream());
                result = binaryReader.ReadBytes((int)image.Length);

                return result;
            }
            else return null;

        }

    }
}
