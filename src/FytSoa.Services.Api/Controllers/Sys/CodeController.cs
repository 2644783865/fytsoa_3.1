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
    public class CodeController : ControllerBase {
        private readonly ISysCodeService _codeService;
        public CodeController (ISysCodeService codeService) {
            _codeService = codeService;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<SysCode>>> Get (PageParam param) => await _codeService.GetPages (param);

        [HttpPost]
        public async Task<ApiResult<int>> Post ([FromBody] SysCode m) => await _codeService.Add (m);

        [HttpPut]
        public async Task<ApiResult<int>> Put ([FromBody] SysCode m) => await _codeService.Update (m);

        [HttpDelete ("{id}")]
        public async Task<ApiResult<int>> Delete (string id) => await _codeService.Delete (id);
    }
}