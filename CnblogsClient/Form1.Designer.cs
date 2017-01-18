namespace CnblogsClient
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
            this.lstPost = new System.Windows.Forms.ListBox();
            this.btnMore = new System.Windows.Forms.Button();
            this.cbbCate = new System.Windows.Forms.ComboBox();
            this.cbbType = new System.Windows.Forms.ComboBox();
            this.btnChangeCate = new System.Windows.Forms.Button();
            this.btnNews = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstPost
            // 
            this.lstPost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstPost.CausesValidation = false;
            this.lstPost.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lstPost.FormattingEnabled = true;
            this.lstPost.ItemHeight = 21;
            this.lstPost.Location = new System.Drawing.Point(12, 12);
            this.lstPost.Name = "lstPost";
            this.lstPost.Size = new System.Drawing.Size(798, 256);
            this.lstPost.TabIndex = 0;
            this.lstPost.Click += new System.EventHandler(this.lstPost_Click);
            this.lstPost.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstPost_KeyDown);
            // 
            // btnMore
            // 
            this.btnMore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMore.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnMore.Location = new System.Drawing.Point(697, 274);
            this.btnMore.Name = "btnMore";
            this.btnMore.Size = new System.Drawing.Size(113, 32);
            this.btnMore.TabIndex = 1;
            this.btnMore.Text = "加载更多&`";
            this.btnMore.UseVisualStyleBackColor = true;
            this.btnMore.Click += new System.EventHandler(this.btnMore_Click);
            // 
            // cbbCate
            // 
            this.cbbCate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbbCate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbCate.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbCate.FormattingEnabled = true;
            this.cbbCate.Items.AddRange(new object[] {
            ".NET技术",
            "编程语言",
            "软件设计",
            "Web前端",
            "企业信息化",
            "手机开发",
            "软件工程",
            "数据库技术",
            "操作系统",
            "其他分类"});
            this.cbbCate.Location = new System.Drawing.Point(13, 276);
            this.cbbCate.Name = "cbbCate";
            this.cbbCate.Size = new System.Drawing.Size(117, 29);
            this.cbbCate.TabIndex = 2;
            this.cbbCate.SelectedIndexChanged += new System.EventHandler(this.cbbCate_SelectedIndexChanged);
            // 
            // cbbType
            // 
            this.cbbType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbType.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbbType.FormattingEnabled = true;
            this.cbbType.Location = new System.Drawing.Point(136, 276);
            this.cbbType.Name = "cbbType";
            this.cbbType.Size = new System.Drawing.Size(162, 29);
            this.cbbType.TabIndex = 3;
            this.cbbType.SelectedIndexChanged += new System.EventHandler(this.cbbType_SelectedIndexChanged);
            // 
            // btnChangeCate
            // 
            this.btnChangeCate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnChangeCate.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnChangeCate.Location = new System.Drawing.Point(304, 274);
            this.btnChangeCate.Name = "btnChangeCate";
            this.btnChangeCate.Size = new System.Drawing.Size(60, 32);
            this.btnChangeCate.TabIndex = 4;
            this.btnChangeCate.Text = "确定";
            this.btnChangeCate.UseVisualStyleBackColor = true;
            this.btnChangeCate.Click += new System.EventHandler(this.btnChangeCate_Click);
            // 
            // btnNews
            // 
            this.btnNews.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnNews.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNews.Location = new System.Drawing.Point(370, 274);
            this.btnNews.Name = "btnNews";
            this.btnNews.Size = new System.Drawing.Size(62, 32);
            this.btnNews.TabIndex = 5;
            this.btnNews.Text = "新闻";
            this.btnNews.UseVisualStyleBackColor = true;
            this.btnNews.Click += new System.EventHandler(this.btnNews_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtSearch.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSearch.Location = new System.Drawing.Point(438, 275);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(185, 31);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyDown);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSearch.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(629, 275);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 32);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "搜索";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(822, 318);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnNews);
            this.Controls.Add(this.btnChangeCate);
            this.Controls.Add(this.cbbType);
            this.Controls.Add(this.cbbCate);
            this.Controls.Add(this.btnMore);
            this.Controls.Add(this.lstPost);
            this.Name = "Form1";
            this.Text = "博客园精华";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstPost;
        private System.Windows.Forms.Button btnMore;
        private System.Windows.Forms.ComboBox cbbCate;
        private System.Windows.Forms.ComboBox cbbType;
        private System.Windows.Forms.Button btnChangeCate;
        private System.Windows.Forms.Button btnNews;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnSearch;
    }
}

