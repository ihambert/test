using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace GradingFund
{
    internal class GradingFundUtil
    {
        //声明一个委托
        public delegate void MyDelegate(GradingFundData data);

        public void Start()
        {
            //测试：记录当前线程ID
            Logger.Debug($"基金服务类主线程ID：{Thread.CurrentThread.ManagedThreadId}");

            #region 初始化基金和股票代码数据

            var gfs = File.ReadAllLines(FileGradingFund);
            Dictionary<int, Task<string>> initTasks = null;
            Task<string> baseFundTask = null;

            for (var i = 0; i < gfs.Length; i++)
            {
                var gf = gfs[i];
                var codes = gf.Split(' ');
                if (codes.Length < 22)
                {
                    if (initTasks == null)
                        initTasks = new Dictionary<int, Task<string>>();
                    initTasks.Add(i, _web.GetStringAsync(UrlGradingStocks + codes[0]));
                    baseFundTask = _web.GetStringAsync(UrlBaseFund);
                }
                else
                {
                    var stocks = new List<Stock>();
                    var holdingRatio = new List<float>();

                    for (var j = 2; j < 12; j++)
                    {
                        var stock = _data.Stocks.Find(o => o.Code == codes[j]);

                        if (stock == null)
                        {
                            stock = new Stock
                            {
                                Code = codes[j]
                            };
                            _data.Stocks.Add(stock);
                        }

                        stocks.Add(stock);
                    }

                    for (int j = 11; j < codes.Length; j++)
                    {
                        holdingRatio.Add(Convert.ToSingle(codes[j]));
                    }

                    _data.GradingFunds.Add(new GradingFund
                    {
                        Code = codes[0],
                        Stocks = stocks,
                        Leader = stocks[0],
                        MaxSpeed = stocks[0],
                        HoldingRatio = holdingRatio,
                        BaseFund = codes[1]
                    });
                }
            }

            #region 初始化行业概念（依赖于东方财富行业概念）

            var ics = File.ReadAllLines(FileIndustry);
            foreach (var s in ics[0].Split(' '))
            {
                _data.IndustryFlow.Add(new Industry
                {
                    Name = s
                });
            }
            foreach (var s in ics[1].Split(' '))
            {
                _data.ConceptFlow.Add(new Industry
                {
                    Name = s
                });
            }

            #endregion

            if (initTasks != null)
            {
                var mj = baseFundTask.Result;
                var mjs = StringUtil.GetList(mj, "base_fund_id\":\"", "\"");
                var stockCodes = StringUtil.GetList(mj, "fundB_id\":\"", "\"");

                foreach (var task in initTasks)
                {
                    var code = gfs[task.Key].Split(' ')[0];

                    if (task.Value.IsFaulted)
                    {
                        Logger.Error(UrlGradingStocks + code, task.Value.Exception);
                        return;
                    }

                    var h = task.Value.Result;
                    var sc = StringUtil.GetList(h, "<tr>", "</tr>");

                    if (sc.Count == 0)
                    {
                        Logger.Warn(UrlGradingStocks + code);
                        return;
                    }

                    var stocks = new List<Stock>();
                    var holdingRatio = new List<float>();

                    for (var i = 1; i < 11; i++)
                    {
                        var hs = StringUtil.GetList(sc[i], ">", "</td>");
                        var stockCode = StringUtil.GetVal(hs[1], ">", "<");
                        var stock = _data.Stocks.Find(o => o.Code == stockCode);

                        if (stock == null)
                        {
                            stock = new Stock
                            {
                                Code = stockCode
                            };
                            _data.Stocks.Add(stock);
                        }

                        stocks.Add(stock);
                        holdingRatio.Add(Convert.ToSingle(hs[6].Replace("%", "")));
                    }
                    var ii = stockCodes.FindIndex(o => o == code);
                    var bf = mjs[ii];

                    _data.GradingFunds.Add(new GradingFund
                    {
                        Code = code,
                        Stocks = stocks,
                        Leader = stocks[0],
                        MaxSpeed = stocks[0],
                        HoldingRatio = holdingRatio,
                        BaseFund = bf
                    });
                    gfs[task.Key] =
                        $"{code} {bf} {string.Join(" ", stocks.Select(o => o.Code))} {string.Join(" ", holdingRatio)}";
                }
                File.WriteAllLines(FileGradingFund, gfs);
            }

            #endregion

            var thread = new Thread(UpdateData);
            thread.Start();
            //测试：记录当前线程ID
            Logger.Debug("开始循环获取数据");
        }

        public string UrlGradingFunds
        {
            get
            {
                if (_urlGradingFunds == null)
                {
                    var codes = new List<string>();

                    foreach (var fund in _data.GradingFunds)
                    {
                        var area = fund.Code.StartsWith("1") ? "sz" : "sh";
                        codes.Add(area + fund.Code);
                    }

                    _urlGradingFunds = UrlStockFund + string.Join(",", codes);
                }

                return _urlGradingFunds;
            }
        }

        private void UpdateData()
        {
            //测试：记录当前线程ID
            Logger.Debug($"UpdateData线程ID：{Thread.CurrentThread.ManagedThreadId}");

            #region 统一异步获取网页数据，可以提高性能

            //概念数据耗时大概188ms-290ms
            var taskConcept = _web.GetStringAsync(UrlConcept);
            //行业数据耗时大概60ms-188ms
            var taskIndustry = _web.GetStringAsync(UrlIndustry);
            for (var i = 0; i < _data.Stocks.Count; i += RequsetStockCount)
            {
                var url = UrlStock + string.Join(",", _data.Stocks.Skip(i).Take(RequsetStockCount).Select(o => o.Code));
                //股票数据耗时大概86ms
                _tasks.Add(_web.GetStringAsync(url));
            }
            //分级数据耗时大概82ms
            var taskGradings = _web.GetStringAsync(UrlGradingFunds);

            #endregion

            string html;

            #region 处理股票实时数据

            for (var i = 0; i < _tasks.Count; i++)
            {
                if (_tasks[i].IsFaulted)
                {
                    Logger.Error("金融界股票接口", _tasks[i].Exception);
                    ContinueUpdate();
                    return;
                }

                try
                {
                    html = _tasks[i].Result;
                }
                catch (Exception e)
                {
                    Logger.Error("金融界股票接口异常", e);
                    ContinueUpdate();
                    return;
                }

                var hq = StringUtil.GetVal(html, "HqData:[", "]}");
                var fs = StringUtil.GetList(hq, "[", "]");

                if (fs.Count == 0)
                {
                    Logger.Warn("金融界股票接口获取空数据");
                    ContinueUpdate();
                    return;
                }

                var ii = i*RequsetStockCount;

                foreach (var f in fs)
                {
                    var a = f.Split(',');
                    var stock = _data.Stocks[ii++];
                    stock.Name = a[0].Substring(1, a[0].Length - 2);
                    stock.YesterdayPrice = Convert.ToSingle(a[1]);
                    stock.StartPrice = Convert.ToSingle(a[2]);
                    stock.MaxPrice = Convert.ToSingle(a[3]);
                    stock.MinPrice = Convert.ToSingle(a[4]);
                    stock.Price = Convert.ToSingle(a[5]);
                    stock.Tm = float.Parse((Convert.ToSingle(a[6])/10000).ToString("F1"));
                    stock.Cat = Convert.ToSingle(a[7]);
                    stock.Tr = Convert.ToSingle(a[8]);
                    stock.Ape = Convert.ToSingle(a[9]);
                    var rate = (stock.Price - stock.YesterdayPrice)*100/stock.YesterdayPrice;
                    stock.Speed = Math.Round(rate - stock.Rate, 2);
                    stock.Rate = Math.Round(rate, 2);
                    stock.Swing = Math.Round((stock.MaxPrice - stock.MinPrice)*100/stock.YesterdayPrice, 2);
                }
            }
            _tasks.Clear();
            if (_data.IsOpen)
            {
                //删除停牌个股
                var rc = _data.Stocks.RemoveAll(o => o.MaxPrice == 0);

                if (rc > 0)
                {
                    foreach (var fund in _data.GradingFunds)
                    {
                        fund.Stocks.RemoveAll(o => o.MaxPrice == 0);
                    }
                }
            }

            #endregion

            #region 处理分级基金实时数据

            if (taskGradings.IsFaulted)
            {
                Logger.Error("新浪股票基金接口", taskGradings.Exception);
                ContinueUpdate();
                return;
            }

            try
            {
                html = taskGradings.Result;
            }
            catch (Exception e)
            {
                Logger.Error("新浪股票基金接口异常", e);
                ContinueUpdate();
                return;
            }

            var codes = StringUtil.GetList(html, "hq_str_", "=");

            if (codes.Count == 0)
            {
                Logger.Warn("新浪股票基金接口获取空数据");
                ContinueUpdate();
                return;
            }

            var gs = StringUtil.GetList(html, "=\"", "\"");

            for (int i = 0; i < gs.Count; i++)
            {
                var ts = gs[i].Split(',');
                var code = codes[i].Substring(2, codes[i].Length - 2);
                var fund = _data.GradingFunds.Find(o => o.Code == code);
                fund.Name = ts[0];
                fund.StartPrice = Convert.ToSingle(ts[1]);
                fund.YesterdayPrice = Convert.ToSingle(ts[2]);
                fund.Price = Convert.ToSingle(ts[3]);
                fund.MaxPrice = Convert.ToSingle(ts[4]);
                fund.MinPrice = Convert.ToSingle(ts[5]);
                //统计数据
                fund.Rate = Math.Round((fund.Price - fund.YesterdayPrice)*100/fund.YesterdayPrice, 2);
                fund.Swing = Math.Round((fund.MaxPrice - fund.MinPrice)*100/fund.YesterdayPrice, 2);
                if (fund.Stocks.Count == 0) continue;
                double rateAll = 0;
                int redCount = 0;
                int stockCount = 0;
                float ratioAll = 0;
                for (int l = 0; l < fund.Stocks.Count; l++)
                {
                    var stock = fund.Stocks[l];
                    if (stock.Rate > -10.1)
                    {
                        rateAll += stock.Rate*fund.HoldingRatio[l];
                        stockCount++;
                        ratioAll += fund.HoldingRatio[l];

                        if (stock.Rate > 0)
                        {
                            redCount++;
                        }
                    }

                    if (stock.Rate > fund.Leader.Rate)
                    {
                        fund.Leader = stock;
                    }
                    if (stock.Speed > fund.MaxSpeed.Speed)
                    {
                        fund.MaxSpeed = stock;
                    }
                }

                var avgRate = rateAll/ratioAll;
                fund.Speed = Math.Round(avgRate - fund.AvgRate, 2);
                fund.AvgRate = Math.Round(avgRate, 2);
                fund.RedRate = redCount*100/stockCount;
            }

            _data.GradingFunds = _data.GradingFunds.OrderByDescending(o => o.AvgRate).ToList();

            #endregion

            #region 处理行业实时数据

            if (taskIndustry.IsFaulted)
            {
                Logger.Error("东方财富行业数据", taskIndustry.Exception);
                ContinueUpdate();
                return;
            }

            try
            {
                html = taskIndustry.Result;
            }
            catch (Exception e)
            {
                Logger.Error("东方财富行业数据异常", e);
                ContinueUpdate();
                return;
            }
            var ihtml = html.Split('"');
            //资金总体流入
            double inflow = 0;
            for (var i = 1; i < ihtml.Length; i += 2)
            {
                var ts = ihtml[i].Split(',');
                var industry = _data.IndustryFlow.Find(o => o.Name == ts[2]);
                var inmoney = double.Parse(ts[3])/10000;
                inflow += inmoney;

                if (industry != null)
                {
                    inmoney = Math.Round(inmoney, 1);
                    industry.Inf = inmoney - industry.Inflow;
                    industry.Inflow = inmoney;
                }
            }

            if (_data.Inflow != 0)
            {
                //排除首次
                _data.Inf = Math.Round(inflow - _data.Inflow, 1);
            }
            _data.Inflow = Math.Round(inflow, 1);

            #endregion

            #region 处理概念实时数据

            if (taskConcept.IsFaulted)
            {
                Logger.Error("东方财富概念数据", taskConcept.Exception);
                ContinueUpdate();
                return;
            }

            try
            {
                html = taskConcept.Result;
            }
            catch (Exception e)
            {
                Logger.Error("东方财富概念数据异常", e);
                ContinueUpdate();
                return;
            }

            var hs = html.Split('"');
            for (var i = 1; i < hs.Length; i += 2)
            {
                var ts = hs[i].Split(',');
                var concept = _data.ConceptFlow.Find(o => o.Name == ts[2]);

                if (concept != null)
                {
                    var inmoney = Math.Round(float.Parse(ts[3])/10000, 1);
                    concept.Inf = inmoney - concept.Inflow;
                    concept.Inflow = inmoney;
                }
            }

            #endregion

            //测试：记录当前线程ID
            Logger.Debug("UpdateData获取数据完毕");

            Updated?.Invoke(_data);
            ContinueUpdate();
        }

        private void ContinueUpdate()
        {
            //将被线程挂起，等待一段时间后继续占用CPU
            Thread.Sleep(SleepTime);

            if (_data.IsDealTime)
            {
                UpdateData();
            }
        }

        #region 常量变量

        #region 常量

        //获取股票最新数据的接口，后接股票代码，每个股票用逗号隔开;op开盘价np最新价hp最高价lp最低价tm成交额cat量比tr换手率name股票名称lcp昨收ape市盈率
        private const string UrlStock = "http://q.jrjimg.cn/?q=cn|s&n=stockHQ&c=op,np,hp,lp,tm,cat,tr,name,lcp,ape&i=";

        /// <summary>
        ///     获取股票基金最新数据的接口用法如http://hq.sinajs.cn/list=sz000002,sh601116,sz150195,sh511880
        /// </summary>
        private const string UrlStockFund = "http://hq.sinajs.cn/list=";

        /// <summary>
        ///     获取分级对应的持仓股票
        /// </summary>
        private const string UrlGradingStocks = "http://fund.eastmoney.com/f10/FundArchivesDatas.aspx?type=jjcc&topline=10&code=";

        ///// <summary>
        /////     获取分级估算涨幅
        ///// </summary>
        //private const string UrlGrading = "http://fundgz.1234567.com.cn/js/{0}.js";

        /// <summary>
        ///     获取东方财富行业资金流向
        /// </summary>
        private const string UrlIndustry =
            "http://nufm.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd=C._BKHY&sty=DCFFPBFM&st=(BalFlowMain)&sr=-1&ps=999&token=894050c76af8597a853f5b408b759f5d";

        /// <summary>
        ///     获取东方财富概念资金流向
        /// </summary>
        private const string UrlConcept =
            "http://nufm.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd=C._BKGN&sty=DCFFPBFM&st=(BalFlowMain)&sr=-1&ps=10000&token=894050c76af8597a853f5b408b759f5d";

        /// <summary>
        /// 获取分级母基金名称
        /// </summary>
        private const string UrlBaseFund = "https://www.jisilu.cn/data/sfnew/fundm_list/";

        /// <summary>
        /// 每次请求的股票个数
        /// </summary>
        private const int RequsetStockCount = 100;

        /// <summary>
        /// 每次刷新数据的间隔时间（单位：毫秒）
        /// </summary>
        private const int SleepTime = 60000;

        /// <summary>
        ///     存放分级的文件
        /// </summary>
        private const string FileGradingFund = "File/GradingFunds.txt";

        /// <summary>
        ///     存放关注的行业和概念的文件（第一行是行业，第二行是概念）
        /// </summary>
        private const string FileIndustry = "File/Industry.txt";

        #endregion

        /// <summary>
        ///     数据
        /// </summary>
        private readonly GradingFundData _data = new GradingFundData();

        private readonly HttpClient _web = new HttpClient();

        private string _urlGradingFunds;

        private readonly List<Task<string>> _tasks = new List<Task<string>>();

        //数据更新事件
        public event MyDelegate Updated;

        #endregion
    }
}