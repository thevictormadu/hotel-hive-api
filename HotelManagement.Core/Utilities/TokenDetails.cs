using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.Utilities
{
    public class TokenDetails: ITokenDetails
    {
        private readonly IHttpContextAccessor _httpContext;

        public TokenDetails(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetId() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string GetUserName() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        public string GetRoles() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
    }
}
