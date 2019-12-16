using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace Framework.Helpers
{
    public static class ImageHelper
    {
        public static byte[] ToByteArray(this IFormFile file, out string ContentType)
        {
            using (BinaryReader reader = new BinaryReader(file.OpenReadStream()))
            {
                ContentType = file.ContentType;
                return reader.ReadBytes((int)file.Length);
            }
        }

        public static FileResult ToPictureFile(byte[] picture, string ContentType)
        {
            return new FileContentResult(picture, ContentType);
        }
    }
}
