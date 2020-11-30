using System;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;

namespace FytSoa.Application.Services
{
    public class SysRoleService: ISysRoleService
    {
        private readonly ISysRoleRepository _sysRoleRepository;
        public SysRoleService(ISysRoleRepository sysRoleRepository)
        {
            _sysRoleRepository = sysRoleRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
