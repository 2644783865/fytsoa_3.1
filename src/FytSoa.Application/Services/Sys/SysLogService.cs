using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LinqKit;

namespace FytSoa.Application.Services
{
    public class SysLogService : ISysLogService
    {
        private readonly ISysLogRepository _sysLogRepository;
        public SysLogService(ISysLogRepository sysLogRepository)
        {
            _sysLogRepository = sysLogRepository;
        }

        public async Task<IEnumerable<SysLog>> GetAll()
        {
            //var where = PredicateBuilder.New<SysLog>();
            //where.And(m=>m.Id==1234566);

            //Expression<Func<SysLog, bool>> _where = t => t.Id > 1000;
            return await _sysLogRepository.GetListAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}
