using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using HotelManagement.Core.Domains;
using System.Net.Http;
using Microsoft.AspNetCore.Http;

namespace HotelManagement.Core.Utilities
{
    public class Token : IToken
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContext;

        public Token(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
        }

        public object CreateToken(UserModel user)
        {
            JwtSecurityToken token = null;

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]));

            token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:ValidIssuer"],
            audience: _configuration["JwtSettings:ValidAudience"],
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            };
        }

        public RefreshToken SetRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Refreshtoken = Guid.NewGuid(),
                RefreshTokenExpiryTime = DateTime.Now.AddDays(1)
            };
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = refreshToken.RefreshTokenExpiryTime
            };
            _httpContext.HttpContext.Response.Cookies.Append("refresh-token", refreshToken.Refreshtoken.ToString(), cookieOptions);
            return refreshToken;
        }
    }
}
