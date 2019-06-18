using System;
using Heznek.Services.Models;

namespace Heznek.Services
{
    public interface ITokenService
    {
        TokenModel GetToken(LoginCredentials loginCredentials);
    }
}