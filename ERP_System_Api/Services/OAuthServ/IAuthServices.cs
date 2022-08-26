using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Services.OAuthServ
{
    public interface IAuthServices<U> where U : class
    {
        Task<AuthResult> CreateUsers(U request);
        Task<AuthResult> SignIn(U request);
        string GetUserName(string userName);
       
    }
}
