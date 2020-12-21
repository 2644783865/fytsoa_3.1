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
    public class AdminController : ControllerBase {
        private readonly ISysAdminService _adminService;
        public AdminController (ISysAdminService adminService) {
            _adminService = adminService;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<SysAdmin>>> Get (PageParam param) => await _adminService.GetPages (param);

        [HttpPost]
        public async Task<ApiResult<int>> Post ([FromBody] SysAdmin m) => await _adminService.Add (m);

        [HttpPut]
        public async Task<ApiResult<int>> Put ([FromBody] SysAdmin m) => await _adminService.Update (m);

        [HttpDelete ("{id}")]
        public async Task<ApiResult<int>> Delete (string id) => await _adminService.Delete (id);
    }
}