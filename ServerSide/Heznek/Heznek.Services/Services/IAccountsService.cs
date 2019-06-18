using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IAccountsService
    {
        Task<bool> Register(RegisterModel model, string link);
        bool Confirm(string code);
        List<UserExtendedModel> GetCandidates();
        Task GrandPermission(UserModel model);
        List<UserExtendedModel> GetStudents();
        byte[] ExportCSV(string[] ids);
    }
}
