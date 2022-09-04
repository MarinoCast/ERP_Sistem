using ERP_System_Api.Model;
using ERP_System_Api.Payloads.Request;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ERP_System_Api.Payloads;
using ERP_System_Api.Helpers;
using System.Text;

namespace ERP_System_Api.Services.OAuthServ
{
    public class AuthServicesImpl : IAuthServices
    {
        private readonly IConfiguration _configuration;
        public static UserAuth user = new UserAuth();
        private readonly UserManager<IdentityUser> userMgr;
        private readonly SignInManager<IdentityUser> signInMgr;
        //private readonly RoleManager<IdentityRole> rolMgr;


        public AuthServicesImpl(IConfiguration config,
            //RoleManager<IdentityRole> roleManager,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _configuration = config;
            userMgr = userManager;
            signInMgr = signInManager;
            //rolMgr = roleManager;



        }
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var User = await userMgr.FindByEmailAsync(email);

            if (User == null)
            {
                throw new Exception();
            }

            var userValidate = await userMgr.CheckPasswordAsync(User, password);
            if (!userValidate)
            {
                throw new Exception();
            }

            return new AuthResult
            {
                Message = "Obtenido"
            };
        }
        public async Task<AuthResult> RegisterAsync(string email, string username, string password)
        {
            var existingUser = await userMgr.FindByEmailAsync(email);

            if (existingUser != null)
            {
                throw new Exception();
            }
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = username,


            };
            var createUser = await userMgr.CreateAsync(newUser, password);

            if (!createUser.Succeeded)
            {
                throw new Exception();
            }
            return new AuthResult
            {
                Message = "Obtenido"
            };
        }


        public async Task<AuthResult> Logout()
        {
            await signInMgr.SignOutAsync();
            return new AuthResult
            {
                Success = true,
            };
        }

        //public async Task<AuthResult> RegisterAdmin(UserRequest request)
        //{
        //    var userExist = await userMgr.FindByNameAsync(request.UserName);
        //    if (userExist != null)
        //    {
        //        throw new Exception("Usuario no encontrado!");
        //    }
        //    //Asignacion de datos del usuario
        //    user.UserName = request.UserName;
        //    user.UserName = request.UserName;
        //    user.Password = request.Password;
        //    //Creacion del Usuario
        //    var createUser = await userMgr.CreateAsync(userExist, user.Password);
        //    if (!createUser.Succeeded)
        //    {
        //        throw new Exception("Error en la conexion! Espere unos momentos");
        //    }
        //    //Crecion y Asignacion de Roles
        //    if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await rolMgr.CreateAsync(new IdentityRole(UserRoles.Admin));
        //    }
        //    if (!await rolMgr.RoleExistsAsync(UserRoles.Employee))
        //    {
        //        await rolMgr.CreateAsync(new IdentityRole(UserRoles.Employee));
        //    }
        //    if (!await rolMgr.RoleExistsAsync(UserRoles.Owner))
        //    {
        //        await rolMgr.CreateAsync(new IdentityRole(UserRoles.Owner));
        //    }

        //    if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userMgr.AddToRoleAsync(userExist, UserRoles.Admin);
        //    }
        //    if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userMgr.AddToRoleAsync(userExist, UserRoles.Employee);
        //    }
        //    if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
        //    {
        //        await userMgr.AddToRoleAsync(userExist, UserRoles.Owner);
        //    }

        //    return new AuthResult
        //    {
        //        Success = true,
        //        Message = "Usuario Creados Sactifactoriamente!"

        //    };
        //}

        //<-------------------------Creation of Json Web Token------------------>


        public RefreshToken GetRefreshToken()
        {
            var refreshToken = new RefreshToken()
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(5),
                Created = DateTime.Now
            };
            return refreshToken;
        }


        public void SetRefreshToken(RefreshToken newRefreshToken)
        {
            AuthRequest request = new AuthRequest();
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
           // Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            request.RefreshToken = newRefreshToken.Token;
            request.TokenCreater = newRefreshToken.Created;
            request.TokenExpire = newRefreshToken.Expires;
        }

        public async Task<AuthResult> CreateToken(IdentityUser user)
        {

            var userRoles = await userMgr.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
             new Claim(ClaimTypes.Name, user.UserName),
             new Claim(JwtRegisteredClaimNames.Jti, user.Id),
             new Claim(ClaimTypes.Email, user.Email)
            };
            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }
            var key = Encoding.ASCII.GetBytes("JwtAuth:Token");

            var cred = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);


            var token = new JwtSecurityToken
            (
                claims: authClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new AuthResult
            {
                UserName = user.UserName,
                Token = jwt,
                Success = true
            };
        }

        public Task<AuthResult> RefreshSession()
        {
           
            throw new NotImplementedException();
        }
    }
}
