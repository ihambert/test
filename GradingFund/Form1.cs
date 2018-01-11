using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Common;

namespace GradingFund
{
    public partial class Form1 : Form
    {
        public delegate void MyDelegate(GradingFundData data);

        private readonly Tip _tip = new Tip();

        public MyDelegate Updated;

        public Form1()
        {
            InitializeComponent();
            //测试：记录当前线程ID
            Logger.Debug($"主线程ID：{Thread.CurrentThread.ManagedThreadId}");
            Updated = Refresh;
            var util = new GradingFundUtil();
            util.Updated += Util_Updated;
            util.Start();
        }

        private void Util_Updated(GradingFundData data)
        {
            //测试：记录当前线程ID
            Logger.Debug($"Util_Updated线程ID：{Thread.CurrentThread.ManagedThreadId}");
            BeginInvoke(Updated, data);
            //Log.Debug("BeginInvoke");
            //Invoke(Updated, data);
            //Log.Debug("Invoke");
        }

        private void Refresh(GradingFundData data)
        {
            //测试：记录当前线程ID
            Logger.Debug($"Refresh线程ID：{Thread.CurrentThread.ManagedThreadId}");
            var gfs = data.GradingFunds.OrderByDescending(o => o.AvgRate).ToList();
            string tip;
            if (data.Inf > 0.5)
            {
                //大盘上涨时按平均涨幅倒序找出3个被低估的分级（此种是理想的买入区间，但最好在开头部分买入，否则可能追高）
                tip = string.Join(" ", gfs.Where(o => o.AvgRate > o.Rate).Take(3).Select(o => o.Rate + "^" + o.Speed + "^" + o.MinPrice + o.Name + o.Code));
                tip = $"{data.Inflow}吸{data.Inf} {tip}";
                if (data.IsOpen)
                {
                    _tip.ShowTip(tip, Color.Red);
                }
            }
            else if (data.Inf < -0.5)
            {
                //大盘下跌时提示逆势的3个分级，很可能是突围的分级（此种情况谨慎抄底，最好抓住下跌末尾抄进去，否则很危险，谨慎者不在此区间买入）
                tip = string.Join(" ",
                    data.GradingFunds.Where(o => o.Speed > 0.1).OrderByDescending(o => o.Speed).Take(3).Select(o => o.Rate + "^" + o.Speed + "^" + o.MinPrice + o.Name + o.Code));
                tip = $"{data.Inflow}抛{data.Inf} {tip}";
                if (data.IsOpen)
                {
                    _tip.ShowTip(tip, Color.Green);
                }
            }
            else if (data.Stocks.Max(o => o.Speed > 1))
            {
                //大盘无大波动的情况下找出异动的个股对应的分级，可能是突围的分级（此种情况食之无味，弃之可惜）
                tip = string.Join(" ", data.GradingFunds.OrderByDescending(o => o.MaxSpeed.Speed).Take(3).Select(o => o.Rate + "^" + o.Speed + "^" + o.MinPrice + o.Name + o.Code + o.MaxSpeed.Name + o.MaxSpeed.Speed));
                tip = $"{data.Inflow}抄{data.Inf} {tip}";
                if (data.IsOpen)
                {
                    _tip.ShowTip(tip, GetColor(data.Inf));
                }
            }
            else
            {
                //大盘无大波动的情况下只找3个被低估的分级按照涨速倒序（此种情况不建议买入）
                tip = string.Join(" ",
                data.GradingFunds.Where(o => o.AvgRate > o.Rate && o.Speed > 0).OrderByDescending(o => o.Speed).Take(3).Select(o => o.Rate + "^" + o.Speed + "^" + o.MinPrice + o.Name + o.Code));
                tip = $"{data.Inflow} {data.Inf} {tip}";
            }

            lblBuy.Text = tip;
            lblHot.Text = string.Join(" ",
                data.IndustryFlow.Where(o => o.Inflow > 0).OrderByDescending(o => o.Inflow).Select(o => o.Inflow + o.Name));
            lblCold.Text = string.Join(" ",
                data.IndustryFlow.Where(o => o.Inflow < 0).OrderBy(o => o.Inflow).Select(o => o.Inflow + o.Name));
            lblHot2.Text = string.Join(" ",
                data.ConceptFlow.Where(o => o.Inflow > 0).OrderByDescending(o => o.Inflow).Select(o => o.Inflow + o.Name));
            lblCold2.Text = string.Join(" ",
                data.ConceptFlow.Where(o => o.Inflow < 0).OrderBy(o => o.Inflow).Select(o => o.Inflow + o.Name));
            dgvFunds.Rows.Clear();

            for (var i = 0; i < gfs.Count; i++)
            {
                var f = gfs[i];
                dgvFunds.Rows.Add(f.Code, f.Name, f.Price, f.Rate, f.AvgRate, f.Speed, f.RedRate, f.Leader.Name, f.Leader.Rate, f.MaxSpeed.Name, f.MaxSpeed.Speed, f.StartPrice, f.MaxPrice, f.MinPrice, f.YesterdayPrice, f.Swing, f.BaseFund);
                dgvFunds.Rows[i].Cells["Rate"].Style.BackColor = GetColor(f.Rate);
                dgvFunds.Rows[i].Cells["AvgRate"].Style.BackColor = GetColor(f.AvgRate);
                dgvFunds.Rows[i].Cells["Speed"].Style.BackColor = GetColor(f.Speed);
                dgvFunds.Rows[i].Cells["LeaderRate"].Style.BackColor = GetColor(f.Leader.Rate);
                dgvFunds.Rows[i].Cells["MaxSpeed"].Style.BackColor = GetColor(f.MaxSpeed.Speed);
            }
            //测试：记录当前线程ID
            Logger.Debug("Refresh更新完毕");
        }

        /// <summary>
        /// 根据数字获取对应的颜色，大于0为红色，小于0为绿色，等于0为灰色
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Color GetColor(double value)
        {
            if (value > 0)
            {
                return Color.Red;
            }

            return value < 0 ? Color.Green : Color.Gray;
        }

        private void dgvFunds_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex != -1)
            {
                Process.Start($"http://fund.eastmoney.com/{dgvFunds.Rows[e.RowIndex].Cells[0].Value}.html");
            }
        }
    }
}