using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IForgotPaswordService
    {
        string GenerateResendPaswordCode(string email);
        Task SendForgotPasswordEmail(string email, string code);
        TokenModel ResetPasword(ResetPaswordModel model);
        TokenModel NewPassword(ResetPaswordModel model);
    }
}
