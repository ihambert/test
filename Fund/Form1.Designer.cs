namespace Fund
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
            this.dgvFunds = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Rate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate30 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate60 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rate90 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rbtAll = new System.Windows.Forms.RadioButton();
            this.rbtChinese = new System.Windows.Forms.RadioButton();
            this.rbtBond = new System.Windows.Forms.RadioButton();
            this.rbtOther = new System.Windows.Forms.RadioButton();
            this.btnSell = new System.Windows.Forms.Button();
            this.btnFavor = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.Column1,
            this.Column2,
            this.Rate,
            this.Rate1,
            this.Rate2,
            this.Rate3,
            this.Rate5,
            this.Rate7,
            this.Rate30,
            this.Rate60,
            this.Rate90,
            this.Column3,
            this.Column5});
            this.dgvFunds.Location = new System.Drawing.Point(0, 44);
            this.dgvFunds.Name = "dgvFunds";
            this.dgvFunds.ReadOnly = true;
            this.dgvFunds.RowTemplate.Height = 23;
            this.dgvFunds.ShowCellErrors = false;
            this.dgvFunds.ShowCellToolTips = false;
            this.dgvFunds.ShowEditingIcon = false;
            this.dgvFunds.ShowRowErrors = false;
            this.dgvFunds.Size = new System.Drawing.Size(1047, 417);
            this.dgvFunds.TabIndex = 16;
            this.dgvFunds.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFunds_CellContentClick);
            this.dgvFunds.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvFunds_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "基金代码";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "基金名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 165;
            // 
            // Rate
            // 
            this.Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate.HeaderText = "估算涨幅";
            this.Rate.Name = "Rate";
            this.Rate.ReadOnly = true;
            // 
            // Rate1
            // 
            this.Rate1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate1.HeaderText = "昨日涨幅";
            this.Rate1.Name = "Rate1";
            this.Rate1.ReadOnly = true;
            // 
            // Rate2
            // 
            this.Rate2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate2.FillWeight = 104.796F;
            this.Rate2.HeaderText = "2天涨幅";
            this.Rate2.Name = "Rate2";
            this.Rate2.ReadOnly = true;
            // 
            // Rate3
            // 
            this.Rate3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate3.HeaderText = "3日涨幅";
            this.Rate3.Name = "Rate3";
            this.Rate3.ReadOnly = true;
            // 
            // Rate5
            // 
            this.Rate5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate5.FillWeight = 104.796F;
            this.Rate5.HeaderText = "5日涨幅";
            this.Rate5.Name = "Rate5";
            this.Rate5.ReadOnly = true;
            // 
            // Rate7
            // 
            this.Rate7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate7.FillWeight = 104.796F;
            this.Rate7.HeaderText = "1周涨幅";
            this.Rate7.Name = "Rate7";
            this.Rate7.ReadOnly = true;
            // 
            // Rate30
            // 
            this.Rate30.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate30.FillWeight = 104.796F;
            this.Rate30.HeaderText = "1月涨幅";
            this.Rate30.Name = "Rate30";
            this.Rate30.ReadOnly = true;
            // 
            // Rate60
            // 
            this.Rate60.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate60.HeaderText = "2月涨幅";
            this.Rate60.Name = "Rate60";
            this.Rate60.ReadOnly = true;
            // 
            // Rate90
            // 
            this.Rate90.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Rate90.HeaderText = "3月涨幅";
            this.Rate90.Name = "Rate90";
            this.Rate90.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.HeaderText = "手续费";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // Column5
            // 
            this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column5.HeaderText = "7天后手续费";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // rbtAll
            // 
            this.rbtAll.AutoSize = true;
            this.rbtAll.Font = new System.Drawing.Font("宋体", 16F);
            this.rbtAll.Location = new System.Drawing.Point(12, 12);
            this.rbtAll.Name = "rbtAll";
            this.rbtAll.Size = new System.Drawing.Size(72, 26);
            this.rbtAll.TabIndex = 17;
            this.rbtAll.Text = "全部";
            this.rbtAll.UseVisualStyleBackColor = true;
            this.rbtAll.CheckedChanged += new System.EventHandler(this.rbtAll_CheckedChanged);
            // 
            // rbtChinese
            // 
            this.rbtChinese.AutoSize = true;
            this.rbtChinese.Checked = true;
            this.rbtChinese.Font = new System.Drawing.Font("宋体", 16F);
            this.rbtChinese.Location = new System.Drawing.Point(90, 12);
            this.rbtChinese.Name = "rbtChinese";
            this.rbtChinese.Size = new System.Drawing.Size(61, 26);
            this.rbtChinese.TabIndex = 19;
            this.rbtChinese.TabStop = true;
            this.rbtChinese.Text = "A股";
            this.rbtChinese.UseVisualStyleBackColor = true;
            this.rbtChinese.CheckedChanged += new System.EventHandler(this.rbtChinese_CheckedChanged);
            // 
            // rbtBond
            // 
            this.rbtBond.AutoSize = true;
            this.rbtBond.Font = new System.Drawing.Font("宋体", 16F);
            this.rbtBond.Location = new System.Drawing.Point(157, 12);
            this.rbtBond.Name = "rbtBond";
            this.rbtBond.Size = new System.Drawing.Size(72, 26);
            this.rbtBond.TabIndex = 20;
            this.rbtBond.Text = "债券";
            this.rbtBond.UseVisualStyleBackColor = true;
            this.rbtBond.CheckedChanged += new System.EventHandler(this.rbtBond_CheckedChanged);
            // 
            // rbtOther
            // 
            this.rbtOther.AutoSize = true;
            this.rbtOther.Font = new System.Drawing.Font("宋体", 16F);
            this.rbtOther.Location = new System.Drawing.Point(235, 12);
            this.rbtOther.Name = "rbtOther";
            this.rbtOther.Size = new System.Drawing.Size(72, 26);
            this.rbtOther.TabIndex = 21;
            this.rbtOther.Text = "其他";
            this.rbtOther.UseVisualStyleBackColor = true;
            this.rbtOther.CheckedChanged += new System.EventHandler(this.rbtOther_CheckedChanged);
            // 
            // btnSell
            // 
            this.btnSell.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSell.Font = new System.Drawing.Font("宋体", 10F);
            this.btnSell.Location = new System.Drawing.Point(959, 12);
            this.btnSell.Name = "btnSell";
            this.btnSell.Size = new System.Drawing.Size(75, 26);
            this.btnSell.TabIndex = 23;
            this.btnSell.Text = "卖  出";
            this.btnSell.UseVisualStyleBackColor = true;
            this.btnSell.Click += new System.EventHandler(this.btnSell_Click);
            // 
            // btnFavor
            // 
            this.btnFavor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFavor.Font = new System.Drawing.Font("宋体", 10F);
            this.btnFavor.Location = new System.Drawing.Point(878, 12);
            this.btnFavor.Name = "btnFavor";
            this.btnFavor.Size = new System.Drawing.Size(75, 26);
            this.btnFavor.TabIndex = 24;
            this.btnFavor.Text = "自选基金";
            this.btnFavor.UseVisualStyleBackColor = true;
            this.btnFavor.Click += new System.EventHandler(this.btnFavor_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("宋体", 10F);
            this.btnCancel.Location = new System.Drawing.Point(797, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 25;
            this.btnCancel.Text = "撤  单";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1046, 461);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnFavor);
            this.Controls.Add(this.btnSell);
            this.Controls.Add(this.rbtOther);
            this.Controls.Add(this.rbtBond);
            this.Controls.Add(this.rbtChinese);
            this.Controls.Add(this.rbtAll);
            this.Controls.Add(this.dgvFunds);
            this.Name = "Form1";
            this.Text = "场外基金小助手";
            ((System.ComponentModel.ISupportInitialize)(this.dgvFunds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvFunds;
        private System.Windows.Forms.RadioButton rbtAll;
        private System.Windows.Forms.RadioButton rbtChinese;
        private System.Windows.Forms.RadioButton rbtBond;
        private System.Windows.Forms.RadioButton rbtOther;
        private System.Windows.Forms.DataGridViewLinkColumn Column1;
        private System.Windows.Forms.DataGridViewLinkColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate30;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate60;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rate90;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.Button btnSell;
        private System.Windows.Forms.Button btnFavor;
        private System.Windows.Forms.Button btnCancel;
    }
}

