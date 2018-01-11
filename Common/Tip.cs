using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common
{
    public partial class Tip : Form
    {
        private readonly Timer _timer = new Timer();
        //当前的屏幕除任务栏外的工作域的宽度
        private readonly int _width;
        //当前的屏幕除任务栏外的工作域的高度
        private readonly int _height;

        public Tip()
        {
            InitializeComponent();
            _width = Screen.PrimaryScreen.WorkingArea.Width;
            _height = Screen.PrimaryScreen.WorkingArea.Height;
            _timer.Interval = 6000;
            _timer.Tick += timer_Tick;
        }

        /// <summary>
        ///     弹出提示框，6秒钟后自动关闭
        /// </summary>
        /// <param name="tip">提示信息</param>
        /// <param name="foreColor">提示文字颜色</param>
        public void ShowTip(string tip, Color foreColor)
        {
            Logger.Info(tip);
            lblTip.Text = tip;
            lblTip.ForeColor = foreColor;
            Width = lblTip.Width > _width ? _width : lblTip.Width;
            Location = new Point((_width - Width) /2, (_height - lblTip.Height)/2);
            if (Visible) return;
            Show();
            _timer.Start();
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