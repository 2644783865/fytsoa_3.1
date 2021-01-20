using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.ViewModels;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
namespace FytSoa.Application.Interfaces
{
    public interface ISysAuthorityService : IDisposable
    {
        /// <summary>
        /// 保存权限信息
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<int>> AddMenu(AuthorityMenuParam authorityMenu);

        /// <summary>
        /// 根据角色获得权限
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<SysAuthority>>> GetAuthority(string roleId);
    }
}
