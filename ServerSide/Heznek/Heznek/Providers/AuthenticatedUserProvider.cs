using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Heznek.Services.Providers;

namespace Heznek.API.Providers
{
    public class AuthenticatedUserProvider : IAuthenticatedUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthenticatedUserProvider(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public bool IsAuthenticated
        {
            get
            {
                ClaimsPrincipal principal = _httpContext.HttpContext.User;
                return principal?.Identity?.IsAuthenticated ?? false;
            }
        }

        public string Id
        {
            get
            {
                var user = _httpContext.HttpContext.User;
                if (!(user?.Identity?.IsAuthenticated) ?? false)
                {
                    return null;
                }
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value;
            }
        }

        public string Email
        {
            get
            {
                var user = _httpContext.HttpContext.User;
                return user?.Identity?.IsAuthenticated ?? false
                    ? user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value
                    : null;
            }
        }

        public string Fullname
        {
            get
            {
                var user = _httpContext.HttpContext.User;
                return user?.Identity?.IsAuthenticated ?? false
                    ? user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value
                    : null;
            }
        }
    }
}