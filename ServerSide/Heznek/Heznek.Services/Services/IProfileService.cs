using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IProfileService
    {
        Task<ProfileModel> Update(ProfileModel model);
        /// <summary>
        /// Getting own profile
        /// </summary>
        /// <returns></returns>
        ProfileModel Get();
        ProfileModel Get(string userId);

        Task<ProfileModel> AdminUpdate(ProfileModel model);

        Task<ProfileModel> Update(ProfileModel model, string userId);
        
        Task Delete(string userId);

        SystemDetailsModel UpdateSystemDetails(SystemDetailsModel model);

        SystemDetailsModel GetSystemDetails();

        Task<ProfileModel> Create(ProfileModel model);
    }
}
