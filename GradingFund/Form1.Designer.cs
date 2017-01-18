namespace GradingFund
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lblCold = new System.Windows.Forms.Label();
            this.lblHot = new System.Windows.Forms.Label();
            this.lblCold2 = new System.Windows.Forms.Label();
            this.lblBuy = new System.Windows.Forms.Label();
            this.lblHot2 = new System.Windows.Forms.Label();
            this.dgvFunds = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AvgRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Speed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LeaderRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaxSpeed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunds)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCold
            // 
            this.lblCold.AutoSize = true;
            this.lblCold.ForeColor = System.Drawing.Color.Green;
            this.lblCold.Location = new System.Drawing.Point(1, 27);
            this.lblCold.Name = "lblCold";
            this.lblCold.Size = new System.Drawing.Size(0, 12);
            this.lblCold.TabIndex = 20;
            // 
            // lblHot
            // 
            this.lblHot.AutoSize = true;
            this.lblHot.ForeColor = System.Drawing.Color.Red;
            this.lblHot.Location = new System.Drawing.Point(1, 46);
            this.lblHot.Name = "lblHot";
            this.lblHot.Size = new System.Drawing.Size(0, 12);
            this.lblHot.TabIndex = 18;
            // 
            // lblCold2
            // 
            this.lblCold2.AutoSize = true;
            this.lblCold2.ForeColor = System.Drawing.Color.Green;
            this.lblCold2.Location = new System.Drawing.Point(1, 65);
            this.lblCold2.Name = "lblCold2";
            this.lblCold2.Size = new System.Drawing.Size(0, 12);
            this.lblCold2.TabIndex = 21;
            // 
            // lblBuy
            // 
            this.lblBuy.AutoSize = true;
            this.lblBuy.Location = new System.Drawing.Point(1, 7);
            this.lblBuy.Name = "lblBuy";
            this.lblBuy.Size = new System.Drawing.Size(0, 12);
            this.lblBuy.TabIndex = 17;
            // 
            // lblHot2
            // 
            this.lblHot2.AutoSize = true;
            this.lblHot2.ForeColor = System.Drawing.Color.Red;
            this.lblHot2.Location = new System.Drawing.Point(1, 84);
            this.lblHot2.Name = "lblHot2";
            this.lblHot2.Size = new System.Drawing.Size(0, 12);
            this.lblHot2.TabIndex = 19;
            // 
            // dgvFunds
            // 
            this.dgvFunds.AllowUserToAddRows = false;
            this.dgvFunds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvFunds.CausesValidation = false;
            this.dgvFunds.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFunds.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4,
            this.Rate,
            this.AvgRate,
            this.Speed,
            this.Column16,
            this.Column14,
            this.LeaderRate,
            this.Column11,
            this.MaxSpeed,
            this.Column7,
            this.Column6,
            this.Column3,
            this.Column8,
            this.Column9});
            this.dgvFunds.Location = new System.Drawing.Point(0, 105);
            this.dgvFunds.Name = "dgvFunds";
            this.dgvFunds.ReadOnly = true;
            this.dgvFunds.RowTemplate.Height = 23;
            this.dgvFunds.ShowCellErrors = false;
            this.dgvFunds.ShowCellToolTips = false;
            this.dgvFunds.ShowEditingIcon = false;
            this.dgvFunds.ShowRowErrors = false;
            this.dgvFunds.Size = new System.Drawing.Size(1047, 240);
            this.dgvFunds.TabIndex = 16;
            this.dgvFunds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFunds_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "分级代码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column2.HeaderText = "分级名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "最新价";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Rate
            // 
            this.Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate.HeaderText = "涨幅";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // AvgRate
            // 
            this.AvgRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.AvgRate.FillWeight = 104.796F;
            this.AvgRate.HeaderText = "平均涨幅";
            this.AvgRate.Name = "AvgRate";
            this.AvgRate.ReadOnly = true;
            // 
            // Speed
            // 
            this.Speed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Speed.HeaderText = "涨速";
            this.Speed.Name = "Speed";
            this.Speed.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column16.FillWeight = 104.796F;
            this.Column16.HeaderText = "红率";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column14.FillWeight = 104.796F;
            this.Column14.HeaderText = "龙头";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // LeaderRate
            // 
            this.LeaderRate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LeaderRate.FillWeight = 104.796F;
            this.LeaderRate.HeaderText = "龙头涨幅";
            this.LeaderRate.Name = "LeaderRate";
            this.LeaderRate.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column11.HeaderText = "急涨";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // MaxSpeed
            // 
            this.MaxSpeed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaxSpeed.HeaderText = "急涨涨速";
            this.MaxSpeed.Name = "MaxSpeed";
            this.MaxSpeed.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.HeaderText = "今开";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.FillWeight = 104.796F;
            this.Column6.HeaderText = "最高价";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "最低价";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.FillWeight = 104.796F;
            this.Column8.HeaderText = "昨收";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.FillWeight = 104.796F;
            this.Column9.HeaderText = "振幅";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 345);
            this.Controls.Add(this.lblCold);
            this.Controls.Add(this.lblHot);
            this.Controls.Add(this.lblCold2);
            this.Controls.Add(this.lblBuy);
            this.Controls.Add(this.lblHot2);
            this.Controls.Add(this.dgvFunds);
            this.Name = "Form1";
            this.Text = "分级基金小助手";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCold;
        private System.Windows.Forms.Label lblHot;
        private System.Windows.Forms.Label lblCold2;
        private System.Windows.Forms.Label lblBuy;
        private System.Windows.Forms.Label lblHot2;
        private System.Windows.Forms.DataGridView dgvFunds;
        private System.Windows.Forms.DataGridViewLinkColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AvgRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Speed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn LeaderRate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaxSpeed;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
    }
}

