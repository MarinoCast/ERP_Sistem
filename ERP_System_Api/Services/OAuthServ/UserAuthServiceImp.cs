using ERP_System_Api.Helpers;
using ERP_System_Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ERP_System_Api.Services.OAuthServ
{
    public class UserAuthServiceImp : IUserAuthService
    {
        private readonly UserManager<IdentityUser> userMgr;
        private readonly IConfiguration config;

        private readonly RoleManager<IdentityRole> rolMgr;

        public UserAuthServiceImp(IConfiguration configuration, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            userMgr = userManager;
            rolMgr = roleManager;
            config = configuration;

        }
        //<---------------------Login/Request Methods-------------------------->
        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var User = await userMgr.FindByEmailAsync(email);

            if (User == null)
            {
                
            }

            var userValidate = await userMgr.CheckPasswordAsync(User, password);
            if (!userValidate)
            {
             
            }
            var Authorize = CreateToken(User);
            return Authorize;
        }

        public async Task<AuthResult> RegisterAsync(string email, string username,string password)
        {
            var existingUser = await userMgr.FindByEmailAsync(email);

            if (existingUser != null)
            {
               
            }
            var newUser = new IdentityUser
            {
                Email = email,
                UserName = username,


            };
            var createUser = await userMgr.CreateAsync(newUser, password);

            if (!createUser.Succeeded)
            {
              
            }
            var Authorize = CreateToken(newUser);
            return Authorize;
        }




        //---------------------Creation Roles Methods------------------------------------->


        public async Task<AuthResult> RegisterAdmin(UserAuth request)
        {
            var userExist = await userMgr.FindByNameAsync(request.UserName);
            if (userExist != null)
            {
                throw new Exception("Usuario no encontrado!");
            }
            //Asignacion de datos del usuario
            var user = new IdentityUser
            {
                UserName = request.UserName,
                Email = request.Email

            };
            
            //Creacion del Usuario
            var createUser = await userMgr.CreateAsync(userExist, request.Password);
            if (!createUser.Succeeded)
            {
                throw new Exception("Error en la conexion! Espere unos momentos");
            }
            //Crecion y Asignacion de Roles
            if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
            {
                await rolMgr.CreateAsync(new IdentityRole(UserRoles.Admin));
            }
            if (!await rolMgr.RoleExistsAsync(UserRoles.Employee))
            {
                await rolMgr.CreateAsync(new IdentityRole(UserRoles.Employee));
            }
            if (!await rolMgr.RoleExistsAsync(UserRoles.Owner))
            {
                await rolMgr.CreateAsync(new IdentityRole(UserRoles.Owner));
            }

            if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
            {
                await userMgr.AddToRoleAsync(userExist, UserRoles.Admin);
            }
            if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
            {
                await userMgr.AddToRoleAsync(userExist, UserRoles.Employee);
            }
            if (!await rolMgr.RoleExistsAsync(UserRoles.Admin))
            {
                await userMgr.AddToRoleAsync(userExist, UserRoles.Owner);
            }

            return new AuthResult
            {
                Success = true,
                Message = "Usuario Creados Sactifactoriamente!"

            };
        }










        //<---------------------JWT Token Methods-------------------------->

       

        public AuthResult CreateToken(IdentityUser newUser)
        {
            

            var authClaims = new List<Claim>
            {
             new Claim(ClaimTypes.Name, newUser.UserName),
             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
          var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
               config.GetSection("JwtAuth:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken
            (
                claims: authClaims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);



            return new AuthResult
            {
                UserName = newUser.UserName,
                Token = jwt,
                Success = true,
                
            };
        }

        public Task<AuthResult> RefreshTokenAsync(string Token, string refershToken)
        {
            throw new NotImplementedException();
        }
    }
}
