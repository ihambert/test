using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using Common;

namespace Fund
{
    public partial class Form1 : Form
    {
        #region Init

        public delegate void MyDelegate();

        public MyDelegate Updated;

        private FundData _fundData;

        private int _type = 1;

        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //测试：记录当前线程ID
            Logger.Debug($"主线程ID：{Thread.CurrentThread.ManagedThreadId}");
            Updated = RefreshData;
            var util = new FundUtil();
            _fundData = util.FundData;
            util.Updated += Util_Updated;
            util.Start();
        }

        private void Util_Updated()
        {
            //测试：记录当前线程ID
            Logger.Debug($"Util_Updated线程ID：{Thread.CurrentThread.ManagedThreadId}");
            BeginInvoke(Updated);
        }

        #endregion

        private void RefreshData()
        {
            //测试：记录当前线程ID
            Logger.Debug($"Refresh线程ID：{Thread.CurrentThread.ManagedThreadId}");
            var funds = _type == 0 ? _fundData.Funds : _fundData.Funds.FindAll(o => o.Type == _type);
            dgvFunds.Rows.Clear();

            for (var i = 0; i < funds.Count; i++)
            {
                var f = funds[i];
                dgvFunds.Rows.Add(f.Code, f.Name, f.Rate, f.Rates[0], f.Rate2, f.Rate3, f.Rate5, f.Rate7, f.Rate30, f.Rate60, f.Rate90, f.Fee, f.Fee7);
                var row = dgvFunds.Rows[i];
                row.Cells["Rate"].Style.BackColor = GetColor(f.Rate);
                row.Cells["Rate1"].Style.BackColor = GetColor(f.Rates[0]);
                row.Cells["Rate2"].Style.BackColor = GetColor(f.Rate2);
                row.Cells["Rate3"].Style.BackColor = GetColor(f.Rate3);
                row.Cells["Rate5"].Style.BackColor = GetColor(f.Rate5);
                row.Cells["Rate7"].Style.BackColor = GetColor(f.Rate7);
                row.Cells["Rate30"].Style.BackColor = GetColor(f.Rate30);
                row.Cells["Rate60"].Style.BackColor = GetColor(f.Rate60);
                row.Cells["Rate90"].Style.BackColor = GetColor(f.Rate90);
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
            if (e.RowIndex == -1 || e.RowIndex >= dgvFunds.RowCount)
                return;

            if (e.ColumnIndex == 0)
            {
                Process.Start($"http://fund.eastmoney.com/{dgvFunds.Rows[e.RowIndex].Cells[0].Value}.html");
            }
            else if (e.ColumnIndex == 1)
            {
                Process.Start($"http://www.howbuy.com/fund/{dgvFunds.Rows[e.RowIndex].Cells[0].Value}/");
            }
        }

        private void rbtChinese_CheckedChanged(object sender, System.EventArgs e)
        {
            _type = 1;
            RefreshData();
        }

        private void rbtBond_CheckedChanged(object sender, System.EventArgs e)
        {
            _type = 2;
            RefreshData();
        }

        private void rbtOther_CheckedChanged(object sender, System.EventArgs e)
        {
            _type = 3;
            RefreshData();
        }

        private void dgvFunds_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 || e.RowIndex >= dgvFunds.RowCount)
                return;
            Process.Start("https://trade5.1234567.com.cn/fundtradepage/default2?fc=" + dgvFunds.Rows[e.RowIndex].Cells[0].Value);
        }

        private void btnSell_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://trade5.1234567.com.cn/FundtradePage/redemption?spm=S");
        }

        private void rbtAll_CheckedChanged(object sender, System.EventArgs e)
        {
            _type = 0;
            RefreshData();
        }

        private void btnFavor_Click(object sender, System.EventArgs e)
        {
            Process.Start("http://fund.eastmoney.com/favor.html");
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            Process.Start("https://query.1234567.com.cn/Query/Index#t_0;bt_0;s_5;ft_0");
        }
    }
}