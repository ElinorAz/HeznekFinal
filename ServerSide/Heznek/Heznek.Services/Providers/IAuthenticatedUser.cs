using System;

namespace Heznek.Services.Providers
{
    public interface IAuthenticatedUser
    {
        bool IsAuthenticated { get; }
        string Fullname { get; }
        string Id { get; }
        string Email { get; }
    }
}