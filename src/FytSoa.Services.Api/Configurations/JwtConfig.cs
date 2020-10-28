using System;
using System.Text;
using FytSoa.Infra.Common;
using FytSoa.Infra.CrossCutting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using FytSoa.Application;

namespace FytSoa.Services.Api.Configurations
{
    public static class JwtConfig
    {
        public static void AddJwtConfiguration(this IServiceCollection services)
        {
            services.Configure<JwtModel>(AppSettingConfig.Configuration.GetSection("JwtAuth"));
            var token = AppSettingConfig.Configuration.GetSection("JwtAuth").Get<JwtModel>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(token.Security)),
                    ValidIssuer = token.Issuer,
                    ValidAudience = token.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime = true
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("App", policy => policy.RequireRole("App").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
            });
        }

        public static void UseJwtSetup(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            // global error
            app.UseMiddleware<ExceptionFilter>();

            // check jwt token
            app.UseMiddleware<JwtTokenFilter>();

            // authentication
            app.UseAuthentication();
        }
    }
}
