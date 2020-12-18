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
    public class MenuController : ControllerBase {
        private readonly ISysMenuService _menuService;
        public MenuController (ISysMenuService menuService) {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ApiResult<List<SysMenu>>> Get (PageParam param) => await _menuService.GetAll (param);

        [HttpPost]
        public async Task<ApiResult<int>> Post ([FromBody] SysMenu m) => await _menuService.Add (m);

        [HttpPut]
        public async Task<ApiResult<int>> Put ([FromBody] SysMenu m) => await _menuService.Update (m);

        [HttpDelete ("{id}")]
        public async Task<ApiResult<int>> Delete (string id) => await _menuService.Delete (id);
    }
}