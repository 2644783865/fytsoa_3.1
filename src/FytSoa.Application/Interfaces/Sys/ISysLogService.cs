using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Application.Interfaces
{
    public interface ISysLogService : IDisposable
    {

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<PageResult<SysLog>>> GetPages(PageParam param);

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Add(SysLog model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Delete(string ids);
    }
}
