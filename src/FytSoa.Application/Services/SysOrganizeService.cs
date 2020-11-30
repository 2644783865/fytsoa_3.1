using System;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;

namespace FytSoa.Application.Services
{
    public class SysOrganizeService: ISysOrganizeService
    {
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        public SysOrganizeService(ISysOrganizeRepository sysOrganizeRepository)
        {
            _sysOrganizeRepository = sysOrganizeRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
