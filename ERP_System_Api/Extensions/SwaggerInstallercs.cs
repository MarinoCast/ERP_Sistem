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
           // configure strongly typed settings object
            services.Configure<JwtSettings>(configuration.GetSection("JwtAuth"));

            // configure DI for application services
           
            var key = Encoding.ASCII.GetBytes("JwtAuth:Token");
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(jwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            var tokenValidationParameters = new TokenValidationParameters
            {

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false,
                RequireExpirationTime = false,
                ValidateLifetime = true

            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(x =>
              {

                  x.SaveToken = true;
                  x.TokenValidationParameters = tokenValidationParameters;
              });

            services.AddSwaggerGen(x =>
            {

                x.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme()
                {
                    Description = "JWT Authorization header using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                //    x.AddSecurityRequirement(new OpenApiSecurityRequirement
                //        {
                //            {new OpenApiSecurityScheme{Reference = new OpenApiReference
                //            {
                //                Id = "Bearer",
                //                Type = ReferenceType.SecurityScheme
                //        }},
                //                new string[] {}
                //            }
                //        });
            });


        }
    }
}
