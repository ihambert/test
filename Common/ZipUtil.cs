using System.IO;
using System.IO.Compression;

namespace Common
{
    public class ZipUtil
    {
        /// <summary>
        /// Gzip压缩
        /// </summary>
        /// <param name="cbytes">需压缩的数据</param>
        /// <returns></returns>
        public static byte[] GzipCompress(byte[] cbytes)
        {
            using (MemoryStream cms = new MemoryStream())
            {
                using (GZipStream gzip = new GZipStream(cms, CompressionMode.Compress))
                {
                    //将数据写入基础流，同时会被压缩
                    gzip.Write(cbytes, 0, cbytes.Length);
                }
                return cms.ToArray();
            }
        }

        /// <summary>
        /// Gzip解压
        /// </summary>
        /// <param name="cbytes">需解压的数据</param>
        /// <returns></returns>
        public static byte[] GzipDecompress(byte[] cbytes)
        {
            using (MemoryStream dms = new MemoryStream())
            {
                using (MemoryStream cms = new MemoryStream(cbytes))
                {
                    using (GZipStream gzip = new GZipStream(cms, CompressionMode.Decompress))
                    {
                        byte[] bytes = new byte[1024];
                        int len;
                        //读取压缩流，同时会被解压
                        while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                        {
                            dms.Write(bytes, 0, len);
                        }
                        return dms.ToArray();
                    }
                }
            }
        }
    }
}
