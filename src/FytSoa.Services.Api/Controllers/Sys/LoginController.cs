using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace FytSoa.Services.Api.Controllers.Sys
{
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        
        [HttpPost]
        public IActionResult Post([FromBody] string username, [FromBody] string password)
        {
            return Ok(new { code=200, msg="success" , data =new { accessToken="admin-accessToken" } });
        }

        [HttpPost("userInfo")]
        public IActionResult UserInfo()
        {
            return Ok(new { code = 200, msg = "success", data = new { username = "admin", permissions=new List<string>() {"admin" } }, avatar= "https://i.gtimg.cn/club/item/face/img/2/15922_100.gif" });
        }
    }
}
