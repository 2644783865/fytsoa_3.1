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
    public class CodeController : ControllerBase
    {
        private readonly ISysCodeService _codeService;
        private readonly ISysCodeTypeService _codeTypeService;
        public CodeController(ISysCodeService codeService
        , ISysCodeTypeService codeTypeService)
        {
            _codeService = codeService;
            _codeTypeService = codeTypeService;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<SysCode>>> Get(PageParam param) => await _codeService.GetPages(param);

        [HttpGet("column")]
        public async Task<ApiResult<List<SysCodeType>>> GetColumn(PageParam param) => await _codeTypeService.GetList(param);

        [HttpPost]
        public async Task<ApiResult<int>> Post([FromBody] SysCode m) => await _codeService.Add(m);

        [HttpPost("column")]
        public async Task<ApiResult<int>> PostColumn([FromBody] SysCodeType m) => await _codeTypeService.Add(m);

        [HttpPut]
        public async Task<ApiResult<int>> Put([FromBody] SysCode m) => await _codeService.Update(m);

        [HttpPut("column")]
        public async Task<ApiResult<int>> PutColumn([FromBody] SysCodeType m) => await _codeTypeService.Update(m);

        [HttpDelete("{id}")]
        public async Task<ApiResult<int>> Delete(string id) => await _codeService.Delete(id);

        [HttpDelete("column/{id}")]
        public async Task<ApiResult<int>> DeleteColumn(string id) => await _codeTypeService.Delete(id);
    }
}