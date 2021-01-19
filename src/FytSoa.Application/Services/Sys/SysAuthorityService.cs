using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Application.ViewModels;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;

namespace FytSoa.Application.Services
{
    public class SysAuthorityService: ISysAuthorityService
    {
        private readonly ISysAuthorityRepository _thisRepository;
        public SysAuthorityService(ISysAuthorityRepository thisRepository)
        {
            _thisRepository = thisRepository;
        }

        /// <summary>
        /// 保存权限信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> AddMenu(AuthorityMenuParam authorityMenu)
        {
            var result = JResult<int>.Success();
            try
            {
                await _thisRepository.DeleteAsync(m=>m.RoleId==authorityMenu.RoleId && m.Types==3);
                var list = new List<SysAuthority>();
                foreach (var item in authorityMenu.Menus)
                {
                    list.Add(new SysAuthority() { 
                        RoleId=authorityMenu.RoleId,
                        MenuId=item.MenuId,
                        BtnFun=item.BtnFun,
                        Types=3
                    });
                }
                result.Data = await _thisRepository.AddListAsync(list);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<int>.Error(ex.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
