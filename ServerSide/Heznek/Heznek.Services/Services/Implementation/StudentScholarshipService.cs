using System;
using System.Collections.Generic;
using System.Text;
using Heznek.Services.Models;
using Heznek.DataAccess.Entities;
using Heznek.Services.Implementation;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Providers;
using System.Threading.Tasks;
using System.Linq;

namespace Heznek.Services.Implementation
{
    public class StudentScholarshipService : AbstractService, IStudentScholarshipService
    {
        public StudentScholarshipService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser)
            : base(unitOfWork, authUser)
            {

        }

        public async Task<StudentScholarShipModel> InsertAsync(StudentScholarShipModel model)
        {
            var entity = new StudentScholarshipEntity
            {
                April = model.April,
                August = model.August,
                December = model.December,
                February = model.February,
                January = model.January,
                July = model.July,
                June = model.June,
                March = model.March,
                May = model.May,
                November = model.November,
                October = model.October,
                September = model.September,
                GivenInPast = model.GivenInPast,
                ScholarshipId = model.Scholarship.Id,
                ProfileId = model.ProfileId
            };

            await _unitOfWork.Repository<StudentScholarshipEntity>().InsertAsync(entity);
            model.Id = entity.Id;

            return model;
        }

        public async Task<StudentScholarShipModel> UpdateAsync(StudentScholarShipModel model)
        {
            var entity = _unitOfWork.Repository<StudentScholarshipEntity>().Set.FirstOrDefault(x => x.Id == model.Id);
            if (entity != null)
            {
                entity.April = model.April;
                entity.August = model.August;
                entity.December = model.December;
                entity.February = model.February;
                entity.January = model.January;
                entity.July = model.July;
                entity.June = model.June;
                entity.March = model.March;
                entity.May = model.May;
                entity.November = model.November;
                entity.October = model.October;
                entity.September = model.September;
                entity.GivenInPast = model.GivenInPast;

                await _unitOfWork.Repository<StudentScholarshipEntity>().UpdateAsync(entity);
                return model;
            }

            return null;
        }

        public List<StudentScholarShipModel> GetMy()
        {
            return GetList(_authUser.Id);
        }

            public List<StudentScholarShipModel> GetList(string userId)
        {
            return _unitOfWork.Repository<StudentScholarshipEntity>()
                .Include(x => x.Scholarship)
                .Where(x => x.Profile.UserId == userId)
                .Select(x => new StudentScholarShipModel
                {
                    Id = x.Id,
                    April = x.April,
                    August = x.August,
                    December = x.December,
                    February = x.February,
                    January = x.January,
                    July = x.July,
                    June = x.June,
                    March = x.March,
                    May = x.May,
                    November = x.November,
                    October = x.October,
                    September = x.September,
                    GivenInPast = x.GivenInPast,
                    Scholarship = new ScholarshipModel {
                           Id = x.Scholarship.Id,
                           Name = x.Scholarship.Name,
                           Admission = x.Scholarship.Admission,
                           Budget = x.Scholarship.Budget,
                           Status = x.Scholarship.Status
                    },
                    ProfileId = x.ProfileId,
                    Total = x.April + x.August + x.December + x.February + x.January + x.July + x.June + x.March + x.May + x.November + x.October + x.September
                }).ToList();
        }

        public void Delete(int id)
        {
            var entity = _unitOfWork.Repository<StudentScholarshipEntity>().Set
                .FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                using (var tran = _unitOfWork.BeginTransaction())
                {
                    _unitOfWork.Repository<StudentScholarshipEntity>().Delete(entity);
                    tran.Commit();
                }
            }
        }
    }
}
