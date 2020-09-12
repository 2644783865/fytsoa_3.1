using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;

namespace FytSoa.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        //private readonly ILogger _logger;

        //public TestController(ILogger<TestController> logger)
        //{
        //    _logger = logger;
        //}

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //_logger.LogInformation("测试");
            Log.Logger = new LoggerConfiguration().CreateLogger();
            Log.Information("No one listens to me!");
            Log.Debug("No one listens to me!");
            return new string[] { "value1", "value2" };
        }

    }
}
