using ERP_System_Api.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace ERP_System_Api
{
    public class SwaggerInstallercs : IInstallerService
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v0.0", new Info { Title = "ERP_System_Api", Version = "v0.0" });

                var Security = new Dictionary<string, IEnumerable<string>>
                    {
                        {"Bearer", new string[0]}
                    };

                x.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Jwt Bearer Schema",
                    Name = "Authenticate",
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
        }
    }
}
