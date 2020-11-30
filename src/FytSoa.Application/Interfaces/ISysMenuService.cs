using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;

namespace FytSoa.Application.Interfaces
{
    public interface ISysMenuService : IDisposable
    {
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> Add(SysMenu model);

        /// <summary>
        /// 根据唯一编号查询菜单信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<SysMenu>> GetModel(string id);

        /// <summary>
        /// 查询所有菜单列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysMenu>>> GetAll();
    }
}
