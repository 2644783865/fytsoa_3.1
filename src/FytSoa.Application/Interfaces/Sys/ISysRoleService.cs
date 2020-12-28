using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;

namespace FytSoa.Application.Interfaces {
    public interface ISysRoleService : IDisposable {
        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysRole>>> GetAll (PageParam param);

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Add (SysRole model);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Update (SysRole model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Delete (string ids);
    }
}