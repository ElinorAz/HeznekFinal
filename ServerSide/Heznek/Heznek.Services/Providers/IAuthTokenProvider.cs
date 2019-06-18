using Heznek.DataAccess.Entities;
using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Providers
{
    public interface IAuthTokenProvider
    {
        TokenModel GetToken(UserEntity user);
    }
}
