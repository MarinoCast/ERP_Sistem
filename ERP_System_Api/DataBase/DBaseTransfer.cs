using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ERP_System_Api.DataBase
{
    public class DBaseTransfer : IdentityDbContext
    {
        public DBaseTransfer(DbContextOptions<DBaseTransfer> options) : base(options)
        {

        }

    }
}
