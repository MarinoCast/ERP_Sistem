using ERP_System_Api.Helpers.Middleware;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using ERP_System_Api.Services.ServicesImpl;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERP_System_Api
{
    public class ApiServices : IInstallerService
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {


            #region AuthSvc
            services.AddTransient<IAuthService, AuthServiceImp>();
            #endregion

            #region CrudSvc
            services.AddScoped<ICrudServices<Test, TestRequest>, TestServicesImpl>();
            #endregion

            #region RuleSvc
            services.AddScoped<RuleEngine<Test>, WorkFlowEngine > ();
            #endregion


        }
    }
}
