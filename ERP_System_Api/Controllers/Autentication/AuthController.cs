using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlHR.Payloads.Response;
using System.Security.Cryptography;

namespace ERP_System_Api.Controllers.Autentication
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

        [HttpPost("/SingUp")]
        public async Task<IActionResult> Register([FromBody] UserAuth userAuth, string email)
        {

            if (email.Equals("marino@owlAgency.com"))
            {
                var AuthResponse = await _AuthService.RegisterAsync(email, userAuth.UserName, userAuth.Password);
                if (AuthResponse.Success)
                {
                    AuthResponse = await _AuthService.RegisterAdmin(userAuth);
                }
                if (!AuthResponse.Success)
                {
                    throw new Exception("Error en la solicitud");

                }

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
            throw new Exception();


        }


    }
}
