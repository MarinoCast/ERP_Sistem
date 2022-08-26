using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ERP_System_Api
{
    public static class ServicesExtension
    {
        public static void ServicesAssembly(this IServiceCollection service,IConfiguration configuration)
        {
            var extension = typeof(Program).Assembly.ExportedTypes.Where(x =>
            typeof(IInstallerService).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                    .Select(Activator.CreateInstance).Cast<IInstallerService>().ToList();

            extension.ForEach(extension => extension.InstallerServices(service, configuration));
        }

    }
}
