using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services.Helpers
{
    public interface IFileHelper
    {
        Task<string> SaveOrUpdateUserFile(IFormFile file, string oldName, string newName, string userId);
        Task<bool> DeleteUserFiles(string userId);
    }
}
