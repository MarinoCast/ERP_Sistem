using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERP_System_Api.Services.OAuthServ
{
    public interface IAuthService
    {

        Task<AuthResult> RegisterAsync(string email, string username, string password);
        Task<AuthResult> LoginAsync(string username, string password);
        Task<AuthResult> RegisterAdmin(UserAuth userAuth);
      
    }

}
