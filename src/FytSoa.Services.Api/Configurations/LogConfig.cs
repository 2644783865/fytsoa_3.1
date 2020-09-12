using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace FytSoa.Services.Api.Configurations
{
    public static class LogConfig
    {
        public static void LoggerConfiguration(this IServiceCollection service, IConfiguration Configuration)
        {
            new LoggerConfiguration()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        }
    }
}
