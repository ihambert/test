using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Common;

namespace test2
{
    class Program
    {
        static void Main(string[] args)
        {
            var web = new WebClient();
            web.Encoding = Encoding.UTF8;
            var sb = "http://www.chinatax.gov.cn/n";
            var x = web.DownloadString("http://www.chinatax.gov.cn/n810341/n810765/index.html");
            var ls = StringUtil.GetList(x, "../../n", "\"");
            foreach (var l in ls)
            {
                if (l.Split('n').Length > 4)
                {
                    var u = sb + l;
                    var r = web.DownloadString(u);
                    DealPage(web, sb, r);
                    int pa = 1;
                    while (true)
                    {
                        var p = u.Replace(".html", $"_1060206_{pa++}.html");

                        try
                        {
                            r = web.DownloadString(p);
                        }
                        catch (Exception e)
                        {
                            break;
                        }

                        DealPage(web, sb, r);
                    }
                }
            }

        }

        private static void DealPage(WebClient web, string sb, string r)
        {
            var xx = StringUtil.GetList(r, "../../../../n", "\"");

            foreach (var y in xx)
            {
                if (y.EndsWith("content.html"))
                {
                    string zz;

                    try
                    {
                        zz = web.DownloadString(sb + y);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }

                    var zx = StringUtil.GetVal(zz, ">税务公报</a>", "</div>");

                    if (string.IsNullOrEmpty(zx))
                    {
                        continue;
                    }

                    var year = StringUtil.GetList(zx, "_blank>", "</a>");
                    var title = StringUtil.GetVal(zz, "sv_texth1_red\"", "</li>", false);
                    title = StringUtil.GetVal(title, ">", "</");
                    Console.WriteLine(title);
                    var path = $"{year[0]}/{year[1]}/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    File.WriteAllText(path + StringUtil.RemoveHtml(title).Replace("/", "") + ".html", zz);
                }
            }
        }
    }
}
