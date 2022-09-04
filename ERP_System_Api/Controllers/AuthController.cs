using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Mvc;
using OwlHR.Payloads.Response;
using System.Security.Cryptography;

namespace ERP_System_Api.Controllers
{
    [ApiController]
    [Route("[api/controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServices _autServices;
        private readonly IUserAuthService _userAuthService;

        public AuthController(IAuthServices authServices, IUserAuthService userAuthService)
        {
            _autServices = authServices;
            _userAuthService = userAuthService;
        }

        [HttpPost("/SignUp")]
        public async Task<IActionResult> Register([FromBody] UserAuth userAuth)
        {
            var AuthResponse = await _userAuthService.RegisterAsync(userAuth.Email, userAuth.UserName, userAuth.Password);

            if (!AuthResponse.Success)
            {
                return BadRequest(new FailAuthResponse
                {
                   
                });
            }

            return Ok(new AuthSuccessResponse
            {
                UserName = AuthResponse.UserName,
                Token = AuthResponse.Token,
            });
        }
        //[HttpPost("/Register-Admin")]
        //public async Task<IActionResult> RegisterAdmin([FromBody] UserRequest request)
        //{
        //    var response = await _autServices.RegisterAdmin(request);
        //    if (response == null)
        //    {
        //        throw new Exception("Error en la solicitud");
        //    }
        //    return Ok(response);
        //}
        [HttpPost("/SingIn")]
        public async Task<IActionResult> Login([FromBody] UserAuth userAuth)
        {
            var AuthResponse = await _userAuthService.LoginAsync(userAuth.Email, userAuth.Password);

            if (!ModelState.IsValid)
            {
                ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));

            }
            if (!AuthResponse.Success)
            {
                return BadRequest(new FailAuthResponse
                {
                });
            }


            return Ok(new AuthSuccessResponse
            {
                UserName = AuthResponse.UserName,
                Token = AuthResponse.Token,
               
            });



        }

        [HttpGet("/LogOut")]
        public async Task<IActionResult> Logout()
        {
            var response = await _autServices.Logout();

            return Ok(response);
        }

    }
}
