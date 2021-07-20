using HtmlAgilityPack;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ReptileDemo
{
    class Program
    {
        //http://www.baidu.com/s?wd=git
        //var nodes = doc.DocumentNode.SelectNodes("//div[@id='news_list']/div/div[2]/h2/a");
        static async Task Main(string[] args)
        {
            for (int i = 0; i < 50; i++)
            {
                Console.WriteLine($"第{i}次请求\t\n\t");
                await Task.Run(() =>
                {
                    GetBaiduResult();
                });
            }

            Console.ReadKey();
        }

        public static void GetBaiduResult()
        {
            Console.WriteLine(DateTime.Now);
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HtmlWeb web = new HtmlWeb();
            var doc = web.LoadFromWebAsync("http://www.baidu.com/s?wd=领克01值得买吗").Result;
            var nodes = doc.DocumentNode.SelectNodes("//div[@id='content_left']/div/div/div/div[2]/div");

            if (nodes == null)
            {
                int i = 1;
                while (nodes == null)
                {
                    i++;
                    doc = web.LoadFromWebAsync("http://www.baidu.com/s?wd=领克01值得买吗").Result;
                    nodes = doc.DocumentNode.SelectNodes("//div[@id='content_left']/div/div/div/div[2]/div");
                }
                Console.WriteLine(i);

            }
            foreach (var item in nodes)
            {
                var ds1 = item.ParentNode.ParentNode.SelectNodes("div[@class='c-abstract']");


                Console.WriteLine($"标题和地址：{item.Attributes["data-tools"]?.Value}\t\n 文章简介：{ds1?[0].InnerText}\t\n");
            }

        }
    }
}
