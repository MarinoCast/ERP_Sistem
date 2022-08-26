using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERP_System_Api
{
    public class ApiServices : IInstallerService
    {
        
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {

            //Dependecy Injection Services Methods
            services.AddScoped<IAuthServices<UserRequest>, AuthServicesImpl>();
            //services.AddTransient<ICrudServices>();





        }
    }
}
