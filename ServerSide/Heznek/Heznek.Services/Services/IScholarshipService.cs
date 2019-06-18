using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IScholarshipService
    {
        List<ScholarshipModel> Get();
        Task<ScholarshipModel> InsertAsync(ScholarshipModel model);
        Task<ScholarshipModel> UpdateAsync(ScholarshipModel model);
        List<UserExtendedModel> StudentByScholarship(int scholarshipId);
        ScholarshipModel Get(int scholarshipId);
        void Delete(int id);
    }
}
