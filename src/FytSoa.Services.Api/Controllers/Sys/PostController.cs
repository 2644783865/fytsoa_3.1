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
    public class PostController : ControllerBase {
        private readonly ISysPostService _postService;
        public PostController (ISysPostService postService) {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ApiResult<PageResult<SysPost>>> Get (PageParam param) => await _postService.GetPages (param);

        [HttpPost]
        public async Task<ApiResult<int>> Post ([FromBody] SysPost m) => await _postService.Add (m);

        [HttpPut]
        public async Task<ApiResult<int>> Put ([FromBody] SysPost m) => await _postService.Update (m);

        [HttpDelete ("{id}")]
        public async Task<ApiResult<int>> Delete (string id) => await _postService.Delete (id);
    }
}