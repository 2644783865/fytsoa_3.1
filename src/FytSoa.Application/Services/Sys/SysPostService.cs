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
    public class SysPostService : ISysPostService {
        private readonly ISysPostRepository _sysPostRepository;
        public SysPostService (ISysPostRepository sysPostRepository) {
            _sysPostRepository = sysPostRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<PageResult<SysPost>>> GetPages (PageParam param) {
            var result = JResult<PageResult<SysPost>>.Success ();
            try {
                var where = PredicateBuilder.New<SysPost> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.Name.Contains (param.key));
                }
                if (!string.IsNullOrEmpty (param.status)) {
                    where.And (m => m.Status == bool.Parse (param.status));
                }
                result.Data = await _sysPostRepository.GetPageResult (where, m => m.Sort, 1, param.page, param.limit);
                return result;
            } catch (Exception ex) {
                return JResult<PageResult<SysPost>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysPost model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                result.Data = await _sysPostRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysPost model) {
            var result = JResult<int>.Success ();
            try {
                result.Data = await _sysPostRepository.UpdateAsync (model);
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
                result.Data = await _sysPostRepository.DeleteAsync (m => ids.StrToListLong ().Contains (m.Id));
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