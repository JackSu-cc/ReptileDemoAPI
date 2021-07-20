using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReptileDemoAPI.ViewModel
{
    /// <summary>
    /// 请求参数
    /// </summary>
    public class ShowInfoRequestVM
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 请求连接
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 请求来源（摩天轮、大麦、票牛。。）
        /// </summary>
        public string Source { get; set; }
    }
}
