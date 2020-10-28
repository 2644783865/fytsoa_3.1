using System;
using System.Threading.Tasks;
using FytSoa.Infra.Common;
using FytSoa.Infra.Common.Logger;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FytSoa.Application
{
    public class ExceptionFilter
    {
        private readonly RequestDelegate _next;

        public ExceptionFilter(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex) 
            {
                await ExceptionHandlerAsync(context, ex);
            }
        }

        private async Task ExceptionHandlerAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            Logger.Default.Setting("GlobalError");
            Logger.Default.Error($"response exception:{ex.Message},path:{context.Request.Path}");
            Logger.Default.Setting("");

            var result = JsonConvert.SerializeObject(new ApiResult<string>()
            {
                status = 500,
                msg = ex.Message
            });

            await context.Response.WriteAsync(result);
        }
    }
}
