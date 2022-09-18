using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlHR.Payloads.Response;
using System.Security.Cryptography;

namespace ERP_System_Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[api/controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService userAuthService)
        {
            _AuthService = userAuthService;
        }
        [HttpPost("/SingIn")]
        public async Task<IActionResult> Login([FromBody] UserAuth userAuth)
        {
            var AuthResponse = await _AuthService.LoginAsync(userAuth.UserName, userAuth.Password);

            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));

            }
            if (!AuthResponse.Success)
            {
                throw new Exception();
            }


            return Ok(AuthResponse);

        }
        

    }
}
