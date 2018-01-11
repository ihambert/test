using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Common
{
    public class StringUtil
    {
        /// <summary>
        ///     根据传入str进行遍历取出列表
        /// </summary>
        /// <param name="str">传入字符串</param>
        /// <param name="startStr">开始字符串</param>
        /// <param name="endStr">结束字符串</param>
        /// <param name="remove">是否去除开始和结束字符串取出数据</param>
        /// <returns></returns>
        public static List<string> GetList(string str, string startStr, string endStr, bool remove = true)
        {
            var lst = new List<string>();
            int startIndex = 0;
            while (true)
            {
                string v = GetVal(str, startStr, endStr, remove, ref startIndex);
                if (startIndex == -1)
                {
                    return lst;
                }
                lst.Add(v);
            }
        }

        public static string GetVal(string str, string startStr, string endStr, bool remove = true, int startIndex = 0)
        {
            return GetVal(str, startStr, endStr, remove, ref startIndex);
        }

        private static string GetVal(string str, string startStr, string endStr, bool remove, ref int startIndex)
        {
            int istart = str.IndexOf(startStr, startIndex, StringComparison.CurrentCulture);
            if (istart == -1)
            {
                startIndex = -1;
                return string.Empty;
            }

            int iend = str.IndexOf(endStr, istart + startStr.Length, StringComparison.Ordinal);

            if (iend == -1)
            {
                startIndex = -1;
                return string.Empty;
            }

            startIndex = iend + endStr.Length;

            if (remove)
            {
                istart += startStr.Length;
                return str.Substring(istart, iend - istart);
            }

            return str.Substring(istart, startIndex - istart);
        }

        /// <summary>
        ///     根据传入str进行遍历取出列表
        /// </summary>
        /// <param name="str">传入字符串</param>
        /// <param name="startStr">开始字符串</param>
        /// <param name="needLength">需要获取的长度(不含开始字符串的长度)</param>
        /// <param name="remove">是否去除开始字符串取出数据</param>
        /// <returns></returns>
        public static List<string> GetList(string str, string startStr, int needLength, bool remove = true)
        {
            var lst = new List<string>();
            int startIndex = 0;
            while (true)
            {
                string v = GetVal(str, startStr, needLength, remove, ref startIndex);
                if (startIndex == -1)
                {
                    return lst;
                }
                lst.Add(v);
            }
        }

        public static string GetVal(string str, string startStr, int needLength, bool remove = true, int startIndex = 0)
        {
            return GetVal(str, startStr, needLength, remove, ref startIndex);
        }

        public static string GetVal(string str, string startStr, int needLength, bool remove, ref int startIndex)
        {
            int istart = str.IndexOf(startStr, startIndex, StringComparison.Ordinal);

            if (istart == -1)
            {
                startIndex = -1;
                return string.Empty;
            }

            startIndex = istart + startStr.Length + needLength;

            if (startIndex > str.Length)
            {
                startIndex = -1;
                return string.Empty;
            }

            return remove
                ? str.Substring(istart + startStr.Length, needLength)
                : str.Substring(istart, startStr.Length + needLength);
        }

        ///// <summary>
        /////     汉字转化为拼音首字母
        ///// </summary>
        ///// <param name="str">汉字</param>
        ///// <returns>首字母</returns>
        //public static string GetFirstPinyin(string str)
        //{
        //    string r = string.Empty;
        //    foreach (char obj in str)
        //    {
        //        try
        //        {
        //            var chineseChar = new ChineseChar(obj);
        //            string t = chineseChar.Pinyins[0];
        //            r += t.Substring(0, 1);
        //        }
        //        catch
        //        {
        //            r += obj.ToString();
        //        }
        //    }
        //    return r;
        //}

        ///// <summary>
        /////     汉字转化为拼音首字母
        ///// </summary>
        ///// <param name="str">汉字</param>
        ///// <returns>首字母</returns>
        //public static string GetFirstOnePinyin(string str)
        //{
        //    if (string.IsNullOrWhiteSpace(str))
        //    {
        //        return "";
        //    }

        //    string r = string.Empty;
        //    try
        //    {
        //        var chineseChar = new ChineseChar(str[0]);
        //        string t = chineseChar.Pinyins[0];
        //        r += t.Substring(0, 1);
        //    }
        //    catch
        //    {
        //        r += str[0].ToString(CultureInfo.InvariantCulture);
        //    }
        //    return r;
        //}

        /// <summary>
        ///     获取字符串里的所有href链接
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static List<string> GetHrefs(string str)
        {
            return GetList(str, "href=\"", "\"");
        }

        /// <summary>
        ///     获取字符串里的首个href链接
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetHref(string str)
        {
            return GetVal(str, "href=\"", "\"");
        }

        public static string ToGB2312(string str)
        {
            string r = "";
            MatchCollection mc = Regex.Matches(str, @"\\u([\w]{2})([\w]{2})",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var bts = new byte[2];
            foreach (Match m in mc)
            {
                bts[0] = (byte) int.Parse(m.Groups[2].Value, NumberStyles.HexNumber);
                bts[1] = (byte) int.Parse(m.Groups[1].Value, NumberStyles.HexNumber);
                r += Encoding.Unicode.GetString(bts);
            }
            return r;
        }

        /// <summary>
        /// 除去所有在html元素中标记
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string RemoveHtml(string html)
        {
            //去除<span>和</span>这种括号包围的标签
            Regex regex = new Regex(@"<[^>]+>|</[^>]+>",
                RegexOptions.Compiled);
            //html转义，如 &gt; 转义为 >
            return HttpUtility.HtmlDecode(regex.Replace(html, "").Trim());
        }
    }
}