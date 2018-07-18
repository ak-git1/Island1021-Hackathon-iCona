using System.IO;
using System.IO.Compression;
using System.Text;

namespace Icona.Logic.Helpers
{
    /// <summary>
    /// Класс для компрессии строк
    /// </summary>
    public class CompressionHelper
    {
        /// <summary>
        /// Архивировать строку
        /// </summary>
        public static byte[] Zip(string str)
        {
            byte[] sourceBytes = Encoding.UTF8.GetBytes(str);

            using (MemoryStream memoryStreamInput = new MemoryStream(sourceBytes))
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(memoryStreamOutput, CompressionMode.Compress))
                {

                    memoryStreamInput.CopyTo(gZipStream);
                }

                return memoryStreamOutput.ToArray();
            }
        }


        /// <summary>
        /// Разархивировать строку
        /// </summary>
        public static string Unzip(byte[] bytes)
        {
            using (MemoryStream memoryStreamInput = new MemoryStream(bytes))
            using (MemoryStream memoryStreamOutput = new MemoryStream())
            {
                using (GZipStream gZipStream = new GZipStream(memoryStreamInput, CompressionMode.Decompress))
                {
                    gZipStream.CopyTo(memoryStreamOutput);
                }

                return Encoding.UTF8.GetString(memoryStreamOutput.ToArray());
            }
        }

    }
}
