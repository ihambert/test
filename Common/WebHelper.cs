using System.Net;
using System.Text;

namespace Common
{
    public class WebHelper
    {
        public readonly WebClient Web = new WebClient();
        private int _tryTimes;

        public WebHelper()
        {
            Web.Encoding = Encoding.UTF8;
        }

        public WebHelper(Encoding encoding)
        {
            Web.Encoding = encoding;
        }

        public Encoding Encoding
        {
            set { Web.Encoding = value; }
        }

        /// <summary>
        ///     下载请求的资源
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public string DownloadString(string url)
        {
            try
            {
                return Web.DownloadString(url);
            }
            catch (WebException e)
            {
                if (e.Message.Contains("404") || e.Status == WebExceptionStatus.ConnectFailure ||
                    e.Status == WebExceptionStatus.ProtocolError || _tryTimes == 2)
                {
                    _tryTimes = 0;
                    return null;
                }

                _tryTimes++;
                return DownloadString(url);
            }
        }

        /// <summary>
        ///     下载请求的资源(资源采用Gzip压缩)
        /// </summary>
        /// <param name="url">URL</param>
        /// <returns></returns>
        public string DownloadGzipString(string url)
        {
            Web.Headers.Add("Accept-Encoding", "gzip");
            try
            {
                return Web.Encoding.GetString(ZipUtil.GzipDecompress(Web.DownloadData(url)));
            }
            catch (WebException e)
            {
                if (e.Message.Contains("404") || e.Status == WebExceptionStatus.ConnectFailure ||
                    e.Status == WebExceptionStatus.ProtocolError || _tryTimes == 2)
                {
                    _tryTimes = 0;
                    return null;
                }

                _tryTimes++;
                return DownloadGzipString(url);
            }
            finally
            {
                Web.Headers.Remove("Accept-Encoding");
            }
        }

        /// <summary>
        ///     将指定的字符串上载到指定的资源
        /// </summary>
        /// <param name="address">地址</param>
        /// <param name="data">参数</param>
        /// <param name="useProxy">是否使用代理</param>
        /// <returns></returns>
        public string UploadString(string address, string data, bool useProxy = false)
        {
            Web.Headers.Add("Content-Type", "application/x-www-form-urlencoded");

            try
            {
                return Web.UploadString(address, "POST", data);
            }
            catch
            {
                if (_tryTimes == 2)
                {
                    _tryTimes = 0;
                    return null;
                }

                _tryTimes++;
                return UploadString(address, data);
            }
        }
    }
}