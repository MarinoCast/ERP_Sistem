using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using ERP_System_Api.Services.OAuthServ;

namespace ERP_System_Api.Helpers.JwtHelpers
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JwtSettings _jwtSettings;
        private readonly IServiceCollection services;
        public JwtMiddleware(RequestDelegate next, IOptions<JwtSettings> jwtSetting)
        {
            _next = next;
            _jwtSettings = jwtSetting.Value;

        }
        public async Task Invoke (HttpContext context, IUserAuthService userAuthService) 
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault();

            if (token != null)
                attachUserToContext(context, userAuthService, token);

            await _next(context);
        }
        private void attachUserToContext(HttpContext context, IUserAuthService userAuthService, string token)
        {
            try
            {

                var key = Encoding.ASCII.GetBytes("JwtSettings:Secret");
                

                services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                  .AddJwtBearer(x =>
                  {

                      x.SaveToken = true;
                      x.TokenValidationParameters = new TokenValidationParameters
                      {

                          ValidateIssuerSigningKey = true,
                          IssuerSigningKey = new SymmetricSecurityKey(key),
                          ValidateAudience = false,
                          ValidateIssuer = false,
                          RequireExpirationTime = false,
                          ValidateLifetime = true

                      };
                  });
            }
            catch
            {
                // do nothing if jwt validation fails
                // user is not attached to context so request won't have access to secure routes
            }
        }
    }
}
