using FytSoa.Application.Interfaces;
using FytSoa.Application.Services;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Repository.Interfaces;
using FytSoa.Infra.Data.Implements.Sys;
using FytSoa.Infra.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Infra.CrossCutting
{
    public static class BootStrapperIoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            services.AddScoped<ISysLogService, SysLogService>();

            // Infra - Data
            services.AddScoped<ISysLogRepository, SysLogRepository>();
        }
    }
}
