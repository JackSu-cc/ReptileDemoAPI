using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReptileDemoAPI.ViewModel
{
    public class ReptileBaiduListDataVM
    {
        /// <summary>
        /// 请求次数
        /// </summary>
        public string RequestCount { get; set; }


        /// <summary>
        /// 返回结果列表
        /// </summary>
        public List<ReptileResult> reptileResult { get; set; }


    }
    /// <summary>
    /// 返回结果
    /// </summary>
    public class ReptileResult
    {
        /// <summary>
        /// 标题json
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 演出地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 演出时间
        /// </summary>
        public string ShowTime { get; set; }

        /// <summary>
        /// 浏览量
        /// </summary>
        public string Views { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Desc { get; set; }
    }
}
