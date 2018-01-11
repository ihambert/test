using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;

namespace Common
{
    /// <summary>
    /// HTTP请求帮助类
    /// </summary>
    public class WebUtil
    {
        /// <summary>
        /// Cookie
        /// </summary>
        public string Cookies;

        /// <summary>
        /// 编码格式，默认是UTF8
        /// </summary>
        public Encoding Encoding = Encoding.UTF8;

        /// <summary>
        /// 请求的基址，可空
        /// </summary>
        public string BaseUrl = string.Empty;

        /// <summary>
        /// 内容类型，默认是text/html
        /// </summary>
        public string ContentType = "text/html";

        /// <summary>
        /// 发出HTTP请求
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="method">请求类型（GET，POST，PUT，DELETE）</param>
        /// <param name="data">请求的参数(GET请求直接写在url里)</param>
        /// <param name="isGzip">是否是gzip压缩过的网页</param>
        /// <returns></returns>
        public string Fetch(string url, string method = "GET", string data = null, bool isGzip = false)
        {
            var req = WebRequest.Create(BaseUrl + url);
            req.Method = method;
            req.ContentType = ContentType;

            if (Cookies != null)
            {
                req.Headers["cookie"] = Cookies;
            }

            if (string.IsNullOrEmpty(data))
            {
                req.ContentLength = 0;
            }
            else
            {
                req.ContentLength = data.Length;
                
                using (var stream = req.GetRequestStream())
                {
                    stream.Write(Encoding.GetBytes(data), 0, data.Length);
                }
            }

            using (var res = req.GetResponse())
            {
                using (var rs = res.GetResponseStream())
                {
                    if (rs == null)
                    {
                        return null;
                    }

                    if (isGzip)
                    {
                        using (MemoryStream dms = new MemoryStream())
                        {
                            using (GZipStream gzip = new GZipStream(rs, CompressionMode.Decompress))
                            {
                                byte[] bytes = new byte[1024];
                                int len;
                                //读取压缩流，同时会被解压
                                while ((len = gzip.Read(bytes, 0, bytes.Length)) > 0)
                                {
                                    dms.Write(bytes, 0, len);
                                }
                                return Encoding.GetString(dms.ToArray());
                            }
                        }
                    }

                    using (var sr = new StreamReader(rs, Encoding))
                    {
                        var ret = sr.ReadToEnd();
                        return ret;
                    }
                }
            }
        }
    }
}
