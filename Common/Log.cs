using System;
using System.IO;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    ///     简易日志类（咱就想简单记个日志，需要那么复杂吗）
    /// </summary>
    public static class Log
    {
        /// <summary>
        ///     记录异常信息
        /// </summary>
        /// <param name="msg">异常附加信息</param>
        /// <param name="e">异常</param>
        public static void Error(string msg, Exception e)
        {
            if (e == null)
            {
                Warn(msg);
            }
            else
            {
                var innerEx = e.InnerException == null
                ? string.Empty
                : $",InMessage:{e.InnerException.Message},InSource:{e.InnerException.Source},InStackTrace:{e.InnerException.StackTrace}";
                Logger(FileError, $"{msg}，Message:{e.Message},Source:{e.Source},StackTrace:{e.StackTrace}{innerEx}");
            }
        }

        /// <summary>
        ///     记录警告信息
        /// </summary>
        /// <param name="msg">警告内容</param>
        public static void Warn(string msg)
        {
            Logger(FileWarn, msg);
        }

        /// <summary>
        ///     记录普通信息
        /// </summary>
        /// <param name="msg">一般信息</param>
        public static void Info(string msg)
        {
            Logger(FileInfo, msg);
        }

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="msg">调试信息</param>
        public static void Debug(string msg)
        {
            Logger(FileDebug, msg);
        }

        private static void Logger(string fileName, string msg)
        {
            //开新线程写日志不阻塞原线程（虽然也无需多长时间）
            Task.Factory.StartNew(() =>
            {
                msg = $"{DateTime.Now:yy-M-d H:m:s}：{msg}{Environment.NewLine}";

                if (!Directory.Exists(FileBase))
                    Directory.CreateDirectory(FileBase);

                try
                {
                    //加锁排队是必须的，否则快速插入日志的情况下会出现异常
                    lock (fileName)
                    {
                        File.AppendAllText(fileName, msg);
                    }
                }
                catch
                {
                    //发生异常一般是文件被占用，先写到另一个文件吧
                    File.AppendAllText(fileName.Replace(".txt", "2.txt"), msg);
                }
            });
        }

        #region 常量

        private const string FileBase = "File/Log";
        //以一个月对一个单位，每个月生成一个文件
        private static readonly string FileError = "File/Log/Error" + DateTime.Now.ToString("yyMM") + ".txt";
        private static readonly string FileWarn = "File/Log/Warn" + DateTime.Now.ToString("yyMM") + ".txt";
        private static readonly string FileInfo = "File/Log/Info" + DateTime.Now.ToString("yyMM") + ".txt";
        private static readonly string FileDebug = "File/Log/Debug" + DateTime.Now.ToString("yyMM") + ".txt";

        #endregion
    }
}