using FytSoa.Domain.Models.Sys;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Application.Interfaces
{
    public interface ISysLogService : IDisposable
    {
        Task<IEnumerable<SysLog>> GetAll();
    }
}
