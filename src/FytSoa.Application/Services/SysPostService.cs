using System;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Interfaces.Sys;

namespace FytSoa.Application.Services
{
    public class SysPostService: ISysPostService
    {
        private readonly ISysPostRepository _sysPostRepository;
        public SysPostService(ISysPostRepository sysPostRepository)
        {
            _sysPostRepository = sysPostRepository;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
