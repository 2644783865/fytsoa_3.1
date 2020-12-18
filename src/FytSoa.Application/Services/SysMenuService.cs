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
    public class SysMenuService : ISysMenuService {
        private readonly ISysMenuRepository _sysMenuRepository;
        public SysMenuService (ISysMenuRepository sysMenuRepository) {
            _sysMenuRepository = sysMenuRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<List<SysMenu>>> GetAll (PageParam param) {
            var result = JResult<List<SysMenu>>.Success ();
            try {
                var where = PredicateBuilder.New<SysMenu> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.Name.Contains (param.key));
                }
                if (!string.IsNullOrEmpty (param.status)) {
                    where.And (m => m.Status == bool.Parse (param.status));
                }
                result.Data = await _sysMenuRepository.GetListAsync (where, m => m.Sort, 1);
                return result;
            } catch (Exception ex) {
                return JResult<List<SysMenu>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysMenu model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                if (model.ParentId != 0) {
                    var _model = await _sysMenuRepository.GetModelAsync (m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString () + ",";
                } else {
                    model.ParentIdList = model.Id.ToString () + ',';
                }
                result.Data = await _sysMenuRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysMenu model) {
            var result = JResult<int>.Success ();
            try {
                if (model.ParentId != 0) {
                    var _model = await _sysMenuRepository.GetModelAsync (m => m.Id == model.ParentId);
                    model.Layer = _model.Layer + 1;
                    model.ParentIdList = _model.ParentIdList + model.Id.ToString () + ",";
                } else {
                    model.ParentIdList = model.Id.ToString () + ',';
                }
                result.Data = await _sysMenuRepository.UpdateAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Delete (string ids) {
            var result = JResult<int>.Success ();
            try {
                result.Data = await _sysMenuRepository.DeleteAsync (m => m.ParentIdList.Contains (ids));
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