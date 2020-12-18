using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
namespace FytSoa.Application.Interfaces {
    public interface ISysCodeService : IDisposable {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<PageResult<SysCode>>> GetPages (PageParam param);

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Add (SysCode model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Update (SysCode model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Delete (string ids);
    }
}