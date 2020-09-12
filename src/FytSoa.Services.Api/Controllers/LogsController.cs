using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Models.Sys;
using Microsoft.AspNetCore.Mvc;


namespace FytSoa.Services.Api.Controllers
{
    [Route("api/[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly ISysLogService _sysLogService;
        public LogsController(ISysLogService sysLogService)
        {
            _sysLogService = sysLogService;
        }

        /// <summary>
        /// test zs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<SysLog>> Get()
        {
            return await _sysLogService.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
