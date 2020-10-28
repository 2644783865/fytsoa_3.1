using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FytSoa.Application
{
    public class ApiAuthorize : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ignore = context.ActionDescriptor.FilterDescriptors
            .Select(f => f.Filter);
            
        }
    }
}
