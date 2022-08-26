﻿using ERP_System_Api.Model;
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

namespace ERP_System_Api.Services.OAuthServ
{
    public class AuthServicesImpl : IAuthServices<UserRequest>
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<UserRequest> _userManager;


        public AuthServicesImpl(IConfiguration config, UserManager<UserRequest> userManager)
        {
            _configuration = config;
            _userManager = userManager;

        }

        public async Task<AuthResult> CreateUsers(UserRequest request)
        {
            try
            {
                var newUser = await _userManager.FindByNameAsync(request.UserName);
                if (newUser == null)
                {

                }
                var createUser = await _userManager.CreateAsync(newUser, request.Password);
                if (!createUser.Succeeded)
                {

                }

                var result = CreateToken(newUser);

                return result;

            }

            catch (Exception)
            {

                throw;
            }

            
        }

        public string GetUserName(string username)
        {
            try
            {
                var user =  _userManager.FindByNameAsync(username);
                if(user == null)
                {

                }

                return user.ToString(); 
               

            }

            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<AuthResult> SignIn(UserRequest request)
        {
            try
            {
                var existUser = await _userManager.FindByNameAsync(request.UserName);
                if (existUser == null)
                {

                }
                var userValidate = await _userManager.CheckPasswordAsync(existUser, request.Password);
                if (!userValidate)
                {

                }
                var result = CreateToken(existUser);

                return result;

            }
            catch (Exception)
            {

                throw;
            }
        }





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

        public AuthResult CreateToken(UserRequest request)
        {
            List<Claim> claims = new List<Claim>
            {
             new Claim(ClaimTypes.Name, request.UserName),
             new Claim(ClaimTypes.Role, request.roles.name.ToString())
            };
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtAuth:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            var token = new JwtSecurityToken
            (
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
            );
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
             
            return new AuthResult
            {
                UserName =  request.UserName,
                Token = jwt,
                Success = true
            };
        }

       
    }
}