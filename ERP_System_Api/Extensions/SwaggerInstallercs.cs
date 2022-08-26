using ERP_System_Api.Controllers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
                x.SwaggerDoc("v0", new Info { Title = "ERP_System_Api", Version = "Version 0.1" });

                var Security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Authorization", new string[0]}
                    };


                x.AddSecurityDefinition("OAuth2", new OpenApiSecurityScheme()
                {
                    Description = "Jwt Authorization header using the Bearer scheme (\"bearer {token}\")",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,


                });

                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {new OpenApiSecurityScheme{Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                    }},
                            new string[] {}
                        }
                    });
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(configuration.GetSection("JwtAuth:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddCors(options => options.AddPolicy(name: "NgOrigins",
            policy =>
            {
                policy.WithOrigins("https://localhost:44359/").AllowAnyMethod().AllowAnyHeader();
            }));

        }
    }
}
