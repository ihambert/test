using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Common;

namespace GoodStock
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _webUtf8.Encoding = Encoding.UTF8;
            var now = DateTime.Now;
            _jjTime = new DateTime(now.Year, now.Month, now.Day, 9, 15, 0);
            _kpTime = new DateTime(now.Year, now.Month, now.Day, 9, 30, 0);
            _zspTime = new DateTime(now.Year, now.Month, now.Day, 11, 30, 0);
            _zkpTime = new DateTime(now.Year, now.Month, now.Day, 13, 0, 0);
            _ngTime = new DateTime(now.Year, now.Month, now.Day, 10, 30, 0);
            //近收盘时不能交易了
            _spTime = new DateTime(now.Year, now.Month, now.Day, 14, 58, 0);
            var htmlths = _web.DownloadString(RecommendUrl);
            var codes = StringUtil.GetList(htmlths, "code\":\"", "\"");
            foreach (var code in codes)
            {
                var stock = _thxStocks.Find(o => o.Code == code);
                if (stock == null)
                    _thxStocks.Add(new Stock
                    {
                        Code = code,
                        ChooseCount = 1
                    });
                else
                    stock.ChooseCount++;
            }
            _thxStocks = _thxStocks.OrderByDescending(o => o.ChooseCount).ToList();
            var funds = File.ReadAllText(GoodStock);
            var stocks = funds.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var s in stocks)
            {
                var ss = s.Split(' ');
                var stos = new List<string>();

                for (var i = 1; i < ss.Length; i++)
                    stos.Add(ss[i]);

                _industryStocks.Add(ss[0], stos);
            }

            foreach (var o in _thxStocks)
                if (o.ChooseCount > 1)
                {
                    AddStock(o.Code);
                    _tstocks.Add(o);
                }
                else if (funds.Contains(o.Code))
                {
                    AddStock(o.Code);
                }

            var tstocks = File.ReadAllText(TStock);

            if (!string.IsNullOrEmpty(tstocks))
                AddTstocks(tstocks.Split(' '), false);

            //string buystocks = File.ReadAllText(BuyStock);

            //if (!string.IsNullOrEmpty(buystocks))
            //{
            //    var bstocks = buystocks.Split(' ');
            //    AddTstocks(bstocks, false);
            //    foreach (var item in bstocks)
            //    {
            //        _buyStocks.Add(_goodStocks.Find(o => o.Code == item));
            //    }
            //}

            if (!File.Exists(GoodIndustryLog))
                File.Create(GoodIndustryLog);

            var industrylog = File.ReadAllLines(GoodIndustryLog);
            var ll = industrylog[industrylog.Length - 1];
            var today = now.ToString("yy-MM-dd");

            if (ll.StartsWith(today))
            {
                var l = ll.Split(' ');
                for (var i = 1; i < l.Length; i++)
                    AddIndustryStocks(l[i], false, false);
            }
            else
            {
                File.AppendAllText(GoodIndustryLog, Environment.NewLine + today);
            }

            var td = now.ToString("M-d ");
            var nglogs = File.ReadAllLines(NgLog).Where(o => o.StartsWith(td));

            foreach (var nglog in nglogs)
                _ngStocks.Add(AddStock(nglog.Split(' ')[2]));

            LoadData();
            _timer.Interval = 60000;
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        //是否获取网络数据并刷新界面
        public bool AskData
        {
            get
            {
                var now = DateTime.Now;

                if (((now > _jjTime) && (now < _zspTime)) || ((now > _zkpTime) && (now < _spTime)))
                    return true;

                return false;
            }
        }

        /// <summary>
        ///     获取请求多个股票最新股价动态的URL
        /// </summary>
        /// <returns></returns>
        private string GetAskStockUrl(IList<Stock> stocks, string command, int start, int end)
        {
            _sb.Clear().Append(command);

            for (var i = start; i < end; i++)
                _sb.Append(stocks[i].Code).Append(",");

            return _sb.ToString(0, _sb.Length - 1);
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            //开盘时间才执行
            if (AskData)
                LoadData();
        }

        private void LoadData()
        {
            //初始股票数据
            InitStocks(_goodStocks.Where(o => o.YesterdayPrice <= 0).ToList());
            var now = DateTime.Now;
            //是否交易时间
            var dealTime = now > _kpTime;
            //是否牛股时间（十点半前）
            var ngTime = now < _ngTime;

            #region 获取股票最新数据

            for (var i = 0; i < _goodStocks.Count;)
            {
                var j = i + RequsetStockCount;
                if (j > _goodStocks.Count)
                    j = _goodStocks.Count;
                string html;
                try
                {
                    html = _web.DownloadString(GetAskStockUrl(_goodStocks, StockCommand, i, j));
                }
                catch
                {
                    return;
                }

                var hq = StringUtil.GetVal(html, "HqData:[", "]}");
                var fs = StringUtil.GetList(hq, "[", "]");

                foreach (var f in fs)
                {
                    var a = f.Split(',');
                    var stock = _goodStocks[i++];
                    stock.StartPrice = Convert.ToSingle(a[0]);
                    stock.MaxPrice = Convert.ToSingle(a[1]);
                    stock.MinPrice = Convert.ToSingle(a[2]);
                    stock.Price = Convert.ToSingle(a[3]);
                    stock.Tm = float.Parse((Convert.ToSingle(a[4])/10000).ToString("F1"));
                    stock.Cat = Convert.ToSingle(a[5]);
                    stock.Tr = Convert.ToSingle(a[6]);
                    stock.Rate = Math.Round((stock.Price - stock.YesterdayPrice)*100/stock.YesterdayPrice, 2);
                    stock.Swing = Math.Round((stock.MaxPrice - stock.MinPrice)*100/stock.YesterdayPrice, 2);
                    stock.Hunter = Math.Round((stock.MaxPrice - stock.Price)*100/stock.MinPrice, 2);
                }
            }

            #endregion

            #region 刷新界面

            var funds = _goodStocks.OrderByDescending(o => o.Rate).ToList();
            dgvFunds.Rows.Clear();

            for (var i = 0; i < funds.Count;)
            {
                var o = funds[i];

                if (dealTime)
                {
                    //牛股涨停
                    if (ngTime && (o.Price == o.UpPrice) && _ngStocks.All(p => p.Code != o.Code))
                    {
                        File.AppendAllText(NgLog,
                            $@"{now:M-d H:m:s} {o.Code} {o.Name} 成交{o.Tm} 量比{o.Cat} {o.Industry} 开{Math.Round((o.StartPrice - o.YesterdayPrice)*100/o.YesterdayPrice, 2)} 振{o.Swing}{Environment.NewLine}");
                        _ngStocks.Add(o);
                        AddTstocks(new List<string> {o.Code}, true);
                    }

                    //去掉停牌和一字板个股，这些不在关注范围
                    if (double.IsNaN(o.Hunter) || ((o.Swing == 0) && (_inflow != _inf)))
                    {
                        funds.RemoveAt(i);
                        _tstocks.Remove(o);
                        continue;
                    }
                }

                dgvFunds.Rows.Add(o.Industry, o.Code, o.Name, o.Price, o.Rate, o.StartPrice, o.MinPrice,
                    o.MaxPrice, o.YesterdayPrice, o.Cat, o.Tr, o.Swing, o.Hunter, o.Tm, o.Ape,
                    o.ChooseCount);

                i++;
            }

            for (var i = 0; i < dgvFunds.Rows.Count; i++)
            {
                if (funds[i].Price > funds[i].YesterdayPrice)
                    dgvFunds.Rows[i].Cells[3].Style.BackColor = Color.Red;
                else if ((funds[i].Price < funds[i].YesterdayPrice) && (funds[i].Price > 0))
                    dgvFunds.Rows[i].Cells[3].Style.BackColor = Color.Green;
                else
                    dgvFunds.Rows[i].Cells[3].Style.BackColor = Color.Gray;

                if (funds[i].Rate > 0)
                    dgvFunds.Rows[i].Cells[4].Style.BackColor = Color.Red;
                else if (funds[i].Rate < 0)
                    dgvFunds.Rows[i].Cells[4].Style.BackColor = Color.Green;
                else
                    dgvFunds.Rows[i].Cells[4].Style.BackColor = Color.Gray;

                if (funds[i].StartPrice > funds[i].YesterdayPrice)
                    dgvFunds.Rows[i].Cells[5].Style.BackColor = Color.Red;
                else if ((funds[i].StartPrice < funds[i].YesterdayPrice) && (funds[i].StartPrice > 0))
                    dgvFunds.Rows[i].Cells[5].Style.BackColor = Color.Green;
                else
                    dgvFunds.Rows[i].Cells[5].Style.BackColor = Color.Gray;

                if (funds[i].MinPrice > funds[i].YesterdayPrice)
                    dgvFunds.Rows[i].Cells[6].Style.BackColor = Color.Red;
                else if ((funds[i].MinPrice < funds[i].YesterdayPrice) && (funds[i].MinPrice > 0))
                    dgvFunds.Rows[i].Cells[6].Style.BackColor = Color.Green;
                else
                    dgvFunds.Rows[i].Cells[6].Style.BackColor = Color.Gray;

                if (funds[i].MaxPrice > funds[i].YesterdayPrice)
                    dgvFunds.Rows[i].Cells[7].Style.BackColor = Color.Red;
                else if ((funds[i].MaxPrice < funds[i].YesterdayPrice) && (funds[i].MaxPrice > 0))
                    dgvFunds.Rows[i].Cells[7].Style.BackColor = Color.Green;
                else
                    dgvFunds.Rows[i].Cells[7].Style.BackColor = Color.Gray;
            }

            #endregion

            //交易时间内才执行
            if (dealTime)
            {
                #region 获取行业动态

                //string ihtml = _web.DownloadString(IndustryCommand);
                //var industrys = StringHelper.GetList(ihtml, "label\\\":\\\"", "\\\"");
                //while (industrys.Count == 0)
                //{
                //    ihtml = _web.DownloadString(IndustryCommand);
                //    industrys = StringHelper.GetList(ihtml, "label\\\":\\\"", "\\\"");
                //}
                //var values = StringHelper.GetList(ihtml, "value\\\":\\\"", "\\\"");
                //var ihtml = _web.DownloadString(DfcfIndustry);
                var ihtml = _webUtf8.DownloadString(DfcfIndustry).Split('"');
                //var industrys = StringHelper.GetList(ihtml, "label\\\":\\\"", "\\\"");
                //var values = StringHelper.GetList(ihtml, "value\\\":\\\"", "\\\"");
                //资金流入行业
                var hots = string.Empty;
                //资金流出行业
                var colds = string.Empty;
                //资金总体流入
                float inflow = 0;
                for (var i = 1; i < ihtml.Length; i += 2)
                {
                    var ts = ihtml[i].Split(',');
                    var inmoney = float.Parse(ts[3])/10000;
                    inflow += inmoney;

                    if (inmoney > 2)
                    {
                        var industry = ts[2];
                        hots += $"{inmoney:F1}{industry} ";
                        AddIndustryStocks(industry, inmoney > 8, true);
                    }
                    else if (inmoney < -2)
                    {
                        var industry = ts[2];
                        colds = $"{inmoney:F1}{industry} {colds}";
                    }
                }
                //for (int i = 0; i < industrys.Count; i++)
                //{
                //    var inmoney = float.Parse(values[i]);
                //    inflow += inmoney;

                //    if (inmoney > 2)
                //    {
                //        var industry = StringHelper.ToGB2312(industrys[i]);
                //        hots += $"{inmoney:F1}{industry} ";
                //        AddIndustryStocks(industry, inmoney > 8, true);
                //    }
                //    else if (inmoney < -2)
                //    {
                //        var industry = StringHelper.ToGB2312(industrys[i]);
                //        colds = $"{inmoney:F1}{industry} {colds}";
                //    }
                //}

                _inf = inflow - _inflow;
                _inflow = inflow;
                var infStr = _inf.ToString("F1");

                if (_inf > 0)
                    hots = $"{infStr} {hots}";
                else
                    colds = $"{infStr} {colds}";

                if (inflow > 0)
                    hots = $"{inflow:F1} {hots}";
                else
                    colds = $"{inflow:F1} {colds}";
                lblHot.Text = hots;
                lblCold.Text = colds;
                ihtml = _webUtf8.DownloadString(DfcfConcept).Split('"');
                //资金流入行业
                var hotsConcept = string.Empty;
                //资金流出行业
                var coldsConcept = string.Empty;
                for (var i = 1; i < ihtml.Length; i += 2)
                {
                    var ts = ihtml[i].Split(',');
                    var inmoney = float.Parse(ts[3]) / 10000;

                    if (inmoney > 2)
                    {
                        var industry = ts[2];
                        hotsConcept += $"{inmoney:F1}{industry} ";
                        AddIndustryStocks(industry, inmoney > 8, true);
                    }
                    else if (inmoney < -2)
                    {
                        var industry = ts[2];
                        coldsConcept = $"{inmoney:F1}{industry} {coldsConcept}";
                    }
                    lblHot2.Text = hotsConcept;
                    lblCold2.Text = coldsConcept;
                }
                #endregion

                #region 智能提示买卖

                var tip = string.Empty;

                if (_inf != _inflow)
                {
                    if (_inf < -0.5)
                        foreach (var o in _tstocks)
                        {
                            if ((o.Hunter < 0.4) && (Math.Abs(o.Hunter) > 0))
                                tip += o.Rate.ToString("F") + o.Name;
                        }
                    else if (_inf > 0.5)
                        foreach (var o in _tstocks)
                        {
                            if (((o.Price - o.MinPrice)/o.YesterdayPrice < 0.002) || (o.Hunter > 6) || (o.Rate < -5))
                                tip += o.Rate.ToString("F") + o.Name + o.Code + " ";
                        }

                    if (tip != string.Empty)
                    {
                        var ys = Math.Abs((int) (inflow*25.5));

                        if (ys > 255)
                            ys = 255;

                        string pos;

                        if (inflow < -20)
                            pos = "空";
                        else if (inflow < -10)
                            pos = "一";
                        else if (inflow < 0)
                            pos = "二";
                        else if (inflow < 10)
                            pos = "三";
                        else if (inflow < 20)
                            pos = "四";
                        else if (inflow < 30)
                            pos = "五";
                        else if (inflow < 40)
                            pos = "六";
                        else
                            pos = "全";

                        var foreColor = _inf > 0 ? Color.FromArgb(ys, 0, 0) : Color.FromArgb(0, ys, 0);
                        var t = _inf > 0 ? "吸" : "抛";
                        _tip.ShowTip($"{pos}{infStr}{t}：{tip}", foreColor);
                    }
                }

                #endregion

                #region 添加涨停个股的所属行业的个股

                foreach (var o in funds)
                    if (o.Rate > 9.5)
                        foreach (var industryStock in _industryStocks)
                            if (industryStock.Value.Contains(o.Code))
                                AddIndustryStocks(industryStock.Key, true, false);

                #endregion
            }
        }

        /// <summary>
        ///     初始化股票信息
        /// </summary>
        /// <param name="stocks">股票列表</param>
        /// <returns>返回是否开盘</returns>
        private void InitStocks(IList<Stock> stocks)
        {
            for (var i = 0; i < stocks.Count;)
            {
                var j = i + RequsetStockCount;
                if (j > stocks.Count)
                    j = stocks.Count;
                var url = GetAskStockUrl(stocks, StockInitCommand, i, j);
                var html = _web.DownloadString(url);
                var hq = StringUtil.GetVal(html, "HqData:[", "]}");
                var fs = StringUtil.GetList(hq, "[", "]");

                foreach (var f in fs)
                {
                    var a = f.Split(',');
                    var stock = stocks[i++];
                    stock.Name = a[0].Replace("\"", "");
                    stock.YesterdayPrice = Convert.ToSingle(a[1]);
                    stock.Ape = Convert.ToSingle(a[2]);
                    stock.UpPrice = Convert.ToSingle((stock.YesterdayPrice*1.1).ToString("F"));
                }
            }
        }

        /// <summary>
        ///     添加当前热门行业的热门股票
        /// </summary>
        /// <param name="industry"></param>
        private void AddIndustryStocks(string industry, bool tStock, bool addFile)
        {
            if (_industryStocks.ContainsKey(industry) && !_goodIndustry.Contains(industry))
            {
                _goodIndustry.Add(industry);

                if (tStock)
                    AddTstocks(_industryStocks[industry], false);
                else
                    foreach (var stockCode in _industryStocks[industry])
                        AddStock(stockCode);

                if (addFile)
                    File.AppendAllText(GoodIndustryLog, " " + industry);

                //var bcode = GetBcodeByIndustry(industry);
                var bcode = GetBcodeByIndustryDfcf(industry);
                if ((bcode != null) && !lblIndustry.Text.Contains(bcode))
                    lblIndustry.Text += bcode + " ";
            }
        }

        private void dgvFunds_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == -1) || (e.RowIndex >= _goodStocks.Count))
                return;

            if (e.ColumnIndex == 1)
            {
                Process.Start("360chrome.exe",
                    $"http://quote.eastmoney.com/{dgvFunds.Rows[e.RowIndex].Cells[1].Value}.html");
            }
            else if (e.ColumnIndex == 2)
            {
                var fund = dgvFunds.Rows[e.RowIndex].Cells[1].Value.ToString();
                string market;

                if (fund.StartsWith("6"))
                    market = "1";
                else if (fund.StartsWith("0"))
                    market = "2";
                else
                    market = "3";

                Process.Start("360chrome.exe",
                    $"http://quote.eastmoney.com/f1.html?code={fund}&market={market}");
            }
        }

        private void btnAddFund_Click(object sender, EventArgs e)
        {
            var fundCodes = txtFundCode.Text.Split(' ');

            foreach (var fundCode in fundCodes)
                AddStock(fundCode);

            txtFundCode.Text = "";
        }

        private Stock AddStock(string stockCode)
        {
            var fund = _goodStocks.Find(o => o.Code == stockCode);
            if (fund != null) return fund;
            fund = _thxStocks.Find(o => o.Code == stockCode) ?? new Stock
                   {
                       Code = stockCode
                   };

            fund.Industry = string.Join(" ", _industryStocks.Where(o => o.Value.Contains(stockCode)).Select(o => o.Key));
            _goodStocks.Add(fund);
            return fund;
        }

        /// <summary>
        ///     添加需要做T的股票
        /// </summary>
        /// <param name="tstocks"></param>
        /// <param name="saveFile"></param>
        private void AddTstocks(IEnumerable<string> tstocks, bool saveFile)
        {
            foreach (var stockCode in tstocks)
                if ((stockCode.Length == 6) && _tstocks.All(o => o.Code != stockCode))
                {
                    var stock = AddStock(stockCode);
                    _tstocks.Add(stock);
                }

            if (saveFile)
                File.WriteAllText(TStock, string.Join(" ", _tstocks.Select(o => o.Code)));
        }

        private void btnT_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFundCode.Text))
                return;
            var stockCodes = txtFundCode.Text.Split(' ');
            txtFundCode.Text = "";
            AddTstocks(stockCodes, true);
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (btnStop.Text == "暂  停")
            {
                btnStop.Text = "继  续";
                _timer.Stop();
            }
            else
            {
                _tip.Hide();
                btnStop.Text = "暂  停";
                _timer.Start();
            }
        }

        private void txtFundCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                btnAddFund_Click(null, null);
        }

        public string GetBcodeByIndustry(string industry)
        {
            switch (industry)
            {
                case "有色冶炼加工":
                case "黄金":
                    return "有色197";
                case "证券":
                    return "证券201";
                case "计算机应用":
                    return "互联网195";
                case "饮料制造":
                case "白酒":
                    return "白酒270";
                case "传媒":
                    return "传媒248";
                case "房地产开发":
                    return "房地产118";
                case "煤炭开采":
                    return "煤炭290";
                case "钢铁":
                    return "钢铁288";
                case "国防军工":
                    return "国防206";
                case "汽车整车":
                case "无人驾驶":
                case "汽车零部件":
                case "公交":
                    return "新能车212";
                case "充电桩":
                case "锂电池":
                    return "新能源218";
                case "医药商业":
                case "医疗器械":
                case "医疗器械服务":
                case "化学制药":
                case "基因测序":
                    return "医疗262";
                case "银行":
                    return "银行228";
                case "央企国资改革":
                case "上海国资改革":
                case "深圳国资改革":
                    return "改革296";
                case "食品加工制造":
                    return "食品199";
                case "保险及其他":
                    return "保险330";
                case "体育产业":
                    return "体育308";
                case "建筑装饰":
                case "一带一路":
                    return "一带一276";
                case "环保工程":
                case "节能环保":
                    return "环保185";
                case "生物制品":
                    return "生物258";
                default:
                    return null;
            }
        }

        public string GetBcodeByIndustryDfcf(string industry)
        {
            switch (industry)
            {
                case "有色金属":
                case "黄金概念":
                case "小金属":
                case "基本金属":
                    return "有色197";
                case "券商信托":
                case "券商概念":
                    return "证券201";
                case "软件服务":
                case "互联网":
                case "国产软件":
                    return "互联网195";
                case "酿酒行业":
                    return "白酒270";
                case "文化传媒":
                    return "传媒248";
                case "房地产":
                    return "房地产118";
                case "煤炭采选":
                    return "煤炭290";
                case "钢铁行业":
                    return "钢铁288";
                case "军工":
                    return "国防206";
                case "汽车行业":
                case "无人驾驶":
                case "新能源车":
                    return "新能车212";
                case "充电桩":
                case "锂电池":
                case "新能源":
                    return "新能源218";
                case "医疗器械":
                case "医疗行业":
                    return "医疗262";
                case "银行":
                    return "银行228";
                case "国企改革":
                    return "改革296";
                case "食品饮料":
                    return "食品199";
                case "保险":
                    return "保险330";
                case "体育产业":
                    return "体育308";
                case "装修装饰":
                case "一带一路":
                case "中字头":
                    return "一带一276";
                case "环保工程":
                case "节能环保":
                    return "环保185";
                //case "生物制品":
                //    return "生物258";
                default:
                    return null;
            }
        }

        #region 常量变量

        #region 常量

        //获取股票最新数据的接口，后接股票代码，每个股票用逗号隔开;op开盘价np最新价hp最高价lp最低价tm成交额cat量比tr换手率
        private const string StockCommand = "http://q.jrjimg.cn/?q=cn|s&n=stockHQ&c=op,np,hp,lp,tm,cat,tr&i=";
        //获取股票最新数据的接口，后接股票代码，每个股票用逗号隔开;name股票名称lcp昨收ape市盈率
        private const string StockInitCommand = "http://q.jrjimg.cn/?q=cn|s&n=stockHQ&c=name,lcp,ape&i=";
        //获取同花顺推荐的股票
        private const string RecommendUrl = "http://comment.10jqka.com.cn/znxg/formula_stocks_pc.json";
        //获取行业资金流向
        //private const string IndustryCommand = "http://q.10jqka.com.cn/thshy/index/field/zjjlr/order/desc/page/1/ajax/1/";
        //获取东方财富行业资金流向
        private const string DfcfIndustry =
            "http://nufm.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd=C._BKHY&sty=DCFFPBFM&st=(BalFlowMain)&sr=-1&ps=999&token=894050c76af8597a853f5b408b759f5d";

        //获取东方财富概念资金流向
        private const string DfcfConcept =
            "http://nufm.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd=C._BKGN&sty=DCFFPBFM&st=(BalFlowMain)&sr=-1&ps=10000&token=894050c76af8597a853f5b408b759f5d";

        //http://nufm2.dfcfw.com/EM_Finance2014NumericApplication/JS.aspx?type=CT&cmd=0000011,3990012,3990062&sty=E1FD&st=z&token=afb2abbc6e10eb3682146dfec6a6d74c
        //每次请求的股票个数
        private const int RequsetStockCount = 50;
        //存放资金流入行业日志的文件
        private const string GoodIndustryLog = "File\\industrylog.txt";
        //存放牛股日志的文件
        private const string NgLog = "File\\stocklog.txt";
        //行业+该行业牛股文件
        private const string GoodStock = "File\\stock.txt";
        //需要做T的股票的文件
        private const string TStock = "File\\tstock.txt";
        ////存放买了的股票的文件
        //private const string BuyStock = "File\\buy.txt";
        ////存放买了的股票的日志文件
        //private const string BuyLog = "File\\buylog.txt";

        #endregion

        //热门行业概念
        private readonly List<string> _goodIndustry = new List<string>();
        //当前界面显示的个股
        private readonly List<Stock> _goodStocks = new List<Stock>();
        //牛股（10点前涨停的个股）
        private readonly List<Stock> _ngStocks = new List<Stock>();
        ////买了的股票
        //private readonly List<Stock> _buyStocks = new List<Stock>();
        //热门行业概念和对应个股的键值对
        private readonly Dictionary<string, List<string>> _industryStocks = new Dictionary<string, List<string>>();
        //同花顺推荐的个股
        private readonly List<Stock> _thxStocks = new List<Stock>();
        private readonly Timer _timer = new Timer();
        private readonly Tip _tip = new Tip();
        //需要做T的个股
        private readonly List<Stock> _tstocks = new List<Stock>();
        private readonly WebClient _web = new WebClient();
        private readonly WebClient _webUtf8 = new WebClient();
        private readonly StringBuilder _sb = new StringBuilder();
        //总流入资金
        private float _inflow;
        //近1分钟流入资金
        private float _inf;
        //竞价时间
        private readonly DateTime _jjTime;
        //开盘时间
        private readonly DateTime _kpTime;
        //上午收盘时间
        private readonly DateTime _zspTime;
        //下午开盘时间
        private readonly DateTime _zkpTime;
        //收盘时间
        private readonly DateTime _spTime;
        //牛股结束涨停时间
        private readonly DateTime _ngTime;

        #endregion
    }
}