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
    public class SysCodeService : ISysCodeService {
        private readonly ISysCodeRepository _sysCodeRepository;
        public SysCodeService (ISysCodeRepository sysCodeRepository) {
            _sysCodeRepository = sysCodeRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<PageResult<SysCode>>> GetPages (PageParam param) {
            var result = JResult<PageResult<SysCode>>.Success ();
            try {
                var where = PredicateBuilder.New<SysCode> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.Name.Contains (param.key));
                }
                if (param.id != 0) {
                    where.And (m => m.TypeId == param.id);
                }
                result.Data = await _sysCodeRepository.GetPageResult (where, m => m.Sort, 1, param.page, param.limit);
                return result;
            } catch (Exception ex) {
                return JResult<PageResult<SysCode>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysCode model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                result.Data = await _sysCodeRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysCode model) {
            var result = JResult<int>.Success ();
            try {
                result.Data = await _sysCodeRepository.UpdateAsync (model);
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
                result.Data = await _sysCodeRepository.DeleteAsync (m => ids.StrToListLong ().Contains (m.Id));
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