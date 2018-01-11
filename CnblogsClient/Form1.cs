using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Common;

namespace CnblogsClient
{
    public partial class Form1 : Form
    {
        private const string TipEnd = "已经到底啦！";
        private const string TipNone = "Sorry，请换一种姿势！";
        private int _pageIndex = 1;
        //每次查看的文章或新闻个数
        private const int PageSize = 10;
        private readonly Tip _tip = new Tip();
        //加载模式0：文章；1：新闻；2：搜索文章
        private int _tpye;
        private readonly List<string> _urls = new List<string>();
        private readonly WebUtil _web = new WebUtil();

        public Form1()
        {
            InitializeComponent();
            for (; _urls.Count < PageSize; _pageIndex++)
                AddPost(_pageIndex);
        }

        /// <summary>
        ///     获取推荐数大于2的博客
        /// </summary>
        /// <param name="pageIndex"></param>
        private bool AddPost(int pageIndex)
        {
            const string url = "https://www.cnblogs.com/mvc/AggSite/PostList.aspx?";
            var html = _web.Fetch(url + GetUrl() + pageIndex, "POST");
            var posts = StringUtil.GetList(html, "\"post_item", "\"article_comment");

            foreach (var item in posts)
            {
                var n = StringUtil.GetVal(item, "\"diggnum", "/span>");
                var diggnum = Convert.ToInt32(StringUtil.GetVal(n, ">", "<"));
                if (diggnum < 3)
                    continue;
                var t = StringUtil.GetVal(item, "\"titlelnk", "/a>");
                var title = StringUtil.RemoveHtml(StringUtil.GetVal(t, ">", "<"));
                var time = StringUtil.GetVal(item, "发布于 ", 16);
                _urls.Add(StringUtil.GetHref(t));
                lstPost.Items.Add($"{diggnum} {title} {time}");
            }

            if (posts.Count == 20)
                return true;

            Warn(TipEnd);
            return false;
        }

        /// <summary>
        ///     添加搜索的博客
        /// </summary>
        /// <param name="pageIndex">页数</param>
        private void AddSearchPost(int pageIndex)
        {
            var url = $"http://zzk.cnblogs.com/s/blogpost?Keywords={txtSearch.Text.Trim()}&pageindex={pageIndex}";
            var html = _web.Fetch(url);
            var posts = StringUtil.GetList(html, "\"searchItem", "\"searchItemInfo-comments");
            foreach (var item in posts)
            {
                var diggnum = StringUtil.GetVal(item, ">推荐(", ")");
                var n = StringUtil.GetVal(item, "searchItemTitle\">", "</h3>");
                var title = StringUtil.RemoveHtml(StringUtil.GetVal(n, "\">", "</a>"));
                var date = StringUtil.GetVal(item, "searchItemInfo-publishDate\">", "</span>");
                _urls.Add(StringUtil.GetHref(n));
                lstPost.Items.Add($"{diggnum} {title} {date}");
            }

            if (_urls.Count == 0)
                Warn(TipNone);
            else if (posts.Count != 15)
                Warn(TipEnd);
        }

        /// <summary>
        ///     获取推荐数大于2的新闻
        /// </summary>
        /// <param name="pageIndex"></param>
        private bool AddNews(int pageIndex)
        {
            string url =
                "https://www.cnblogs.com/mvc/AggSite/NewsList.aspx?CategoryId=-1&CategoryType=News&ItemListActionName=NewsList&ItemListActionName=NewsList&PageIndex=" +
                pageIndex;
            var html = _web.Fetch(url, "POST");
            var posts = StringUtil.GetList(html, "\"post_item", "\"article_comment");

            foreach (var item in posts)
            {
                var n = StringUtil.GetVal(item, "\"diggnum", "/span>");
                var diggnum = Convert.ToInt32(StringUtil.GetVal(n, ">", "<"));
                if (diggnum < 3)
                    continue;
                var t = StringUtil.GetVal(item, "\"titlelnk", "/a>");
                var title = StringUtil.RemoveHtml(StringUtil.GetVal(t, ">", "<"));
                var time = StringUtil.GetVal(item, "发布于 ", 16);
                var link = StringUtil.GetHref(t);
                if (!link.Contains("http"))
                    link = "https:" + link;
                _urls.Add(link);
                lstPost.Items.Add($"{diggnum} {title} {time}");
            }

            if (posts.Count == 30)
                return true;

            Warn(TipEnd);
            return false;
        }

        private void lstPost_Click(object sender, EventArgs e)
        {
            Process.Start(_urls[lstPost.SelectedIndex]);
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            var topIndex = _urls.Count;
            var count = _urls.Count + PageSize;
            switch (_tpye)
            {
                case 1:
                    while ((_urls.Count < count) && AddNews(_pageIndex++))
                    {
                    }
                    break;
                case 2:
                    AddSearchPost(_pageIndex++);
                    break;
                default:
                    while ((_urls.Count < count) && AddPost(_pageIndex++))
                    {
                    }
                    break;
            }
            //拉动滚动条查看新的文章或新闻
            lstPost.TopIndex = topIndex;
        }

        private void Warn(string tip)
        {
            _tip.ShowTip(tip, Color.Red);
        }

        private string GetUrl()
        {
            var categoryId = "808";
            var categoryType = "SiteHome";
            var parentCategoryId = "0";
            switch (cbbCate.SelectedIndex)
            {
                case 1:
                    parentCategoryId = "108698";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "18156";
                            break;
                        case 1:
                            categoryId = "108699";
                            break;
                        case 2:
                            categoryId = "108700";
                            break;
                        case 3:
                            categoryId = "108760";
                            break;
                        case 4:
                            categoryId = "108716";
                            break;
                        case 5:
                            categoryId = "108717";
                            break;
                        case 6:
                            categoryId = "108718";
                            break;
                        case 7:
                            categoryId = "108719";
                            break;
                        case 8:
                            categoryId = "108720";
                            break;
                        case 9:
                            categoryId = "108728";
                            break;
                        case 10:
                            categoryId = "108729";
                            break;
                        case 11:
                            categoryId = "108730";
                            break;
                        case 12:
                            categoryId = "108738";
                            break;
                        case 13:
                            categoryId = "108739";
                            break;
                        case 14:
                            categoryId = "108758";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 2:
                    parentCategoryId = "2";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "106876";
                            break;
                        case 1:
                            categoryId = "106880";
                            break;
                        case 2:
                            categoryId = "106882";
                            break;
                        case 3:
                            categoryId = "106877";
                            break;
                        case 4:
                            categoryId = "108696";
                            break;
                        case 5:
                            categoryId = "106894";
                            break;
                        case 6:
                            categoryId = "108735";
                            break;
                        case 7:
                            categoryId = "108746";
                            break;
                        case 8:
                            categoryId = "108748";
                            break;
                        case 9:
                            categoryId = "108751";
                            break;
                        case 10:
                            categoryId = "108752";
                            break;
                        case 11:
                            categoryId = "108753";
                            break;
                        case 12:
                            categoryId = "108742";
                            break;
                        case 13:
                            categoryId = "108754";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 3:
                    parentCategoryId = "108701";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "106892";
                            break;
                        case 1:
                            categoryId = "108702";
                            break;
                        case 2:
                            categoryId = "106884";
                            break;
                        case 3:
                            categoryId = "108750";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 4:
                    parentCategoryId = "108703";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "106883";
                            break;
                        case 1:
                            categoryId = "106893";
                            break;
                        case 2:
                            categoryId = "108731";
                            break;
                        case 3:
                            categoryId = "108737";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 5:
                    parentCategoryId = "108704";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "78111";
                            break;
                        case 1:
                            categoryId = "50349";
                            break;
                        case 2:
                            categoryId = "106878";
                            break;
                        case 3:
                            categoryId = "108732";
                            break;
                        case 4:
                            categoryId = "108734";
                            break;
                        case 5:
                            categoryId = "108747";
                            break;
                        case 6:
                            categoryId = "108749";
                            break;
                        case 7:
                            categoryId = "3";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 6:
                    parentCategoryId = "108705";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "108706";
                            break;
                        case 1:
                            categoryId = "108707";
                            break;
                        case 2:
                            categoryId = "108736";
                            break;
                        case 3:
                            categoryId = "108708";
                            break;
                        case 4:
                            categoryId = "106886";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 7:
                    parentCategoryId = "108709";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "108710";
                            break;
                        case 1:
                            categoryId = "106891";
                            break;
                        case 2:
                            categoryId = "106889";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 8:
                    parentCategoryId = "108712";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "108713";
                            break;
                        case 1:
                            categoryId = "108714";
                            break;
                        case 2:
                            categoryId = "108715";
                            break;
                        case 3:
                            categoryId = "108743";
                            break;
                        case 4:
                            categoryId = "108756";
                            break;
                        case 5:
                            categoryId = "106881";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 9:
                    parentCategoryId = "108724";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "108721";
                            break;
                        case 1:
                            categoryId = "108725";
                            break;
                        case 2:
                            categoryId = "108726";
                            break;
                        case 3:
                            categoryId = "108755";
                            break;
                        case 4:
                            categoryId = "108757";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
                case 10:
                    parentCategoryId = "4";
                    categoryType = "SiteCategory";
                    switch (cbbType.SelectedIndex)
                    {
                        case 0:
                            categoryId = "807";
                            break;
                        case 1:
                            categoryId = "106879";
                            break;
                        case 2:
                            categoryId = "33909";
                            break;
                        case 3:
                            categoryId = "106885";
                            break;
                        case 4:
                            categoryId = "106895";
                            break;
                        case 5:
                            categoryId = "108759";
                            break;
                        default:
                            categoryId = parentCategoryId;
                            categoryType = "TopSiteCategory";
                            parentCategoryId = "0";
                            break;
                    }
                    break;
            }

            return
                $"CategoryId={categoryId}&CategoryType={categoryType}&ParentCategoryId={parentCategoryId}&ItemListActionName=PostList&PageIndex=";
        }

        private void cbbCate_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbType.Items.Clear();
            switch (cbbCate.SelectedIndex)
            {
                case 1:
                    cbbType.Items.AddRange(new object[]
                    {
                        ".NET新手区", "ASP.NET", "C#", ".NET Core", "WinForm", "Silverlight", "WCF", "CLR", "WPF", "XNA",
                        "Visual Studio", "ASP.NET MVC", "控件开发", "Entity Framework", "NHibernate"
                    });
                    break;
                case 2:
                    cbbType.Items.AddRange(new object[]
                    {
                        "JAVA", "C++", "PHP", "Delphi", "Python", "Ruby", "C", "Erlang", "Go", "Swift", "Scala", "R",
                        "Verilog", "其它"
                    });
                    break;
                case 3:
                    cbbType.Items.AddRange(new object[] {"架构设计", "面向对象", "设计模式", "领域驱动设计"});
                    break;
                case 4:
                    cbbType.Items.AddRange(new object[] {"Html/Css", "JavaScript", "jQuery", "HTML5"});
                    break;
                case 5:
                    cbbType.Items.AddRange(new object[]
                        {"SharePoint", "GIS", "SAP", "Oracle ERP", "Dynamics CRM", "K2 BPM", "信息安全", "企业信息化其他"});
                    break;
                case 6:
                    cbbType.Items.AddRange(new object[] {"Android", "iOS", "Windows Phone", "Windows Mobile", "其他"});
                    break;
                case 7:
                    cbbType.Items.AddRange(new object[] {"敏捷开发", "项目与团队管理", "软件工程其他"});
                    break;
                case 8:
                    cbbType.Items.AddRange(new object[] {"SQL Server", "Oracle", "MySQL", "NoSQL", "大数据", "其他"});
                    break;
                case 9:
                    cbbType.Items.AddRange(new object[] {"Windows", "Windows Server", "Linux", "OS X", "嵌入式"});
                    break;
                case 10:
                    cbbType.Items.AddRange(new object[]
                    {
                        "非技术区", "软件测试", "代码与软件发布", "计算机图形学", "Google开发", "游戏开发", "程序人生", "求职面试", "读书区", "转载区",
                        "Windows CE", "翻译区", "开源研究"
                    });
                    break;
            }
        }

        private void btnChangeCate_Click(object sender, EventArgs e)
        {
            ClearListBoxItems(0);
            while (AddPost(_pageIndex++) && (_urls.Count < PageSize))
            {
            }
        }

        private void btnNews_Click(object sender, EventArgs e)
        {
            ClearListBoxItems(1);
            while (AddNews(_pageIndex++) && (_urls.Count < PageSize))
            {
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                ClearListBoxItems(2);
                AddSearchPost(_pageIndex++);
            }
        }

        /// <summary>
        ///     清空列表数据，并定于当前类型
        /// </summary>
        /// <param name="type"></param>
        public void ClearListBoxItems(int type)
        {
            lstPost.Items.Clear();
            _urls.Clear();
            _pageIndex = 1;
            _tpye = type;
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                btnSearch_Click(null, null);
        }

        private void cbbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnChangeCate_Click(null, null);
        }

        private void lstPost_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                lstPost_Click(null, null);
        }
    }
}