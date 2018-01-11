using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Common;

namespace Fund
{
    internal class FundUtil
    {
        //声明一个委托
        public delegate void MyDelegate();

        public void Start()
        {
            #region 获取天天基金账号的自选基金

            _web.Cookies = "pi=" + ConfigurationManager.AppSettings["pi"];
            var h = _web.Fetch(UrlFund);
            var hh = StringUtil.GetVal(h, "kfdatas:[", "],fbdatas").Replace("\"", "");
            var hhs = StringUtil.GetList(hh, "[", "]");
            var funds = new List<Fund>();
            var t = _web.Fetch("http://fund.eastmoney.com/js/fundcode_search.js");
            var ts = StringUtil.GetList(StringUtil.GetVal(t, "= [", "];"), "[\"", "]");
            
            foreach (var hh1 in hhs)
            {
                var s = hh1.Split(',');
                var fund = new Fund
                {
                    Code = s[0],
                    Name = s[1],
                    Rate = s[25] == "" ? 0 : Convert.ToDouble(s[25]),
                    Fee = Convert.ToSingle(s[29].Remove(s[29].Length - 1))
                };

                #region 基金分类

                var tt = ts.Find(o => o.StartsWith(fund.Code));
                if (tt.Contains("债券"))
                {
                    fund.Type = 2;
                }
                else if (tt.Contains("QDII"))
                {
                    fund.Type = 3;
                }
                else
                {
                    fund.Type = fund.Name.Contains("黄金") ? 3 : 1;
                }

                #endregion

                #region 初始化基金每日涨幅信息

                var x = _web.Fetch("http://fund.eastmoney.com/f10/F10DataApi.aspx?type=lsjz&page=1&per=90&code=" + fund.Code);
                var xs = StringUtil.GetList(x, "<td", "/td>");
                fund.Rates = new List<double>();

                for (int i = 3; i < xs.Count; i += 7)
                {
                    var r = StringUtil.GetVal(xs[i], ">", "%");
                    if (string.IsNullOrEmpty(r))
                    {
                        continue;
                    }
                    fund.Rates.Add(Convert.ToDouble(r));
                }

                fund.Rate2 = Math.Round(fund.Rates.Take(2).Sum(), 2);
                fund.Rate3 = Math.Round(fund.Rates.Take(3).Sum(), 2);
                fund.Rate5 = Math.Round(fund.Rates.Take(5).Sum(), 2);
                fund.Rate7 = Math.Round(fund.Rates.Take(7).Sum(), 2);
                fund.Rate30 = Math.Round(fund.Rates.Take(30).Sum(), 2);
                fund.Rate60 = Math.Round(fund.Rates.Take(60).Sum(), 2);
                fund.Rate90 = Math.Round(fund.Rates.Sum(), 2);

                #endregion

                #region 初始化基金费率信息

                var y = _web.Fetch($"http://fund.eastmoney.com/f10/jjfl_{fund.Code}.html");
                var yy = StringUtil.GetVal(y, "赎回费率<a", "</table>");
                var ys = StringUtil.GetList(yy, "<td", "/td>");
                fund.Fee7 = fund.Fee;
                fund.Fee += Convert.ToSingle(StringUtil.GetVal(ys[2], ">", "%"));

                if (ys[1].Contains("7"))
                {
                    fund.Fee7 += Convert.ToSingle(StringUtil.GetVal(ys[5], ">", "%"));
                }
                else
                {
                    fund.Fee7 = fund.Fee;
                }

                #endregion

                funds.Add(fund);
            }

            FundData.Funds = funds.OrderByDescending(o => o.Rate).ToList();

            #endregion

            //测试：记录当前线程ID
            Logger.Debug($"基金服务类主线程ID：{Thread.CurrentThread.ManagedThreadId}");

            var thread = new Thread(UpdateData);
            thread.Start();
            //测试：记录当前线程ID
            Logger.Debug("开始循环获取数据");
        }

        private void UpdateData()
        {
            //测试：记录当前线程ID
            Logger.Debug($"UpdateData线程ID：{Thread.CurrentThread.ManagedThreadId}");

            try
            {
                var h = _web.Fetch(UrlFund);
                var hh = StringUtil.GetVal(h, "kfdatas:[", "],fbdatas").Replace("\"", "");
                var hhs = StringUtil.GetList(hh, "[", "]");

                foreach (var hh1 in hhs)
                {
                    var s = hh1.Split(',');
                    var fund = FundData.Funds.Find(o => o.Code == s[0]);
                    fund.Rate = s[25] == "" ? 0 : Convert.ToDouble(s[25]);
                }

                FundData.Funds = FundData.Funds.OrderByDescending(o => o.Rate).ToList();
            }
            catch (Exception e)
            {
                Logger.Error("获取数据异常", e);
            }

            //测试：记录当前线程ID
            Logger.Debug("UpdateData获取数据完毕");
            Updated?.Invoke();
            ContinueUpdate();
        }

        private void ContinueUpdate()
        {
            //将被线程挂起，等待一段时间后继续占用CPU
            Thread.Sleep(SleepTime);

            if (FundData.IsDealTime)
            {
                UpdateData();
            }
        }

        #region 常量变量

        #region 常量

        /// <summary>
        /// 每次刷新数据的间隔时间（单位：毫秒）
        /// </summary>
        private const int SleepTime = 60000;

        private const string UrlFund = "http://fund.eastmoney.com/Data/FavorCenter_v3.aspx?o=r";

        #endregion

        /// <summary>
        ///     数据
        /// </summary>
        public readonly FundData FundData = new FundData();

        private readonly WebUtil _web = new WebUtil();

        //数据更新事件
        public event MyDelegate Updated;

        #endregion
    }
}