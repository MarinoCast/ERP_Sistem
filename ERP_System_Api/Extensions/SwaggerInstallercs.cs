using ERP_System_Api.Controllers;
using ERP_System_Api.DataBase;
using ERP_System_Api.Helpers.JwtHelpers;
using ERP_System_Api.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Text;

namespace ERP_System_Api
{
    public class SwaggerInstallercs : IInstallerService
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                x.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                 .AddJwtBearer(x =>
                 {
                     x.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                             .GetBytes(configuration.GetSection("JwtAuth:Token").Value)),
                         ValidateIssuer = false,
                         ValidateAudience = false
                     };
                 });


        }
    }
}
