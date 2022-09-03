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
        private readonly TokenValidationParameters tokenParams;
        private readonly RoleManager<IdentityRole> rolMgr;

        public UserAuthServiceImp(UserManager<IdentityUser> userManager, TokenValidationParameters tokenValidationParameters, RoleManager<IdentityRole> roleManager)
        {
            userMgr = userManager;
            tokenParams = tokenValidationParameters;
            rolMgr = roleManager;

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

        //public async Task<AuthResult> RefreshTokenAsync(string Token, string refreshToken)
        //{

        //    var validatedToken = getPrincipalToken(Token);

        //    if(validatedToken == null)
        //    {
        //    }
        //    var exiryDateUnix = long.Parse(validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        //    var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)
        //        .AddSeconds(exiryDateUnix);


        //    if(expiryDateTimeUtc > DateTime.UtcNow)
        //    {
        //    }

        //    var jti = validatedToken.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        //    var storeRefreshToken =  _context.RefershTokens.SingleOrDefault(x => x.Token == refreshToken);

        //    if (storeRefreshToken == null)
        //    {
        //        return new AuthResult { Errors = new[] { "This token hasn't expired yet" } };
        //    }
        //    if(DateTime.UtcNow > storeRefreshToken.ExpiryDate)
        //    {
        //        return new AuthResult { Errors = new[] { "This refresh token has expired" } };

        //    }
        //    if (storeRefreshToken.Invalidated)
        //    {
        //        return new AuthResult { Errors = new[] { "This refresh token has been invalidated" } };
        //    }
        //    if (storeRefreshToken.Used)
        //    {
        //        return new AuthResult { Errors = new[] { "This refresh token has been used" } };
        //    }
        //    if(storeRefreshToken.JwtId != jti)
        //    {
        //        return new AuthResult { Errors = new[] { "This refresh token doesn't match this Jwt" } };
        //    }
        //    storeRefreshToken.Used = true;
        //    _context.RefershTokens.Update(storeRefreshToken);
        //    await _context.SaveChangesAsync();

        //    var user = await userMgr.FindByIdAsync(validatedToken.Claims.Single(x => x.Type == "id").Value);
        //    return CreateToken(user);

        //}


        private static AuthResult CreateToken(IdentityUser newUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("JwtSettings:Secret");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, newUser.UserName),
                    new Claim(ClaimTypes.Email, newUser.Email),

                    new Claim("id", newUser.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return new AuthResult
            {
                UserName = newUser.UserName,
                Token = tokenHandler.WriteToken(token),
                Success = true,
                
            };
        }

        public Task<AuthResult> RefreshTokenAsync(string Token, string refershToken)
        {
            throw new NotImplementedException();
        }
    }
}
