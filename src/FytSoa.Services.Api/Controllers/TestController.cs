using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FytSoa.Infra.Common.Logger;
using FytSoa.Infra.CrossCutting;
using Microsoft.AspNetCore.Authorization;
using FytSoa.Application;

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

        [HttpGet("token")]
        public IEnumerable<string> Get(string role)
        {
            //_logger.LogError("Test error logging");

            //Logger.Default.Info("Test Info");

            //Logger.Default.Debug("Test Debug");

            //Logger.Default.Error("Test Error");

            //Logger.Default.Setting("cur");
            //Logger.Default.Info("Test Info");

            var token = JwtAuthService.IssueJWT(new JwtToken() { id="123456",role= role });
            return new string[] {  token };
        }

        [HttpGet("Admin")]
        [Authorize("Admin")]
        public IEnumerable<string> GetAdmin()
        {
            return new string[] { "Admin" };
        }

        [HttpGet("App")]
        [Authorize("App")]
        public IEnumerable<string> GetApp()
        {
            return new string[] { "App" };
        }

    }
}
