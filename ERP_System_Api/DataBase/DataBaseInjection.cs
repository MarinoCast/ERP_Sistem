using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ERP_System_Api
{
    public class DataBaseInjection : IInstallerService
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<DBaseTransfer>(options =>
            //options.(configuration.GetConnectionString("ConnectionSQL")));

        }
    }
}
