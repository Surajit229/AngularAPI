using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Rota.Business.Interfaces;
using Rota.Model.RequestModels;
using Rota.Utility;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rota.API.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [EnableCors("AllowSpecificOrigins")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IRotaService _rotaService;

        public AccountController(IRotaService _rotaService)
        {
            this._rotaService = _rotaService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public ActionResult Login(LoginInput model)
        {
            //  This line is only for test loaded in login page.
            System.Threading.Thread.Sleep(5000);

            var response = _rotaService.Login(model);
            if (response.ResponseCode == (int)Enums.StatusCode.OK)
            {
                var token = GenerateJSONWebToken(response.Response);
                return Ok(new { Authentication = response, Token = token });
            }
            return BadRequest(response);
        }

        [HttpGet]
        [Route("Test")]
        public ActionResult Test()
        {
            return Ok("User authenticated to invoke Test API.");
        }

        private string GenerateJSONWebToken(dynamic userInfo)
        {
            if (userInfo != null)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new  Claim("UserId", string.IsNullOrEmpty(Convert.ToString(userInfo.UserId)) ? string.Empty : Convert.ToString(userInfo.UserId)),
                    new  Claim("Email", string.IsNullOrEmpty(Convert.ToString(userInfo.Email)) ? string.Empty : Convert.ToString(userInfo.Email)),
                    new  Claim("FullName", string.IsNullOrEmpty(Convert.ToString(userInfo.FullName)) ? string.Empty : Convert.ToString(userInfo.FullName)),
                    new  Claim("UserRole", string.IsNullOrEmpty(Convert.ToString(userInfo.UserRole)) ? string.Empty : Convert.ToString(userInfo.UserRole)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(AppSettings.JwtIssuer, AppSettings.JwtIssuer, claims, expires: DateTime.Now.AddMinutes(Convert.ToInt32(AppSettings.TokenExpiry)), signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
