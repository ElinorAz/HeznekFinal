using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Models;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class StatisticService : AbstractService, IStatisticService
    {
        public StatisticService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser) 
            : base(unitOfWork, authUser)
        {
        }

        public StatisticModel Get()
        {
            var result = new StatisticModel
            {
                Students = new StatisticScholarshipStudentsModel(),
                Candidates = new StatisticCandidatesModel(),
                ScholarshipCandidates = new StatisticScholarshipCandidatesModel()
            };

            var users = _unitOfWork.Repository<ProfileEntity>().Include(x=>x.VolunteerHours)//.Where(x=>x.)
                .Select(x => new
                {
                    x.City,
                    x.Gender,
                    x.General.Disabilities,
                    x.AcademicStudies.AcademicInstitution,
                    x.AcademicStudies.FieldOfStudy,
                    x.General.PsychometricGrade,
                    x.MilitaryService.TypeOfSevice,
                    x.VolunteerHours,
                    x.Status })
                .ToList();

            //Scholarship student
            var filteredUsers = users.Where(x => x.Status == UserStatusEnum.NewScholarship || x.Status == UserStatusEnum.Scholarship).ToList();

            result.Students.Cities = filteredUsers.Where(x=>!string.IsNullOrEmpty(x.City)).GroupBy(x => x.City)
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                })
                .ToList();
            result.Students.Sex = filteredUsers.Where(x => x.Gender != null).GroupBy(x => x.Gender)
                .Select(x => new KeyValue<GenderEnum, int>
                {
                    Key = x.Key.Value,
                    Value = x.Count()
                })
                .ToList();
            result.Students.Scholarships = _unitOfWork.Repository<ScholarshipEntity>().Set
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Name,
                    Value = x.Students.Count(n => n.Profile.Status == UserStatusEnum.NewScholarship || n.Profile.Status == UserStatusEnum.Scholarship)
                })
                .ToList();
            result.Students.AcademicInstitutions = filteredUsers.Where(x => !string.IsNullOrEmpty(x.AcademicInstitution)).GroupBy(x => x.AcademicInstitution)
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                })
                .ToList();
            result.Students.StudyFields = filteredUsers.Where(x => !string.IsNullOrEmpty(x.FieldOfStudy)).GroupBy(x => x.FieldOfStudy)
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                })
                .ToList();
            result.Students.DisabilityLearning = filteredUsers.Count(x => x.Disabilities);
            result.Students.FinishedVolunteerHours = filteredUsers.Where(x => x.VolunteerHours != null)
                .Where(x=>x.VolunteerHours.Sum(n=>(TimeSpan.Parse(n.Start) - TimeSpan.Parse(n.End)).Hours) >= 12)
                .Count();

            //Applicants- Active candidates
            filteredUsers = users.Where(x => x.Status == UserStatusEnum.ActiveCandidate
                            || x.Status == UserStatusEnum.InactiveCandidateTermination
                            || x.Status == UserStatusEnum.InactiveCandidateInProcess).ToList();

            result.Candidates.Cities = filteredUsers.Where(x=>!string.IsNullOrEmpty(x.City))
                .GroupBy(x => x.City)
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                })
                .ToList();

            result.Candidates.Sex = filteredUsers.Where(x => x.Gender != null).GroupBy(x => x.Gender)
                .Select(x => new KeyValue<GenderEnum, int>
                {
                    Key = x.Key.Value,
                    Value = x.Count()
                })
                .ToList();

            result.Candidates.AcademicInstitution = filteredUsers.Count(x => !string.IsNullOrEmpty(x.AcademicInstitution));
            result.Candidates.PsychometricGrade = filteredUsers.Count(x => x.PsychometricGrade > 650);
            result.Candidates.FullMilitary = filteredUsers.Count(x => x.TypeOfSevice == TypeOfSeviceEnum.FullMilitary);
            result.Candidates.DisabilityLearning = filteredUsers.Count(x => x.Disabilities);

            //Scholarships students + Active candidates
            filteredUsers = users.Where(x => x.Status == UserStatusEnum.NewScholarship
                           || x.Status == UserStatusEnum.Scholarship
                           || x.Status == UserStatusEnum.ActiveCandidate).ToList();
            result.ScholarshipCandidates.Cities = filteredUsers.Where(x => !string.IsNullOrEmpty(x.City))
                .GroupBy(x => x.City)
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                })
                .ToList();
            result.ScholarshipCandidates.Sex = filteredUsers.Where(x => x.Gender != null).GroupBy(x => x.Gender)
                .Select(x => new KeyValue<GenderEnum, int>
                {
                    Key = x.Key.Value,
                    Value = x.Count()
                })
                .ToList();
            result.ScholarshipCandidates.AcademicInstitution = filteredUsers.Count(x => !string.IsNullOrEmpty(x.AcademicInstitution));
            result.ScholarshipCandidates.StudyFields = filteredUsers.Where(x => !string.IsNullOrEmpty(x.FieldOfStudy))
                .GroupBy(x => x.FieldOfStudy)
                .Select(x => new KeyValue<string, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                })
                .ToList();
            result.ScholarshipCandidates.DisabilityLearning = filteredUsers.Count(x => x.Disabilities);

            //Users count per status
            result.Users = _unitOfWork.Repository<ProfileEntity>().Set
                .GroupBy(x => x.Status)
                .Select(x => new KeyValue<UserStatusEnum, int>
                {
                    Key = x.Key,
                    Value = x.Count()
                }).ToList();

            return result;
        }
    }
}
