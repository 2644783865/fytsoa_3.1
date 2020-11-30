using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Services.Api.Controllers.Sys
{
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly ISysMenuService _sysMenuService;
        public MenuController(ISysMenuService sysMenuService)
        {
            _sysMenuService = sysMenuService;
        }

        [HttpGet]
        public async Task<ApiResult<List<SysMenu>>> Get() => await _sysMenuService.GetAll();

        [HttpGet("{id}")]
        public async Task<ApiResult<SysMenu>> Get(string id) => await _sysMenuService.GetModel(id);

        [HttpPost]
        public async Task<ApiResult<int>> Post([FromBody] SysMenu model) => await _sysMenuService.Add(model);

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
