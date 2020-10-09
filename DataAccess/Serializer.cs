using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Web;

namespace DataAccess
{

    static class Serializer
    {
      

        /// <summary>
        /// Sets up the current page or handler to use GZip through a Response.Filter
        /// IMPORTANT:  
        /// You have to call this method before any output is generated!
        /// </summary>
  

        public static T Decompress<T>(byte[] compressedData) where T : class
        {
            using (MemoryStream memory = new MemoryStream(compressedData))
            {
                using (GZipStream zip = new GZipStream(memory, CompressionMode.Decompress, false))
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    return formatter.Deserialize(zip) as T;
                }
            }
        }


        public static byte[] Compress<T>(T data)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                using (GZipStream zip = new GZipStream(memory, CompressionMode.Compress, false))
                {
                    var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    formatter.Serialize(zip, data);
                }

                return memory.ToArray();
            }
        }
        public static string Decompress(string input)
        {
            byte[] compressed = Convert.FromBase64String(input);
            byte[] decompressed = Decompress(compressed);
            return Encoding.UTF8.GetString(decompressed);
        }

        public static string Compress(string input)
        {
            byte[] encoded = Encoding.UTF8.GetBytes(input);
            byte[] compressed = Compress(encoded);
            return Convert.ToBase64String(compressed);
        }
        public static byte[] Decompress(byte[] input)
        {
            using (var source = new MemoryStream(input))
            {
                byte[] lengthBytes = new byte[4];
                source.Read(lengthBytes, 0, 4);

                var length = BitConverter.ToInt32(lengthBytes, 0);
                using (var decompressionStream = new GZipStream(source,
                    CompressionMode.Decompress))
                {
                    var result = new byte[length];
                    decompressionStream.Read(result, 0, length);
                    return result;
                }
            }
        }

        public static byte[] Compress(byte[] input)
        {
            using (var result = new MemoryStream())
            {
                var lengthBytes = BitConverter.GetBytes(input.Length);
                result.Write(lengthBytes, 0, 4);

                using (var compressionStream = new GZipStream(result,
                    CompressionMode.Compress))
                {
                    compressionStream.Write(input, 0, input.Length);
                    compressionStream.Flush();

                }
                return result.ToArray();
            }
        }
        public static byte[] ToBinary<T>(T data)
        {
            using (MemoryStream memory = new MemoryStream())
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                formatter.Serialize(memory, data);
                return memory.ToArray();
            }
        }


        public static T FromBinary<T>(byte[] binary) where T : class
        {
            using (MemoryStream memory = new MemoryStream(binary))
            {
                var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                return formatter.Deserialize(memory) as T;
            }
        }
    }
}
