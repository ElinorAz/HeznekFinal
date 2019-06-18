using FluentValidation;
using Heznek.Common.Options;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heznek.API.Validators.Extensions
{
    public static class HeznekValidatorExtension
    {
        public static IRuleBuilderOptions<T, TProperty> AllowedFileExtension<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, AllowedExtensionsOptions options) where TProperty : IFormFile
        {
            return ruleBuilder
                .Must(x=>!string.IsNullOrEmpty(x.FileName))
                .Must(x => options.Extensions.Contains(x.FileName.Split('.').LastOrDefault()))
                .WithMessage($"Filename empty or Forbidden file extension. Allowed extension: {string.Join(", ", options.Extensions)}");
        }
    }
}
