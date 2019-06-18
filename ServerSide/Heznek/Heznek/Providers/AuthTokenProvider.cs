using Heznek.DataAccess.Entities;
using Heznek.Services.Models;
using Heznek.Services.Options;
using Heznek.Services.Providers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.API.Providers
{
    public class AuthTokenProvider : IAuthTokenProvider
    {
        private readonly IOptions<JwtOptions> _options;

        public AuthTokenProvider(IOptions<JwtOptions> options)
        {
            _options = options;
        }

        public TokenModel GetToken(UserEntity user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new Claim[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.Now.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName + " " + user.LastName),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(ClaimTypes.Actor, user.Profile == null ? string.Empty : ((int)user.Profile.Status).ToString())
                };

            // TODO Change expiration date
            var token = new JwtSecurityToken(
              issuer: _options.Value.ValidIssuer,
              audience: _options.Value.ValidAudience,
              claims: claims,
              expires: DateTime.Now.AddMinutes(_options.Value.LifetimeInMinutes),
              signingCredentials: creds);

            return new TokenModel
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiresAt = DateTimeOffset.Now.AddYears(2),
                IssueAt = DateTimeOffset.Now,
                Id = user.Id
            };
        }
    }
}
