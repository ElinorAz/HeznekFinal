using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Heznek.Services.Models;

namespace Heznek.API.Validators.Token
{
    public class LoginCredentialsValidator : AbstractValidator<LoginCredentials>
    {
        public LoginCredentialsValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Invalid ID");

        }
    }
}