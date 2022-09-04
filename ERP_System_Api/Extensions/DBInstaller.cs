using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ERP_System_Api.DataBase;
using ERP_System_Api.Model;

namespace ERP_System_Api
{
    #region Database Configuration
    public class DBInstaller : IInstallerService
    {
        public void InstallerServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("ConnectionSQL")));
           

            //Dependecy Injection Services Identity

            services.AddIdentity<IdentityUser, IdentityRole>(op =>
            {

                // Password settings.
                op.Password.RequireDigit = false;
                op.Password.RequireLowercase = false;
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequireUppercase = false;
                op.Password.RequiredLength = 6;
                op.Password.RequiredUniqueChars = 0;

                

                // User settings.
                op.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                op.User.RequireUniqueEmail = false;

            }).AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();


        

        }
    }
    #endregion
}
