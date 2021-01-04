using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using FytSoa.Infra.Common;
using FytSoa.Infra.Common.Extensions;

namespace FytSoa.Application.Services
{
    public class SysLogService : ISysLogService
    {
        private readonly ISysLogRepository _sysLogRepository;
        public SysLogService(ISysLogRepository sysLogRepository)
        {
            _sysLogRepository = sysLogRepository;
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<PageResult<SysLog>>> GetPages(PageParam param)
        {
            var result = JResult<PageResult<SysLog>>.Success();
            try
            {
                var where = PredicateBuilder.New<SysLog>(m => true);
                if (!string.IsNullOrEmpty(param.key))
                {
                    where.And(m => m.Method.Contains(param.key) || m.Module.Contains(param.key));
                }
                if (!string.IsNullOrEmpty(param.status))
                {
                    where.And(m => m.LogType == int.Parse(param.status));
                }
                result.Data = await _sysLogRepository.GetPageResult(where, m => m.OperateTime, m => new SysLog()
                {
                    Id = m.Id,
                    OperateUser = m.OperateUser,
                    Address = m.Address,
                    IP = m.IP,
                    Method=m.Method,
                    Browser = m.Browser,
                    OperateTime = m.OperateTime,
                    Parameters = m.Parameters,
                    ExecutionDuration = m.ExecutionDuration,
                    Message = m.Message
                }, 1, param.page, param.limit);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<PageResult<SysLog>>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 获得详情
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<SysLog>> GetModel(long id)
        {
            var result = JResult<SysLog>.Success();
            try
            {
                result.Data = await _sysLogRepository.GetModelAsync(m=>m.Id==id);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<SysLog>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Add(SysLog model)
        {
            var result = JResult<int>.Success();
            try
            {
                model.Id = Unique.Id();
                result.Data = await _sysLogRepository.AddAsync(model);
                return result;
            }
            catch (Exception ex)
            {
                return JResult<int>.Error(ex.Message);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResult<int>> Delete(string ids)
        {
            var result = JResult<int>.Success();
            try
            {
                result.Data = await _sysLogRepository.DeleteAsync(m => ids.StrToListLong().Contains(m.Id));
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
