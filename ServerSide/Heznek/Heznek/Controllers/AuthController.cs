using Microsoft.AspNetCore.Mvc;
using Heznek.Common.DomainTaskStatus;
using Heznek.Services;
using Heznek.Services.Models;

namespace Heznek.API.Controllers
{
    public class AuthController : AbstractController
    {
        private readonly ITokenService _tokenService;

        public AuthController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TokenModel), 200)]
        public IActionResult AccessToken([FromBody] LoginCredentials loginCredentials)
        {
            var response = _tokenService.GetToken(loginCredentials);
            return response == null? BadRequest("Invalid credentials") : (IActionResult)Ok(response);
        }
    }
}