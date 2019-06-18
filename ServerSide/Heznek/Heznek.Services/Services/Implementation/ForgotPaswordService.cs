using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heznek.Common.Email;
using Heznek.Common.Email.Models;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Crypto;
using Heznek.Services.Models;
using Heznek.Services.Options;
using Heznek.Services.Providers;
using Microsoft.Extensions.Options;

namespace Heznek.Services.Implementation
{
    public class ForgotPaswordService : IForgotPaswordService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IAuthenticatedUser _au;
        private readonly ICryptoContext _cryptoContext;
        private readonly IAuthTokenProvider _authTokenProvider;
        private readonly ResetPasswordOptions _resetPasswordOptions;

        public ForgotPaswordService(IUnitOfWork unitOfWork,
            IAuthTokenProvider authTokenProvider,
            IEmailSender emailSender,
            IAuthenticatedUser au,
            IOptions<ResetPasswordOptions> options,
            ICryptoContext cryptoContext)
        {
            _unitOfWork = unitOfWork;
            _authTokenProvider = authTokenProvider;
            _emailSender = emailSender;
            _cryptoContext = cryptoContext;
            _resetPasswordOptions = options.Value;
            _au = au;
        }

        public string GenerateResendPaswordCode(string email)
        {
            var user = _unitOfWork.Repository<UserEntity>().Set.FirstOrDefault(x => x.Email == email);
            if(user != null)
            {
                var token = new ForgotPaswordTokenEntity
                {
                    Code = Guid.NewGuid().ToString(),
                    ExpireTime = DateTime.Now.AddMinutes(5),
                    Used = false,
                    UserId = user.Id
                };
                _unitOfWork.Repository<ForgotPaswordTokenEntity>().Insert(token);

                return token.Code;
            }
            return null;
        }

        public async Task SendForgotPasswordEmail(string email, string code)
        {
            await _emailSender.SendEmail(email, "Reset your password code", "ForgotPasswordEmail", new ResetPasswordEmail { Link = _resetPasswordOptions.ResetLink + code })
                .ConfigureAwait(false);
        }

        public TokenModel ResetPasword(ResetPaswordModel model)
        {
            var token = _unitOfWork.Repository<ForgotPaswordTokenEntity>().Include(x=>x.User).FirstOrDefault(x => x.Code == model.Code && !x.Used);

            if (token!= null)
            {
                var salt = _cryptoContext.GenerateSaltAsBase64();
                var password = Convert.ToBase64String(_cryptoContext.DeriveKey(model.Password, salt));
                token.User.Salt = salt;
                token.User.Password = password;
                token.Used = true;

                _unitOfWork.Repository<UserEntity>().Update(token.User);
                _unitOfWork.Repository<ForgotPaswordTokenEntity>().Update(token);

                return  _authTokenProvider.GetToken(token.User);
            }

            return null;
        }

        public TokenModel NewPassword(ResetPaswordModel model)
        {
            var user = _unitOfWork.Repository<UserEntity>().Set.FirstOrDefault(x => _au.Id == x.Id);
            if(user != null)
            {
                var salt = _cryptoContext.GenerateSaltAsBase64();
                var password = Convert.ToBase64String(_cryptoContext.DeriveKey(model.Password, salt));
                user.Salt = salt;
                user.Password = password;

                return _authTokenProvider.GetToken(user);
            }
            return null;
        }
    }
}
