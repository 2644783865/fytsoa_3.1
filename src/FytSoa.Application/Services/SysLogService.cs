using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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
            return await _sysLogRepository.GetListAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        
    }
}
