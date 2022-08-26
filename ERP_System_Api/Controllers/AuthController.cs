using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Mvc;

namespace ERP_System_Api.Controllers
{
    public class AuthController : BaseApiController
    {
        public readonly IAuthServices<UserRequest> _authServices;

        public AuthController(IAuthServices<UserRequest> authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("/SignUp")]
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            try
            {
                var response = await _authServices.CreateUsers(request);
                if (!response.Success)
                {
                   // Manejo de excepciones
                }
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            }
           
        }
        [HttpPost("/SingIn")]
        public  async Task<IActionResult> Login([FromBody] UserRequest request)
        {
            try
            {
                var response = await _authServices.SignIn(request);
                if (!ModelState.IsValid)
                {
                    ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));
                }
                if (!ModelState.IsValid)
                {
                    ModelState.Values.SelectMany(x => x.Errors.Select(xx => xx.ErrorMessage));

                }
                return Ok(response);
            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}
