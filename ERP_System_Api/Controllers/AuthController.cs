using ERP_System_Api.Controllers.BaseController;
using ERP_System_Api.Payloads.Request;
using ERP_System_Api.Services.OAuthServ;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace ERP_System_Api.Controllers
{
    public class AuthController : BaseApiController
    {
       

        public AuthController()
        {
          
        }

        [HttpPost("/SignUp")]
        public async Task<IActionResult> Register([FromBody] UserRequest request)
        {
            throw new NotImplementedException();

        }
        [HttpPost("/SingIn")]
        public  async Task<IActionResult> Login([FromBody] UserRequest request)
        {
            throw new NotImplementedException();
        }



        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
