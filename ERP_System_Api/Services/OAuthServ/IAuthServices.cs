﻿using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Services.OAuthServ
{
    public interface IAuthServices
    {
        Task<AuthResult> RegisterAsync(string email, string username, string password);
        Task<AuthResult> LoginAsync(string email, string password);
        Task<AuthResult> Logout();

        //Task<AuthResult> RegisterAdmin(UserRequest request);
        Task<AuthResult> RefreshSession();
    }
}
