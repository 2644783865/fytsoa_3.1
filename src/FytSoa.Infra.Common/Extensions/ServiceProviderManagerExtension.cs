using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FytSoa.Infra.Common.Extensions
{
    public static class ServiceProviderManagerExtension
    {
        public static object GetService(this Type serviceType)
        {
            return AppSettingConfig.HttpCurrent.RequestServices.GetService(serviceType);
        }

    }

    //public static class ServiceLocator
    //{
    //    public static IServiceProvider Instance { get; set; }

    //    // <summary>
    //    /// 手动获取注入的对象
    //    /// </summary>
    //    /// <typeparam name="T"></typeparam>
    //    /// <returns></returns>
    //    public static T GetService<T>() where T : class
    //    {
    //        //return Instance?.GetService<IHttpContextAccessor>()?.HttpContext.RequestServices.GetService<T>();
    //        return Instance?.GetService<IHttpContextAccessor>()?.HttpContext.RequestServices.GetService<T>();
    //    }
    //}
}
