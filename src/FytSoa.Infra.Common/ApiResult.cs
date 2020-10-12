using System.Net;

namespace FytSoa.Infra.Common
{
    /// <summary>
    /// API 返回JSON格式
    /// </summary>
    public class ApiResult<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// 信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public int StatusCode { get; set; } = (int)HttpStatusCode.OK;

        /// <summary>
        /// 数据集
        /// </summary>
        public T Data { get; set; }
    }
}
