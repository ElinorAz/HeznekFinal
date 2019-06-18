using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Heznek.Services
{
    public interface IStudentScholarshipService
    {
        Task<StudentScholarShipModel> InsertAsync(StudentScholarShipModel model);
        Task<StudentScholarShipModel> UpdateAsync(StudentScholarShipModel model);
        void Delete(int id);
        List<StudentScholarShipModel> GetList(string userId);
        List<StudentScholarShipModel> GetMy();
    }
}
