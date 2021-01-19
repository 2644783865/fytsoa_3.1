using FytSoa.Application.Interfaces;
using FytSoa.Application.ViewModels;
using FytSoa.Infra.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FytSoa.Services.Api.Controllers.Sys
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorityController : ControllerBase
    {
        private readonly ISysAuthorityService _authorityService;
        public AuthorityController(ISysAuthorityService authorityService)
        {
            _authorityService = authorityService;
        }

        [HttpPost]
        public async Task<ApiResult<int>> Post([FromBody] AuthorityMenuParam authorityMenu) => await _authorityService.AddMenu(authorityMenu);
    }
}
