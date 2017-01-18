using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace GoodStock
{
    public partial class Tip : Form
    {
        private const string Log = "File\\log.txt";
        private readonly Timer _timer = new Timer();

        public Tip()
        {
            InitializeComponent();
            _timer.Interval = 10000;
            _timer.Tick += timer_Tick;
        }

        /// <summary>
        /// </summary>
        /// <param name="tip">提示信息</param>
        /// <param name="foreColor">提示文字颜色</param>
        /// <param name="addLog">是否添加日志</param>
        public void ShowTip(string tip, Color foreColor, bool addLog = true)
        {
            lblTip.Text = tip;
            lblTip.ForeColor = foreColor;
            Width = lblTip.Width;
            _timer.Start();
            Show();
            if (addLog)
                File.AppendAllText(Log, $@"{Environment.NewLine}{DateTime.Now:M-d H:m:s}：{tip}");
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            _timer.Stop();
            Hide();
        }

        private void lblTip_Click(object sender, EventArgs e)
        {
            timer_Tick(null, null);
        }
    }
}