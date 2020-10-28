using System.Net;

namespace FytSoa.Infra.Common
{
    /// <summary>
    /// API 返回JSON格式
    /// </summary>
    public class ApiResult<T>
    {
        /// <summary>
        /// 信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int status { get; set; } = (int)HttpStatusCode.OK;

        /// <summary>
        /// 数据集
        /// </summary>
        public T data { get; set; }
    }
}
