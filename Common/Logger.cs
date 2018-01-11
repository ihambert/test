using System;
using System.IO;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    ///     简易日志类（咱就想简单记个日志，需要那么复杂吗）
    /// </summary>
    public class Logger
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
                : $",InMessage:{e.InnerException.Message},InStackTrace:{e.InnerException.StackTrace}";
                Log(_fileError, $"{msg}，Message:{e.Message},StackTrace:{e.StackTrace}{innerEx}");
            }
        }

        /// <summary>
        ///     记录警告信息
        /// </summary>
        /// <param name="msg">警告内容</param>
        public static void Warn(string msg)
        {
            Log(_fileWarn, msg);
        }

        /// <summary>
        ///     记录普通信息
        /// </summary>
        /// <param name="msg">一般信息</param>
        public static void Info(string msg)
        {
            Log(_fileInfo, msg);
        }

        /// <summary>
        ///     记录调试信息
        /// </summary>
        /// <param name="msg">调试信息</param>
        public static void Debug(string msg)
        {
            Log(_fileDebug, msg);
        }

        private static void Log(string fileName, string msg)
        {
            //开新线程写日志不阻塞原线程（虽然也无需多长时间）
            Task.Factory.StartNew(() =>
            {
                msg = $"{DateTime.Now:yy-M-d H:m:s}：{msg}{Environment.NewLine}";

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

        /// <summary>
        /// 注册启动日志
        /// </summary>
        public void Register()
        {
            const string fileBase = "File/Log";

            if (!Directory.Exists(fileBase))
            {
                Directory.CreateDirectory(fileBase);
            }

            var ym = DateTime.Now.ToString("yyMM");
            _fileError = $"{fileBase}/Error{ym}.txt";
            _fileWarn = $"{fileBase}/Warn{ym}.txt";
            _fileInfo = $"{fileBase}/Info{ym}.txt";
            _fileDebug = $"{fileBase}/Debug{ym}.txt";
        }

        #region 常量

        //以一个月对一个单位，每个月生成一个文件
        private static string _fileError;
        private static string _fileWarn;
        private static string _fileInfo;
        private static string _fileDebug;

        #endregion
    }
}