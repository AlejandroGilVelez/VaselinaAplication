using System.IO;
using System.Reflection;
using System.Text;

namespace Framework.Utilidades
{
    public static class TemplateUtils
    {
        /// <summary>
        /// Método generico para retornar un template.
        /// </summary>
        /// <param name="templateName"></param>
        /// <returns></returns>
        public static string BodyTemplate(string templateName)
        {
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(templateName))
            {
                byte[] fileContent = new byte[stream.Length];
                stream.Read(fileContent, 0, (int)stream.Length);
                return Encoding.UTF8.GetString(fileContent, 0, fileContent.Length);
            }
        }
    }
}
