using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Extensions;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Models;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class ScholarshipService : AbstractService, IScholarshipService
    {
        public ScholarshipService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser) 
            : base(unitOfWork, authUser)
        {

        }

        public List<ScholarshipModel> Get()
        {
            return _unitOfWork.Repository<ScholarshipEntity>().Set
                .Select(x => new ScholarshipModel
                {
                    Id = x.Id,
                    Admission = x.Admission,
                    Budget = x.Budget,
                    Name = x.Name,
                    Status = x.Status,
                    StundentsCount = x.Students.Select(n=>n.ProfileId).Distinct().Count()
                })
                .ToList();
        }

        public async Task<ScholarshipModel> InsertAsync(ScholarshipModel model)
        {
            var entity = new ScholarshipEntity
            {
                Admission = model.Admission,
                Budget = model.Budget,
                Status = model.Status,
                Name = model.Name
            };

            await _unitOfWork.Repository<ScholarshipEntity>().InsertAsync(entity);
            model.Id = entity.Id;

            return model;
        }

        public async Task<ScholarshipModel> UpdateAsync(ScholarshipModel model)
        {
            var entity = _unitOfWork.Repository<ScholarshipEntity>().Set.FirstOrDefault(x => x.Id == model.Id);
            if (entity != null)
            {
                entity.Admission = model.Admission;
                entity.Budget = model.Budget;
                entity.Status = model.Status;
                entity.Name = model.Name;

                await _unitOfWork.Repository<ScholarshipEntity>().UpdateAsync(entity);
                return model;
            }

            return null;
        }

        public List<UserExtendedModel> StudentByScholarship(int scholarshipId)
        {
            return _unitOfWork.Repository<ProfileEntity>().Set
                .Where(x => x.Scholarships.Any(n=>n.ScholarshipId == scholarshipId))
                .Select(x => new UserExtendedModel
                {
                    Id = x.UserId,
                    Email = x.User.Email,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Phone = x.Phone,
                    Status = x.Status,
                    Domain = x.AcademicStudies == null ? null : x.AcademicStudies.FieldOfStudy,
                    Faculty = x.AcademicStudies == null ? null : x.AcademicStudies.AcademicDegree,
                    University = x.AcademicStudies == null ? null : x.AcademicStudies.AcademicInstitution
                })
                .ToList();
        }

        public ScholarshipModel Get(int scholarshipId)
        {
            return _unitOfWork.Repository<ScholarshipEntity>().Set
                .IncludeEntity(x=>x.Students)
                    .ThenIncludeEntity(x=>x.Profile)
                        .ThenIncludeEntity(x=>x.User)
                .Where(x => x.Id == scholarshipId)
                .Select(x => new ScholarshipModel
                {
                    Id = x.Id,
                    Admission = x.Admission,
                    Budget = x.Budget,
                    Name = x.Name,
                    Status = x.Status,
                    Students = x.Students.Select(n => new UserExtendedModel
                    {
                        Id = n.Profile.UserId,
                        Email = n.Profile.User.Email,
                        FirstName = n.Profile.User.FirstName,
                        LastName = n.Profile.User.LastName,
                        Phone = n.Profile.Phone,
                        Status = n.Profile.Status
                    }).Distinct().ToList()
                })
                .FirstOrDefault();
        }

        public void Delete(int id)
        {
            var entity = _unitOfWork.Repository<ScholarshipEntity>()
                .Include(x => x.Students)
                .FirstOrDefault(x => x.Id == id);
            if (entity != null)
            {
                using (var tran = _unitOfWork.BeginTransaction())
                {
                    _unitOfWork.Repository<StudentScholarshipEntity>().DeleteRange(entity.Students);
                    _unitOfWork.Repository<ScholarshipEntity>().Delete(entity);
                    tran.Commit();
                }
            }
        }
    }
}
