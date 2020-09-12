using System;
using System.Collections.Generic;
using System.Text;

namespace FytSoa.Infra.Common.Extensions
{
    public static class ServiceProviderManagerExtension
    {
        public static object GetService(this Type serviceType)
        {
            return AppSettingConfig.HttpCurrent.RequestServices.GetService(serviceType);
        }

    }
}
