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
    public class SysAdminService : ISysAdminService {
        private readonly ISysAdminRepository _thisRepository;
        public SysAdminService (ISysAdminRepository thisRepository) {
            _thisRepository = thisRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<PageResult<SysAdmin>>> GetPages (PageParam param) {
            var result = JResult<PageResult<SysAdmin>>.Success ();
            try {
                var where = PredicateBuilder.New<SysAdmin> (m => true);
                if (!string.IsNullOrEmpty (param.key)) {
                    where.And (m => m.FullName.Contains (param.key) || m.Mobile.Contains (param.key) || m.LoginAccount.Contains (param.key));
                }
                if (!string.IsNullOrEmpty (param.status)) {
                    where.And (m => m.Status == bool.Parse (param.status));
                }
                result.Data = await _thisRepository.GetPageResult (where, m => m.CreateTime, 1, param.page, param.limit);
                return result;
            } catch (Exception ex) {
                return JResult<PageResult<SysAdmin>>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add (SysAdmin model) {
            var result = JResult<int>.Success ();
            try {
                model.Id = Unique.Id ();
                model.LoginPassWord = Security.DES3Encrypt.EncryptString(model.LoginPassWord);
                result.Data = await _thisRepository.AddAsync (model);
                return result;
            } catch (Exception ex) {
                return JResult<int>.Error (ex.Message);
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Update (SysAdmin model) {
            var result = JResult<int>.Success ();
            try {
                model.LoginPassWord = Security.DES3Encrypt.EncryptString(model.LoginPassWord);
                result.Data = await _thisRepository.UpdateAsync (model);
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
                result.Data = await _thisRepository.DeleteAsync (m => ids.StrToListLong ().Contains (m.Id));
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