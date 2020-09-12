using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Data.Repository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FytSoa.Infra.Data.Implements.Sys
{
    public class SysLogRepository: BaseRepository<SysLog>, ISysLogRepository
    {
        
    }
}
