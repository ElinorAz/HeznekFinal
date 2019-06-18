using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Implementation
{
    public abstract class AbstractService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IAuthenticatedUser _authUser;

        public AbstractService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser)
        {
            _unitOfWork = unitOfWork;
            _authUser = authUser;
        }
    }
}
