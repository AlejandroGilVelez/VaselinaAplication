using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace VaselinaWeb.API.Utilidades
{
    public static class ConvertImagen
    {
        public static byte[] ImagenToArray(IFormFile imagen)
        {

            using (var ms = new MemoryStream())
            {
                imagen.CopyTo(ms);
                var fileBytes = ms.ToArray();
                //string s = Convert.ToBase64String(fileBytes);
                //product.Imagen = fileBytes;
                return fileBytes;
                // act on the Base64 data
            }
           
        }

        public static string ImagenToString(byte[] imagen)
        {

            return Convert.ToBase64String(imagen);
            
        }
    }
}
