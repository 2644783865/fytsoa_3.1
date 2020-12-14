using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;

namespace FytSoa.Application.Services
{
    public class SysOrganizeService: ISysOrganizeService
    {
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        public SysOrganizeService(ISysOrganizeRepository sysOrganizeRepository)
        {
            _sysOrganizeRepository = sysOrganizeRepository;
        }

        /// <summary>
        /// 查询所有机构列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysOrganize>>> GetAll()
        {
            var result = JResult<List<SysOrganize>>.Success();
            try
            {
                result.Data = await _sysOrganizeRepository.GetListAsync(m => m.Sort, 2);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<List<SysOrganize>>.Error(ex.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
