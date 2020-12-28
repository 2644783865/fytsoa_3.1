using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace FytSoa.Application.Filters {
    public class AuditLogActionFilter : IAsyncActionFilter {
        public AuditLogActionFilter () { }
        public async Task OnActionExecutionAsync (ActionExecutingContext context, ActionExecutionDelegate next) {
            // 判断是否写日志
            // if (false) {
            //     await next ();
            //     return;
            // }
            //接口Type
            var type = (context.ActionDescriptor as ControllerActionDescriptor).ControllerTypeInfo.AsType ();
            //方法信息
            var method = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
            //方法参数
            var arguments = context.ActionArguments;
            //开始计时
            var stopwatch = Stopwatch.StartNew ();
            //构建实体
            var logInfo = new AuditLogInfo () { };
            ActionExecutedContext result = null;
            try {
                result = await next ();
                if (result.Exception != null && !result.ExceptionHandled) {
                    logInfo.Exception = result.Exception;
                }
            } catch (Exception ex) {
                logInfo.Exception = ex;
                throw;
            } finally {
                stopwatch.Stop ();
                logInfo.ExecutionDuration = Convert.ToInt32 (stopwatch.Elapsed.TotalMilliseconds);
                if (result != null) {
                    switch (result.Result) {
                        case ObjectResult objectResult:
                            logInfo.ReturnValue = JsonConvert.SerializeObject (objectResult.Value);
                            break;
                        case JsonResult jsonResult:
                            logInfo.ReturnValue = JsonConvert.SerializeObject (jsonResult.Value);
                            break;
                        case ContentResult contentResult:
                            logInfo.ReturnValue = contentResult.Content;
                            break;
                    }
                }
                //保存日志信息
                //_logService.Add(logInof);
            }
        }
        /// <summary>
        /// 是否需要记录审计
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private bool ShouldSaveAudit (ActionExecutingContext context) {
            if (!(context.ActionDescriptor is ControllerActionDescriptor))
                return false;
            var methodInfo = (context.ActionDescriptor as ControllerActionDescriptor).MethodInfo;
            if (methodInfo == null) {
                return false;
            }
            if (!methodInfo.IsPublic) {
                return false;
            }
            return false;
        }
    }

    public class AuditLogInfo {
        /// <summary>
        /// 调用参数
        /// </summary>
        public string Parameters { get; set; }
        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string BrowserInfo { get; set; }
        /// <summary>
        /// 客户端信息
        /// </summary>
        public string ClientName { get; set; }
        /// <summary>
        /// 客户端IP地址
        /// </summary>
        public string ClientIpAddress { get; set; }
        /// <summary>
        /// 执行耗时
        /// </summary>
        public int ExecutionDuration { get; set; }
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecutionTime { get; set; }
        /// <summary>
        /// 返回内容
        /// </summary>
        public string ReturnValue { get; set; }
        /// <summary>
        /// 异常对象
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// 方法名
        /// </summary>
        public string MethodName { get; set; }
        /// <summary>
        /// 服务名
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 调用者信息
        /// </summary>
        public string UserInfo { get; set; }
        /// <summary>
        /// 自定义数据
        /// </summary>
        public string CustomData { get; set; }
    }
}