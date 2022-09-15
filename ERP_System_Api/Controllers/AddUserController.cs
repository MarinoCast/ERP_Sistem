using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Model;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwlHR.Payloads.Response;

namespace ERP_System_Api.Controllers
{
    public class AddUserController : BaseApiController
    {
        private readonly IAuthService AuthSvc;

        public AddUserController(IAuthService authService)
        {
            AuthSvc = authService;
        }

        [HttpPost("/CreateUser"), Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateUsers([FromBody] UserAuth userAuth)
        {
            var defaultEmail = userAuth.UserName + "@owlAgency.com";
            var AuthResponse = await AuthSvc.RegisterAsync(defaultEmail, userAuth.UserName, userAuth.Password);

            if (AuthResponse.Success)
            {
                AuthResponse = await AuthSvc.RegisterAdmin(userAuth);
            }
            if (!AuthResponse.Success)
            {
                throw new Exception("Error en la solicitud");

            }

            return Ok(AuthResponse);
        }
      

    }
}
