using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IFormService
    {
        FormEntity GenerateForm(string userId);
        FormModel GetForm();
        FormModel GetForm(string userId);
        Task SendForm();
        Task ChangeFormStatus(string UserId, FormStatusEnum status);
        Task<FormParentsSalaryModel> UpdateParentsSalary(FormParentsSalaryModel model);
        Task<FormTaskModel> UpdateTask(FormTaskModel model);
    }
}
