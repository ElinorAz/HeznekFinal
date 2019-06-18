using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Heznek.Common.DomainTaskStatus;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Crypto;
using Heznek.Services.Models;
using Heznek.Services.Options;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoContext _cryptoContext;
        private readonly DomainTaskStatus _taskStatus;
        private readonly IOptions<JwtOptions> _options;
        private readonly IAuthTokenProvider _authTokenProvider;

        public TokenService(IUnitOfWork unitOfWork, ICryptoContext cryptoContext, DomainTaskStatus taskStatus, IOptions<JwtOptions> options, IAuthTokenProvider authTokenProvider)
        {
            _unitOfWork = unitOfWork;
            _cryptoContext = cryptoContext;
            _taskStatus = taskStatus;
            _options = options;
            _authTokenProvider = authTokenProvider;
        }

        public TokenModel GetToken(LoginCredentials loginCredentials)
        {
            var user = _unitOfWork.Repository<UserEntity>().Include(x=>x.Profile).FirstOrDefault(x => x.Id == loginCredentials.Id && x.Confirmation.Confirmed);

            if (user == null)
            {
                return null;
            }

            if (_cryptoContext.ArePasswordsEqual(loginCredentials.Password, user.Password, user.Salt))
            {
                return _authTokenProvider.GetToken(user);
            }

            return null;
        }

    }
}