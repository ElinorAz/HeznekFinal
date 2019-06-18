using Heznek.Common.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly AllowedExtensionsOptions _options;
        public FileHelper(IOptions<AllowedExtensionsOptions> options)
        {
            _options = options.Value;
        }

        public async Task<bool> DeleteUserFiles(string userId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "users", userId);
            if (Directory.Exists(path))
            {
                await Task.Factory.StartNew(()=> Directory.Delete(path, true));
            }
            return true;
        }

        public async Task<string> SaveOrUpdateUserFile(IFormFile file, string oldName, string newName, string userId)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "users", userId);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            else
            {

                var oldPath = Path.Combine(path, oldName ?? string.Empty);
                if (File.Exists(oldPath))
                {
                    File.Delete(oldPath);
                }
            }

            using (var ms = new MemoryStream())
            {
                var ext = file.FileName.Split('.').LastOrDefault();

                if(!_options.Extensions.Contains(ext))
                {
                    return null;
                }

                newName = $"{newName}.{ext}";
                await file.CopyToAsync(ms).ConfigureAwait(false);
                var newPath = Path.Combine(path, newName);
                await File.WriteAllBytesAsync(newPath ,ms.ToArray()).ConfigureAwait(false);
            }

            return newName;

        }
    }
}
