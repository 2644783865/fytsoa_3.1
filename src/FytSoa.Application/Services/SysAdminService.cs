using System;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;

namespace FytSoa.Application.Services
{
    public class SysAdminService: ISysAdminService
    {
        private readonly ISysAdminRepository _sysAdminRepository;
        public SysAdminService(ISysAdminRepository sysAdminRepository)
        {
            _sysAdminRepository = sysAdminRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
