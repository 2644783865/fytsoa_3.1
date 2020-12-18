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
    public class SysCodeTypeService : ISysCodeTypeService {
        private readonly ISysCodeTypeRepository _sysCodeTypeRepository;
        public SysCodeTypeService (ISysCodeTypeRepository sysCodeTypeRepository) {
            _sysCodeTypeRepository = sysCodeTypeRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<PageResult<SysCodeType>>> GetPages (PageParam param) {
            var result = JResult<PageResult<SysCodeType>>.Success ();
            try {
                var where = PredicateBuilder.New<SysCodeType> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.Name.Contains (param.key));
                }
                result.Data = await _sysCodeTypeRepository.GetPageResult (where, m => m.Sort, 1, param.page, param.limit);
                return result;
            } catch (Exception ex) {
                return JResult<PageResult<SysCodeType>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysCodeType model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                result.Data = await _sysCodeTypeRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysCodeType model) {
            var result = JResult<int>.Success ();
            try {
                result.Data = await _sysCodeTypeRepository.UpdateAsync (model);
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
                result.Data = await _sysCodeTypeRepository.DeleteAsync (m => ids.StrToListLong ().Contains (m.Id));
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