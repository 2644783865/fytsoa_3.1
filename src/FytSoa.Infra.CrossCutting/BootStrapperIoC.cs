using FytSoa.Application.Interfaces;
using FytSoa.Application.Services;
using FytSoa.Domain.Interfaces.Sys;
using FytSoa.Domain.Repository.Interfaces;
using FytSoa.Infra.Common;
using FytSoa.Infra.Data.Implements.Sys;
using FytSoa.Infra.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace FytSoa.Infra.CrossCutting
{
    public static class BootStrapperIoC
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // Application
            Assembly assemblyService = Assembly.Load("FytSoa.Application");
            List<Type> serviceType = assemblyService.GetTypes().Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType && u.Name.EndsWith("Service")).ToList();
            foreach (var item in serviceType.Where(s => !s.IsInterface))
            {
                var interfaceType = item.GetInterfaces();
                services.AddScoped(interfaceType[0], item);
            }


            // Infra - Data
            Assembly assemblyRepository = Assembly.Load("FytSoa.Infra.Data");
            List<Type> repositoryType = assemblyRepository.GetTypes().Where(u => u.IsClass && !u.IsAbstract && !u.IsGenericType && u.Name.EndsWith("Repository")).ToList();
            foreach (var item in repositoryType.Where(s => !s.IsInterface))
            {
                var interfaceType = item.GetInterfaces().FirstOrDefault(m=>!m.Name.Contains("IBaseRepository"));
                services.AddScoped(interfaceType, item);
            }

            //redis cache
            RedisHelper.Initialization(new CSRedis.CSRedisClient(AppSettingConfig.RedisConnectionString));
        }
    }

    public static class ServiceLocator
    {
        public static IServiceProvider Instance { get; set; }

        // <summary>
        /// 手动获取注入的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetService<T>() where T : class
        {
            return Instance?.GetService<IHttpContextAccessor>()?.HttpContext.RequestServices.GetService<T>();
        }
    }
}
