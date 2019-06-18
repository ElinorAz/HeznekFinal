using FluentValidation;
using Heznek.API.Validators.Extensions;
using Heznek.Common.Options;
using Heznek.Services.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heznek.API.Validators
{
    public class FormTaskValidator: AbstractValidator<FormTaskModel>
    {
        public FormTaskValidator(IOptions<AllowedExtensionsOptions> options)
        {
            
            When(x => x.File != null, () => 
            {
                RuleFor(x => x.File)
                    .AllowedFileExtension(options.Value);
            });
        }
    }
}
