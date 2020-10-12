using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Infra.Common
{
    public class PageResult<T>
    {
        /// <summary>
        /// 当前页索引
        /// </summary>
        public long CurrentPage { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        /// 每页的记录数
        /// </summary>
        public long ItemsPerPage { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        public List<T> Items { get; set; }
    }

    /// <summary>
    /// 查询列表分页基本参数
    /// </summary>
    public class PageParam
    {
        public int page { get; set; } = 1;

        public int limit { get; set; } = 15;

        public string id { get; set; }

        public string key { get; set; }

        /// <summary>
        /// 状态值  1=true  2=false  0=默认
        /// </summary>
        public int status { get; set; } = 0;
    }
}
