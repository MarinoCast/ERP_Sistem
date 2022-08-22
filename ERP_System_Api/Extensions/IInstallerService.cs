using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ERP_System_Api
{
    public interface IInstallerService
    {
        void InstallerServices(IServiceCollection services, IConfiguration configuration);
    }
}
