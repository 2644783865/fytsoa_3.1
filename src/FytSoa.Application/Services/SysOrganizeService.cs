using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using FytSoa.Infra.Common.Extensions;
using LinqKit;

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
        public async Task<ApiResult<List<SysOrganize>>> GetAll(PageParam param)
        {
            var result = JResult<List<SysOrganize>>.Success();
            try
            {
                var where = PredicateBuilder.New<SysOrganize>(m=>true);
                if (!string.IsNullOrEmpty(param.key))
                {
                    where.And(m=>m.Name.Contains(param.key));
                }
                if (!string.IsNullOrEmpty(param.status))
                {
                    where.And(m=>m.Status==bool.Parse(param.status));
                }
                result.Data = await _sysOrganizeRepository.GetListAsync(where,m=>m.Sort,1);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<List<SysOrganize>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add(SysOrganize model)
        {
            var result = JResult<int>.Success();
            try
            {
                model.Id = Unique.Id();
                if (model.ParentId!=0)
                {
                    var _model = await _sysOrganizeRepository.GetModelAsync(m=>m.Id==model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString() + ",";
                }
                else
                {
                    model.ParentIdList = model.Id.ToString() + ',';
                }
                result.Data = await _sysOrganizeRepository.AddAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<int>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 修改机构
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update(SysOrganize model)
        {
            var result = JResult<int>.Success();
            try
            {
                if (model.ParentId != 0)
                {
                    var _model = await _sysOrganizeRepository.GetModelAsync(m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString() + ",";
                }
                else
                {
                    model.ParentIdList = model.Id.ToString() + ',';
                }
                result.Data = await _sysOrganizeRepository.UpdateAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<int>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Delete(string ids)
        {
            var result = JResult<int>.Success();
            try
            {
                result.Data = await _sysOrganizeRepository.DeleteAsync(m=>m.ParentIdList.Contains(ids));
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
