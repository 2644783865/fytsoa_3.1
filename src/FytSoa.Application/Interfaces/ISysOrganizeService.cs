using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;

namespace FytSoa.Application.Interfaces
{
    public interface ISysOrganizeService : IDisposable
    {
        /// <summary>
        /// 查询所有菜单列表
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysOrganize>>> GetAll();
    }
}
