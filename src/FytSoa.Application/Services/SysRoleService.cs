using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using FytSoa.Infra.Common.Extensions;
using LinqKit;

namespace FytSoa.Application.Services {
    public class SysRoleService : ISysRoleService {
        private readonly ISysRoleRepository _sysRoleRepository;
        public SysRoleService (ISysRoleRepository sysRoleRepository) {
            _sysRoleRepository = sysRoleRepository;
        }

        /// <summary>
        /// 查询所有机构列表
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysRole>>> GetAll (PageParam param) {
            var result = JResult<List<SysRole>>.Success ();
            try {
                var where = PredicateBuilder.New<SysRole> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.Name.Contains (param.key));
                }
                if (!string.IsNullOrEmpty (param.status)) {
                    where.And (m => m.Status == bool.Parse (param.status));
                }
                result.Data = await _sysRoleRepository.GetListAsync (where, m => m.Sort, 1);
                return result;
            } catch (Exception ex) {
                return JResult<List<SysRole>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加机构
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysRole model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                if (model.ParentId != 0) {
                    var _model = await _sysRoleRepository.GetModelAsync (m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString () + ",";
                } else {
                    model.ParentIdList = model.Id.ToString () + ',';
                }
                result.Data = await _sysRoleRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改机构
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysRole model) {
            var result = JResult<int>.Success ();
            try {
                if (model.ParentId != 0) {
                    var _model = await _sysRoleRepository.GetModelAsync (m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString () + ",";
                } else {
                    model.ParentIdList = model.Id.ToString () + ',';
                }
                result.Data = await _sysRoleRepository.UpdateAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 删除机构
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Delete (string ids) {
            var result = JResult<int>.Success ();
            try {
                result.Data = await _sysRoleRepository.DeleteAsync (m => m.ParentIdList.Contains (ids));
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        public void Dispose () {
            GC.SuppressFinalize (this);
        }
    }
}