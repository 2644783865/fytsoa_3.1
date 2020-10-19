using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FytSoa.Infra.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FytSoa.Infra.CrossCutting
{
    public class JwtAuthService
    {
        public static string IssueJWT(JwtToken token)
        {
            var _jwtModel = AppSettingConfig.Configuration.GetSection("JwtAuth").Get<JwtModel>();
            var claims = new List<Claim>();
            claims.AddRange(new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, token.id),
                new Claim(ClaimTypes.Role,token.role)
            });
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtModel.Security));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                issuer: _jwtModel.Issuer,
                audience: _jwtModel.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtModel.WebExp),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }

    public class JwtToken
    {
        public string id { get; set; }

        public string role { get; set; }
    }

    public class JwtModel
    {
        public string Security { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int WebExp { get; set; }
    }
}
