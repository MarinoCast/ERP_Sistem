using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ERP_System_Api.DataBase;

namespace ERP_System_Api
{
    public class DBInstaller : IInstallerService
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DBaseTransfer>(db =>
            db.UseSqlServer(configuration.GetConnectionString("DBaseTransfer")));

            
        }
    }
}
