using System;
using System.Threading.Tasks;
using FytSoa.Infra.Common;
using FytSoa.Infra.Common.Logger;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace FytSoa.Application
{
    public class JwtTokenFilter
    {
        private readonly RequestDelegate _next;

        public JwtTokenFilter(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            if (context.Request.Method == "OPTIONS")
            {
                return _next(context);
            }
            var headers = context.Request.Headers;

            //if (!headers.ContainsKey("Authorization"))
            //{
            //    string path = context.Request.Path.Value;
            //    if (path.Contains("swagger") || path.Contains("token"))
            //    {
            //        return _next(context);
            //    }
            //    // check authorize  wait...
            //    return JwtTokenHandlerAsync(context);
            //}
            return _next(context);
        }

        private async Task JwtTokenHandlerAsync(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            var result = JsonConvert.SerializeObject(new ApiResult<string>()
            {
                status = 401,
                msg = "无权限"
            });

            await context.Response.WriteAsync(result);
        }
    }
}
