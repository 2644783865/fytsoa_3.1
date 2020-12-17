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
    public class OrganizeController : ControllerBase
    {
        private readonly ISysOrganizeService _sysOrganizeService;
        public OrganizeController(ISysOrganizeService sysOrganizeService)
        {
            _sysOrganizeService = sysOrganizeService;
        }

        [HttpGet]
        public async Task<ApiResult<List<SysOrganize>>> Get(PageParam param) => await _sysOrganizeService.GetAll(param);

        [HttpPost]
        public async Task<ApiResult<int>> Post([FromBody] SysOrganize m) => await _sysOrganizeService.Add(m);

        [HttpPut]
        public async Task<ApiResult<int>> Put([FromBody] SysOrganize m) => await _sysOrganizeService.Update(m);

        [HttpDelete("{id}")]
        public async Task<ApiResult<int>> Delete(string id) => await _sysOrganizeService.Delete(id);
    }
}
