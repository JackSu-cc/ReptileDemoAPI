using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReptileDemoAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ReptileDemoAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        /// <summary>
        /// Home
        /// </summary>
        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }


        /// <summary>
        /// 请求百度
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBaiduResult(string request)
        {
            var result = new ReptileBaiduListDataVM();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            HtmlWeb web = new HtmlWeb();
            var doc = web.LoadFromWebAsync($"http://www.baidu.com/s?wd={request}&tn=baiduhome_pg&rsv_spt=1&cl=3").Result;
            var nodes = doc.DocumentNode.SelectNodes("//div[@id='content_left']/div/div/div/div[2]/div");

            if (nodes == null)
            {
                int i = 1;
                while (nodes == null)
                {
                    i++;
                    doc = web.LoadFromWebAsync($"http://www.baidu.com/s?wd={request}").Result;
                    nodes = doc.DocumentNode.SelectNodes("//div[@id='content_left']/div/div/div/div[2]/div");
                }
                result.RequestCount = $"本次请求运行次数：{i}";

            }
            result.reptileResult = new List<ReptileResult>();
            foreach (var item in nodes)
            {
                var ds1 = item.ParentNode.ParentNode.SelectNodes("div[@class='c-abstract']");

                ReptileResult reptile = new ReptileResult()
                {
                    Title = $"标题和地址：{item.Attributes["data-tools"]?.Value}",
                    Desc = $"文章简介：{ds1?[0].InnerText}"
                };
                result.reptileResult.Add(reptile);
            }

            return Ok(result);
        }

        /// <summary>
        /// 初始化加载页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetDefaultShowInfoByPageList()
        {

            return Ok();
        }

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetShowInfoByPageList(string request)
        {
            var client = _httpClientFactory.CreateClient("motianlun");
            var data = await client.GetStringAsync($"http://www.moretickets.com/search/{request}");
            while (string.IsNullOrWhiteSpace(data))
            {
                data = await client.GetStringAsync($"http://www.moretickets.com/search/{request}");
            }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            var nodes = doc.DocumentNode.SelectNodes("//div[@class='list-page-content']/div/div/div[3]/a");

            var result = new List<ReptileResult>();
            foreach (var item in nodes)
            {
                var time = item.SelectNodes("div[@class='show-inner']/div/div[2]/div[4]/div")?[0].InnerText;
                var address = item.SelectNodes("div[@class='show-inner']/div/div[2]/div[4]/div")?[1].InnerText;
                var view = item.SelectNodes("div[@class='show-inner']/div[2]/div[1]")?[0].InnerText;

                ReptileResult reptile = new ReptileResult()
                {
                    Title = item.Attributes["data-sashowname"]?.Value,
                    Url = item.Attributes["href"]?.Value,
                    Address = address,
                    ShowTime = time,
                    Views = view
                };
                result.Add(reptile);
            }

            return Ok(result.Where(c => !string.IsNullOrWhiteSpace(c.Title)).ToList());
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> GetShowInfo(ShowInfoRequestVM request)
        {
            return Ok();
        }
    }
}
