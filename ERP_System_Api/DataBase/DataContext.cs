using ERP_System_Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERP_System_Api.DataBase
{
    public class DataContext : IdentityDbContext<IdentityUser>
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {

        }
        public DbSet<Test> Test { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Processing> Processing { get; set; }

        public DbSet<Billing> Billings { get; set; }


    }
}
