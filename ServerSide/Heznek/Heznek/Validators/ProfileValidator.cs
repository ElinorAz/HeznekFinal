using FluentValidation;
using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heznek.API.Validators
{
    public class ProfileValidator : AbstractValidator<ProfileModel>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty();

            RuleFor(x => x.LastName)
                .NotEmpty();

            RuleFor(x => x.Gender)
                .NotNull();

            RuleFor(x => x.Phone)
                .NotEmpty();
        }
    }
}
