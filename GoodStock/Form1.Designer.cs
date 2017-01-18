namespace GoodStock
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvFunds = new System.Windows.Forms.DataGridView();
            this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnT = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnAddFund = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.txtFundCode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblIndustry = new System.Windows.Forms.Label();
            this.lblHot = new System.Windows.Forms.Label();
            this.lblCold = new System.Windows.Forms.Label();
            this.lblHot2 = new System.Windows.Forms.Label();
            this.lblCold2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunds)).BeginInit();
            this.SuspendLayout();
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
            this.Column12,
            this.Column1,
            this.Column2,
            this.Column4,
            this.Column5,
            this.Column7,
            this.Column3,
            this.Column6,
            this.Column8,
            this.Column13,
            this.Column14,
            this.Column9,
            this.Column11,
            this.Column15,
            this.Column16,
            this.Column10});
            this.dgvFunds.Location = new System.Drawing.Point(-1, 103);
            this.dgvFunds.Name = "dgvFunds";
            this.dgvFunds.ReadOnly = true;
            this.dgvFunds.RowTemplate.Height = 23;
            this.dgvFunds.ShowCellErrors = false;
            this.dgvFunds.ShowCellToolTips = false;
            this.dgvFunds.ShowEditingIcon = false;
            this.dgvFunds.ShowRowErrors = false;
            this.dgvFunds.Size = new System.Drawing.Size(1047, 201);
            this.dgvFunds.TabIndex = 5;
            this.dgvFunds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFunds_CellContentClick);
            // 
            // Column12
            // 
            this.Column12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column12.HeaderText = "行业";
            this.Column12.Name = "Column12";
            this.Column12.ReadOnly = true;
            this.Column12.Width = 150;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "股票代号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Column2.HeaderText = "股票名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 70;
            // 
            // Column4
            // 
            this.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column4.HeaderText = "最新价";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "涨幅";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column7
            // 
            this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column7.HeaderText = "今开";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "最低价";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column6
            // 
            this.Column6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column6.HeaderText = "最高价";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column8.HeaderText = "昨收";
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            // 
            // Column13
            // 
            this.Column13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column13.HeaderText = "量比";
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            // 
            // Column14
            // 
            this.Column14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column14.HeaderText = "换手率";
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column9.HeaderText = "振幅";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column11.HeaderText = "抄底";
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            // 
            // Column15
            // 
            this.Column15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column15.HeaderText = "成交额";
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column16.HeaderText = "市盈率";
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column10.HeaderText = "推荐个数";
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            // 
            // btnT
            // 
            this.btnT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnT.Location = new System.Drawing.Point(362, 309);
            this.btnT.Name = "btnT";
            this.btnT.Size = new System.Drawing.Size(94, 32);
            this.btnT.TabIndex = 7;
            this.btnT.Text = "做T";
            this.btnT.UseVisualStyleBackColor = true;
            this.btnT.Click += new System.EventHandler(this.btnT_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(939, 309);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(94, 32);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "刷  新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // btnAddFund
            // 
            this.btnAddFund.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddFund.Location = new System.Drawing.Point(262, 309);
            this.btnAddFund.Name = "btnAddFund";
            this.btnAddFund.Size = new System.Drawing.Size(94, 32);
            this.btnAddFund.TabIndex = 9;
            this.btnAddFund.Text = "添加股票";
            this.btnAddFund.UseVisualStyleBackColor = true;
            this.btnAddFund.Click += new System.EventHandler(this.btnAddFund_Click);
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(839, 309);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(94, 32);
            this.btnStop.TabIndex = 10;
            this.btnStop.Text = "暂  停";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // txtFundCode
            // 
            this.txtFundCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtFundCode.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFundCode.Location = new System.Drawing.Point(108, 310);
            this.txtFundCode.Name = "txtFundCode";
            this.txtFundCode.Size = new System.Drawing.Size(148, 30);
            this.txtFundCode.TabIndex = 12;
            this.txtFundCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtFundCode_KeyDown);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(7, 315);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 11;
            this.label1.Text = "股票代码：";
            // 
            // lblIndustry
            // 
            this.lblIndustry.AutoSize = true;
            this.lblIndustry.Location = new System.Drawing.Point(3, 0);
            this.lblIndustry.Name = "lblIndustry";
            this.lblIndustry.Size = new System.Drawing.Size(0, 12);
            this.lblIndustry.TabIndex = 13;
            // 
            // lblHot
            // 
            this.lblHot.AutoSize = true;
            this.lblHot.ForeColor = System.Drawing.Color.Red;
            this.lblHot.Location = new System.Drawing.Point(3, 39);
            this.lblHot.Name = "lblHot";
            this.lblHot.Size = new System.Drawing.Size(0, 12);
            this.lblHot.TabIndex = 14;
            // 
            // lblCold
            // 
            this.lblCold.AutoSize = true;
            this.lblCold.ForeColor = System.Drawing.Color.Green;
            this.lblCold.Location = new System.Drawing.Point(3, 20);
            this.lblCold.Name = "lblCold";
            this.lblCold.Size = new System.Drawing.Size(0, 12);
            this.lblCold.TabIndex = 15;
            // 
            // lblHot2
            // 
            this.lblHot2.AutoSize = true;
            this.lblHot2.ForeColor = System.Drawing.Color.Red;
            this.lblHot2.Location = new System.Drawing.Point(3, 77);
            this.lblHot2.Name = "lblHot2";
            this.lblHot2.Size = new System.Drawing.Size(0, 12);
            this.lblHot2.TabIndex = 14;
            // 
            // lblCold2
            // 
            this.lblCold2.AutoSize = true;
            this.lblCold2.ForeColor = System.Drawing.Color.Green;
            this.lblCold2.Location = new System.Drawing.Point(3, 58);
            this.lblCold2.Name = "lblCold2";
            this.lblCold2.Size = new System.Drawing.Size(0, 12);
            this.lblCold2.TabIndex = 15;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(1046, 345);
            this.Controls.Add(this.lblCold);
            this.Controls.Add(this.lblHot);
            this.Controls.Add(this.lblCold2);
            this.Controls.Add(this.lblHot2);
            this.Controls.Add(this.lblIndustry);
            this.Controls.Add(this.txtFundCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnAddFund);
            this.Controls.Add(this.dgvFunds);
            this.Controls.Add(this.btnT);
            this.Controls.Add(this.btnRefresh);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "选股小助手";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFunds;
        private System.Windows.Forms.Button btnT;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnAddFund;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.TextBox txtFundCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblIndustry;
        private System.Windows.Forms.Label lblHot;
        private System.Windows.Forms.Label lblCold;
        private System.Windows.Forms.Label lblHot2;
        private System.Windows.Forms.Label lblCold2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column12;
        private System.Windows.Forms.DataGridViewLinkColumn Column1;
        private System.Windows.Forms.DataGridViewLinkColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column13;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column14;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column15;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column16;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
    }
}

