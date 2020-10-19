using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FytSoa.Infra.Common.Logger;
using FytSoa.Infra.CrossCutting;

namespace FytSoa.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //_logger.LogError("Test error logging");

            //Logger.Default.Info("Test Info");

            //Logger.Default.Debug("Test Debug");

            //Logger.Default.Error("Test Error");

            //Logger.Default.Setting("cur");
            //Logger.Default.Info("Test Info");

            var token = JwtAuthService.IssueJWT(new JwtToken() { id="123456",role= "Admin" });
            return new string[] { "value1", "value2", token };
        }

    }
}
