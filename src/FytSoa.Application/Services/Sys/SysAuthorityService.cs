using System;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;

namespace FytSoa.Application.Services
{
    public class SysAuthorityService: ISysAuthorityService
    {
        private readonly ISysAuthorityRepository _sysAuthorityRepository;
        public SysAuthorityService(ISysAuthorityRepository sysAuthorityRepository)
        {
            _sysAuthorityRepository = sysAuthorityRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
