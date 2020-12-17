using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FytSoa.Application.Interfaces;
using FytSoa.Domain.Models.Sys;
using FytSoa.Infra.Common;
using Microsoft.AspNetCore.Mvc;

namespace FytSoa.Services.Api.Controllers.Sys {
    [Route ("api/[controller]")]
    public class RoleController : ControllerBase {
        private readonly ISysRoleService _roleService;
        public RoleController (ISysRoleService roleService) {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<ApiResult<List<SysRole>>> Get (PageParam param) => await _roleService.GetAll (param);

        [HttpPost]
        public async Task<ApiResult<int>> Post ([FromBody] SysRole m) => await _roleService.Add (m);

        [HttpPut]
        public async Task<ApiResult<int>> Put ([FromBody] SysRole m) => await _roleService.Update (m);

        [HttpDelete ("{id}")]
        public async Task<ApiResult<int>> Delete (string id) => await _roleService.Delete (id);
    }
}