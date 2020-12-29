using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using FytSoa.Infra.CrossCutting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace FytSoa.Services.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize("Admin")]
    public class LogsController : ControllerBase
    {
        private readonly ISysLogService _sysLogService;
        public LogsController(ISysLogService sysLogService)
        {
            _sysLogService = sysLogService;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<SysLog>>> Get(PageParam param) => await _sysLogService.GetPages(param);

        [HttpDelete("{id}")]
        public async Task<ApiResult<int>> Delete(string id) => await _sysLogService.Delete(id);
    }
}
