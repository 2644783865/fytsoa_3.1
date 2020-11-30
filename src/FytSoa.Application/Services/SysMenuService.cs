using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;

namespace FytSoa.Application.Services
{
    public class SysMenuService: ISysMenuService
    {
        private readonly ISysMenuRepository _sysMenuRepository;
        public SysMenuService(ISysMenuRepository sysMenuRepository)
        {
            _sysMenuRepository = sysMenuRepository;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add(SysMenu model)
        {
            var result = JResult<int>.Success();
            try
            {
                model.Id = Unique.Id();
                if (string.IsNullOrEmpty(model.ParentId))
                {
                    model.ParentId = "0";
                    model.ParentGroupId = ","+model.Id+",";
                }
                else
                {
                    var _parent = await _sysMenuRepository.GetModelAsync(m=>m.Id==model.Id);
                    model.ParentGroupId = _parent.ParentGroupId +model.Id+ "," ;
                    model.Layer = _parent.Layer + 1;
                }
                var _upParent = await _sysMenuRepository.GetFirstAsync(m=>true,m=>m.Sort,1);
                model.Sort = string.IsNullOrEmpty(_upParent?.Id) ? 1 : _upParent.Sort + 1;
                await _sysMenuRepository.AddAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<int>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 查询所有菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenu>>> GetAll()
        {
            var result = JResult<List<SysMenu>>.Success();
            try
            {
                result.Data = await _sysMenuRepository.GetListAsync(m=>m.Sort,2);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<List<SysMenu>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 根据唯一编号查询菜单信息
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<SysMenu>> GetModel(string id)
        {
            var result = JResult<SysMenu>.Success();
            try
            {
                result.Data = await _sysMenuRepository.GetModelAsync(m => m.Id==id);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<SysMenu>.Error(ex.Message);
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
