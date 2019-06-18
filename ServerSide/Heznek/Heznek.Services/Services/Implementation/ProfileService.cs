using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Helpers;
using Heznek.Services.Models;
using Heznek.Services.Providers;
using Heznek.Services.Crypto;

namespace Heznek.Services.Implementation
{
    public class ProfileService : AbstractService, IProfileService
    {
        private readonly IFileHelper _fileHelper;
        private readonly ICryptoContext _cryptoContext;
        private readonly IAuthTokenProvider _authTokenProvider;
        private readonly IFormService _formService;

        public ProfileService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser, IFileHelper fileHelper, ICryptoContext cryptoContext, IAuthTokenProvider authTokenProvider, IFormService formService) 
            : base(unitOfWork, authUser)
        {
            _fileHelper = fileHelper;
            _cryptoContext = cryptoContext;
            _authTokenProvider = authTokenProvider;
            _formService = formService;
        }

        public ProfileModel Get(string userId)
        {
            var result = _unitOfWork.Repository<ProfileEntity>()
               .Include(x => x.User,
                        x => x.AcademicStudies,
                        x => x.CandidateAdditionalData,
                        x => x.General,
                        x => x.HighSchool,
                        x => x.BankInfo,
                        x => x.ScholarDetails,
                        x => x.Scholarships,
                        x => x.VolunteerDetails,
                        x => x.MilitaryService,
                        x => x.Telephony,
                        x => x.VolunteerHours)
               .Select(x => new ProfileModel
               {
                   Id = x.Id,
                   UserId = x.UserId,
                   Email = x.User.Email,
                   FirstName = x.User.FirstName,
                   LastName = x.User.LastName,
                   City = x.City,
                   Gender = x.Gender,
                   BirthDate = x.BirthDate,
                   Address = x.Address,
                   Siblings = x.Siblings,
                   Phone = x.Phone,
                   AcademicParents = x.AcademicParents,
                   Status = x.Status,
                   AcademicStudies = x.AcademicStudies == null ? null : new AcademicStudiesModel
                   {
                       AcademicDegree = x.AcademicStudies.AcademicDegree,
                       AcademicInstitution = x.AcademicStudies.AcademicInstitution,
                       FieldOfStudy = x.AcademicStudies.FieldOfStudy,
                       GraduationYear = x.AcademicStudies.GraduationYear,
                       Residence = x.AcademicStudies.Residence,
                       AprovalFileName = x.AcademicStudies.AprovalFileName,
                       GradesFileName = x.AcademicStudies.GradesFileName,
                       AprovalDownloadName = x.AcademicStudies.AprovalDownloadName == null ? "" : $@"Users/{x.UserId}/{x.AcademicStudies.AprovalDownloadName}",
                       GradesDownloadName = x.AcademicStudies.GradesDownloadName == null ? "" : $@"Users/{x.UserId}/{x.AcademicStudies.GradesDownloadName}",
                       BeginningDegree = x.AcademicStudies.BeginningDegree,
                       FundStudies = x.AcademicStudies.FundStudies,
                       StudyYear = x.AcademicStudies.StudyYear,
                       Tuition = x.AcademicStudies.Tuition,
                       OtherFundings = x.AcademicStudies.OtherFundings,
                       TypeOfDegree = x.AcademicStudies.TypeOfDegree
                   },
                   CandidateAdditionalData = x.CandidateAdditionalData == null ? null : new CandidateAdditionalDataModel
                   {
                       Difficulties = x.CandidateAdditionalData.Difficulties,
                       FamilyDifficulties = x.CandidateAdditionalData.FamilyDifficulties,
                       FinancialProblems = x.CandidateAdditionalData.FinancialProblems,
                       HealthProblems = x.CandidateAdditionalData.HealthProblems,
                       LifeStory = x.CandidateAdditionalData.LifeStory,
                       ParticipationDescription = x.CandidateAdditionalData.ParticipationDescription,
                       Reason = x.CandidateAdditionalData.Reason,
                       SituationDetails = x.CandidateAdditionalData.SituationDetails,
                       Status = x.CandidateAdditionalData.Status,
                       HasFamilyDifficulties = x.CandidateAdditionalData.HasFamilyDifficulties,
                       HasFinancialProblems = x.CandidateAdditionalData.HasFinancialProblems,
                       HasHealthProblems = x.CandidateAdditionalData.HasHealthProblems
                   },
                   General = x.General == null ? null : new GeneralModel
                   {
                       Disabilities = x.General.Disabilities,
                       Points = x.General.Points,
                       PsychometricGrade = x.General.PsychometricGrade,
                       WorthyOfAdvancment = x.General.WorthyOfAdvancment
                   },
                   HighSchool = x.HighSchool == null ? null : new HighSchoolModel
                   {
                       Name = x.HighSchool.Name,
                       Year = x.HighSchool.Year
                   },
                   MilitaryService = x.MilitaryService == null ? null : new MilitaryServiceModel
                   {
                       Details = x.MilitaryService.Details,
                       Role = x.MilitaryService.Role,
                       TypeOfService = x.MilitaryService.TypeOfSevice,
                       EaseOfService = x.MilitaryService.EaseOfService,
                       Ease = x.MilitaryService.Ease
                   },
                   BankInfo = x.BankInfo == null ? null : new BankInfoModel
                   {
                       AccountNumber = x.BankInfo.AccountNumber,
                       BankName = x.BankInfo.BankName,
                       BranchNumber = x.BankInfo.BranchNumber
                   },
                   ScholarDetails = x.ScholarDetails == null ? null : new ScholarDetailsModel
                   {
                       Amount = x.ScholarDetails.Amount,
                       Refund = x.ScholarDetails.Refund,
                       Budgeting = x.ScholarDetails.Budgeting,
                       Scholarships = x.Scholarships.Select(s => new StudentScholarShipModel {
                           April = s.April,
                           August = s.August,
                           December = s.December,
                           February = s.February,
                           January = s.January,
                           July = s.July,
                           June = s.June,
                           March = s.March,
                           May = s.May,
                           November = s.November,
                           October = s.October,
                           September = s.September,
                           GivenInPast = s.GivenInPast,
                           Id = s.Id,
                           Scholarship = new ScholarshipModel
                           {
                               Id = s.Scholarship.Id,
                               Name = s.Scholarship.Name,
                               Admission = s.Scholarship.Admission,
                               Budget = s.Scholarship.Budget,
                               Status = s.Scholarship.Status
                           },
                           Total = s.April + s.August + s.December + s.February + s.January + s.July + s.June + s.March + s.May + s.November +s.October +s.September
                       }).ToList()
                   },
                   VolunteerDetails = x.VolunteerDetails == null ? null : new VolunteerDetailsModel
                   {
                       Contribution = x.VolunteerDetails.Contribution,
                       Hours = x.VolunteerDetails.Hours,
                       HoursSpent = 0// x.VolunteerHours == null ? 0 : x.VolunteerHours.Sum(m => (m.End - m.Start).Hours)
                   },
                   VolunteerHoursA = x.VolunteerHours.Where(va => va.Semester == SemesterEnum.A).Select(v => new VolunteerHoursModel
                    {
                       Id = v.Id,
                       Start = v.Start,
                       End = v.End,
                       ActivityType = v.ActivityType,
                       Date = v.Date,
                       Semester = v.Semester
                   }).ToList(),
                   VolunteerHoursB = x.VolunteerHours.Where(vb => vb.Semester == SemesterEnum.B).Select(v => new VolunteerHoursModel
                   {
                       Id = v.Id,
                       Start = v.Start,
                       End = v.End,
                       ActivityType = v.ActivityType,
                       Date = v.Date,
                       Semester = v.Semester
                   }).ToList(),
                   VolunteerHoursSummer = x.VolunteerHours.Where(vb => vb.Semester == SemesterEnum.Summer).Select(v => new VolunteerHoursModel
                   {
                       Id = v.Id,
                       Start = v.Start,
                       End = v.End,
                       ActivityType = v.ActivityType,
                       Date = v.Date,
                       Semester = v.Semester
                   }).ToList(),
                   Scholarships = x.Scholarships.Select(v => new StudentScholarShipModel
                   {
                       Id = v.Id,
                       April = v.April,
                       August = v.August,
                       December = v.December,
                       February = v.February,
                       January = v.January,
                       July = v.July,
                       June = v.June,
                       March = v.March,
                       May = v.May,
                       November = v.November,
                       October = v.October,
                       September = v.September,
                       GivenInPast = v.GivenInPast
                   }).ToList(),
                   Telephony = x.Telephony == null ? null : new TelephonyModel
                   {
                       DateBackFirst = x.Telephony.DateBackFirst,
                       DateBackSecond = x.Telephony.DateBackSecond,
                       DateBackThird = x.Telephony.DateBackThird,
                       FundingAvailability = x.Telephony.FundingAvailability,
                       Remarks = x.Telephony.Remarks,
                       Thoughts = x.Telephony.Thoughts
                   }
                   
               })
               .FirstOrDefault(x => x.UserId == userId);
            var scholars = _unitOfWork.Repository<StudentScholarshipEntity>()
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
                    Scholarship = new ScholarshipModel
                    {
                        Id = x.Scholarship.Id,
                        Name = x.Scholarship.Name,
                        Admission = x.Scholarship.Admission,
                        Budget = x.Scholarship.Budget,
                        Status = x.Scholarship.Status
                    },
                    ProfileId = x.ProfileId,
                    Total = x.April + x.August + x.December + x.February + x.January + x.July + x.June + x.March + x.May + x.November + x.October + x.September
                }).ToList();
            result.Scholarships = scholars;
            var hours = new TimeSpan();
            result.VolunteerHoursA.ForEach(v =>
            {
                var _hours =  TimeSpan.Parse(v.End) - TimeSpan.Parse(v.Start);
                if (_hours.TotalMinutes > 0)
                {
                    hours += _hours;
                }
            });
            result.VolunteerHoursB.ForEach(v =>
            {
                var _hours = TimeSpan.Parse(v.End) - TimeSpan.Parse(v.Start);
                if (_hours.TotalMinutes > 0)
                {
                    hours += _hours;
                }
            });
            result.VolunteerHoursSummer.ForEach(v =>
            {
                var _hours = TimeSpan.Parse(v.End) - TimeSpan.Parse(v.Start);
                if (_hours.TotalMinutes > 0)
                {
                    hours += _hours;
                }
            });

            if (result?.VolunteerDetails != null)
                result.VolunteerDetails.HoursSpent = (int)hours.TotalHours;

            if (result?.General != null)
            {
                var programs = _unitOfWork.Repository<ParticipationProgramEntity>().Set
                    .Select(x => new IdValue
                    {
                        Id = x.ProgramName,
                        Value = x.Generals.Any(m => m.GeneralId == result.Id),
                        Description = x.Generals.FirstOrDefault(m => m.GeneralId == result.Id).Description
                    })
                    .ToList();

                result.General.ParticipationInPrograms = programs;
            }

            return result;
        }

        public ProfileModel Get()
        {
            return this.Get(_authUser.Id);
        }

        public async Task<ProfileModel> Update(ProfileModel model)
        {
            return await this.Update(model, _authUser.Id);
        }

        public async Task<ProfileModel> AdminUpdate(ProfileModel model)
        {
            return await this.Update(model, model.UserId);
        }

        public async Task<ProfileModel> Update(ProfileModel model, string userId)
        {
            var entity =_unitOfWork.Repository<ProfileEntity>()
                .Include(x => x.AcademicStudies,
                         x => x.CandidateAdditionalData,
                         x => x.General,
                         x => x.HighSchool,
                         x => x.User,
                         x => x.BankInfo,
                         x => x.VolunteerDetails,
                         x => x.MilitaryService,
                         x => x.Telephony,
                         x => x.ScholarDetails)
                .FirstOrDefault(x => x.UserId == userId);
            if (entity != null)
            {
                using (var tran = _unitOfWork.BeginTransaction())
                {
                    entity.City = model.City;
                    entity.Gender = model.Gender;
                    entity.BirthDate = model.BirthDate;
                    entity.Address = model.Address;
                    entity.Siblings = model.Siblings;
                    entity.Phone = model.Phone;
                    entity.AcademicParents = model.AcademicParents;
                    entity.User.FirstName = model.FirstName;
                    entity.User.LastName = model.LastName;
                    entity.Status = model.Status;
                    if(entity.User.Email != model.Email && !string.IsNullOrWhiteSpace(model.Email))
                    {
                        entity.User.Email = model.Email;
                    }

                    if (model.AcademicStudies != null)
                    {
                        if (entity.AcademicStudies == null)
                        {
                            entity.AcademicStudies = new AcademicStudiesEntity();
                        }
                        entity.AcademicStudies.AcademicDegree = model.AcademicStudies.AcademicDegree;
                        entity.AcademicStudies.FieldOfStudy = model.AcademicStudies.FieldOfStudy;
                        entity.AcademicStudies.AcademicInstitution = model.AcademicStudies.AcademicInstitution;
                        entity.AcademicStudies.Residence = model.AcademicStudies.Residence;
                        entity.AcademicStudies.GraduationYear = model.AcademicStudies.GraduationYear;

                        entity.AcademicStudies.BeginningDegree = model.AcademicStudies.BeginningDegree;
                        entity.AcademicStudies.Tuition = model.AcademicStudies.Tuition;
                        entity.AcademicStudies.OtherFundings = model.AcademicStudies.OtherFundings;
                        entity.AcademicStudies.FundStudies = model.AcademicStudies.FundStudies;
                        entity.AcademicStudies.StudyYear = model.AcademicStudies.StudyYear;
                        entity.AcademicStudies.TypeOfDegree = model.AcademicStudies.TypeOfDegree;

                        if (!string.IsNullOrEmpty(model.AcademicStudies.Aproval?.FileName))
                        {
                            var newName = await _fileHelper.SaveOrUpdateUserFile(model.AcademicStudies.Aproval, entity.AcademicStudies.AprovalDownloadName, nameof(model.AcademicStudies.Aproval), entity.UserId);
                            model.AcademicStudies.AprovalDownloadName = newName;
                            entity.AcademicStudies.AprovalDownloadName = newName;
                            model.AcademicStudies.AprovalFileName = model.AcademicStudies.Aproval.FileName;
                            entity.AcademicStudies.AprovalFileName = model.AcademicStudies.Aproval.FileName;

                            model.AcademicStudies.Aproval = null;

                        }

                        if (!string.IsNullOrEmpty(model.AcademicStudies.Grades?.FileName))
                        {
                            var newName = await _fileHelper.SaveOrUpdateUserFile(model.AcademicStudies.Grades, entity.AcademicStudies.GradesDownloadName, nameof(model.AcademicStudies.Grades), entity.UserId);

                            model.AcademicStudies.GradesDownloadName = newName;
                            entity.AcademicStudies.GradesDownloadName = newName;
                            model.AcademicStudies.GradesFileName = model.AcademicStudies.Grades.FileName;
                            entity.AcademicStudies.GradesFileName = model.AcademicStudies.Grades.FileName;

                            model.AcademicStudies.Grades = null;
                        }

                    }

                    if (model.CandidateAdditionalData != null)
                    {
                        if (entity.CandidateAdditionalData == null)
                        {
                            entity.CandidateAdditionalData = new CandidateAdditionalDataEntity();
                        }
                        entity.CandidateAdditionalData.Difficulties = model.CandidateAdditionalData.Difficulties;
                        entity.CandidateAdditionalData.FamilyDifficulties = model.CandidateAdditionalData.FamilyDifficulties;
                        entity.CandidateAdditionalData.FinancialProblems = model.CandidateAdditionalData.FinancialProblems;
                        entity.CandidateAdditionalData.HealthProblems = model.CandidateAdditionalData.HealthProblems;
                        entity.CandidateAdditionalData.LifeStory = model.CandidateAdditionalData.LifeStory;
                        entity.CandidateAdditionalData.ParticipationDescription = model.CandidateAdditionalData.ParticipationDescription;
                        entity.CandidateAdditionalData.Reason = model.CandidateAdditionalData.Reason;
                        entity.CandidateAdditionalData.SituationDetails = model.CandidateAdditionalData.SituationDetails;
                        entity.CandidateAdditionalData.Status = model.CandidateAdditionalData.Status;
                    }

                    if (model.General != null)
                    {
                        if (entity.General == null)
                        {
                            entity.General = new GeneralEntity();
                        }

                            entity.General.Disabilities = model.General.Disabilities;
                        
                        entity.General.Points = model.General.Points;
                        entity.General.PsychometricGrade = model.General.PsychometricGrade;
                        entity.General.WorthyOfAdvancment = model.General.WorthyOfAdvancment;

                        var gp_delete =_unitOfWork.Repository<GeneralParticipationEntity>().Set.Where(x => x.GeneralId == entity.General.Id).ToList();
                        _unitOfWork.Repository<GeneralParticipationEntity>().DeleteRange(gp_delete);

                        var gp_new = _unitOfWork.Repository<ParticipationProgramEntity>().Set
                            .Join(model.General.ParticipationInPrograms.Where(x=>x.Value), 
                                x=>x.ProgramName, 
                                x=>x.Id, 
                                (x,y)=>new GeneralParticipationEntity
                                {
                                    GeneralId = entity.General.Id,
                                    Description = y.Description,
                                    ProgramId = x.Id
                                })
                            .ToList();
                        _unitOfWork.Repository<GeneralParticipationEntity>().InsertRange(gp_new);
                    }

                    if (model.HighSchool != null)
                    {
                        if (entity.HighSchool == null)
                        {
                            entity.HighSchool = new HighSchoolEntity();
                        }
                        entity.HighSchool.Name = model.HighSchool.Name;
                        entity.HighSchool.Year = model.HighSchool.Year;
                    }

                    if (model.MilitaryService != null)
                    {
                        if (entity.MilitaryService == null)
                        {
                            entity.MilitaryService = new MilitaryServiceEntity();
                        }
                        entity.MilitaryService.Details = model.MilitaryService.Details;
                        entity.MilitaryService.Role = model.MilitaryService.Role;
                        entity.MilitaryService.TypeOfSevice = model.MilitaryService.TypeOfService;
                        entity.MilitaryService.EaseOfService = model.MilitaryService.EaseOfService;
                        entity.MilitaryService.Ease = model.MilitaryService.Ease;
                    }

                    if (model.VolunteerDetails != null)
                    {
                        if (entity.VolunteerDetails == null)
                        {
                            entity.VolunteerDetails = new VolunteerDetailsEntity();
                        }
                        entity.VolunteerDetails.Contribution = model.VolunteerDetails.Contribution;
                        entity.VolunteerDetails.Hours = model.VolunteerDetails.Hours;
                    }

                    if(model.Telephony != null)
                    {
                        if (entity.Telephony == null)
                        {
                            entity.Telephony = new TelephonyEntity();
                        }
                        entity.Telephony.DateBackFirst = model.Telephony.DateBackFirst;
                        entity.Telephony.DateBackSecond = model.Telephony.DateBackSecond;
                        entity.Telephony.DateBackThird = model.Telephony.DateBackThird;
                        entity.Telephony.FundingAvailability = model.Telephony.FundingAvailability;
                        entity.Telephony.Remarks = model.Telephony.Remarks;
                        entity.Telephony.Thoughts = model.Telephony.Thoughts;
                    }

                    //Student part
                    if((entity.Status == UserStatusEnum.NewScholarship || entity.Status == UserStatusEnum.Scholarship))
                    {
                        if (model.BankInfo != null)
                        {
                            if (entity.BankInfo == null)
                            {
                                entity.BankInfo = new BankInfoEntity();
                            }
                            entity.BankInfo.BankName = model.BankInfo.BankName;
                            entity.BankInfo.AccountNumber = model.BankInfo.AccountNumber;
                            entity.BankInfo.BranchNumber = model.BankInfo.BranchNumber;
                        }

                        if(model.ScholarDetails != null)
                        {
                            if(entity.ScholarDetails == null)
                            {
                                entity.ScholarDetails = new ScholarDetailsEntity();
                            }
                            entity.ScholarDetails.Amount = model.ScholarDetails.Amount;
                            entity.ScholarDetails.Budgeting = model.ScholarDetails.Budgeting;
                            entity.ScholarDetails.Refund = model.ScholarDetails.Refund;
                        }
                    }

                    _unitOfWork.Repository<ProfileEntity>().Update(entity);
                    tran.Commit();
                }
                return model;
            }

            return null;
        }

        public async Task Delete(string userId)
        {
            var entity = _unitOfWork.Repository<ProfileEntity>()
                .Include(x => x.AcademicStudies,
                         x => x.CandidateAdditionalData,
                         x => x.General,
                         x => x.HighSchool,
                         x => x.BankInfo,
                         x => x.VolunteerDetails,
                         x => x.MilitaryService,
                         x => x.ScholarDetails,
                         x => x.VolunteerHours,
                         x => x.AttendingEvents,
                         x => x.Telephony,
                         x => x.Scholarships)
                .FirstOrDefault(x => x.UserId == userId);
            if (entity != null)
            {
                var user = _unitOfWork.Repository<UserEntity>()
                    .Include(x => x.ForgotPaswordTokens, x => x.Confirmation)
                    .FirstOrDefault(x => x.Id == userId);
                var formEntity = _unitOfWork.Repository<FormEntity>()
                    .Include(x=> x.ParentsSalary, x=>x.Tasks)
                    .FirstOrDefault(x => x.UserId == userId);

                using (var tran = _unitOfWork.BeginTransaction())
                {
                    if(entity.General != null)
                    {
                        var participations = _unitOfWork.Repository<GeneralParticipationEntity>().Set.Where(x => x.GeneralId == entity.General.Id).ToList();
                        _unitOfWork.Repository<GeneralParticipationEntity>().DeleteRange(participations);
                    }
                    if(formEntity != null)
                    {
                        _unitOfWork.Repository<FormParentsSalaryEntity>().Delete(formEntity.ParentsSalary);
                        _unitOfWork.Repository<FormTaskEntity>().DeleteRange(formEntity.Tasks);
                        _unitOfWork.Repository<FormEntity>().Delete(formEntity);
                    }
                    _unitOfWork.Repository<AcademicStudiesEntity>().Delete(entity.AcademicStudies);
                    _unitOfWork.Repository<CandidateAdditionalDataEntity>().Delete(entity.CandidateAdditionalData, true);
                    _unitOfWork.Repository<GeneralEntity>().Delete(entity.General, true);
                    _unitOfWork.Repository<HighSchoolEntity>().Delete(entity.HighSchool, true);
                    _unitOfWork.Repository<BankInfoEntity>().Delete(entity.BankInfo, true);
                    _unitOfWork.Repository<VolunteerDetailsEntity>().Delete(entity.VolunteerDetails, true);
                    _unitOfWork.Repository<MilitaryServiceEntity>().Delete(entity.MilitaryService, true);
                    _unitOfWork.Repository<ScholarDetailsEntity>().Delete(entity.ScholarDetails, true);
                    _unitOfWork.Repository<VolunteerHoursEntity>().DeleteRange(entity.VolunteerHours);
                    _unitOfWork.Repository<StudentScholarshipEntity>().DeleteRange(entity.Scholarships);
                    _unitOfWork.Repository<EventAttendeeEntity>().DeleteRange(entity.AttendingEvents);
                    _unitOfWork.Repository<TelephonyEntity>().Delete(entity.Telephony, true);
                    _unitOfWork.Repository<ProfileEntity>().Delete(entity);

                    _unitOfWork.Repository<ConfirmationEntity>().Delete(user.Confirmation);
                    _unitOfWork.Repository<ForgotPaswordTokenEntity>().DeleteRange(user.ForgotPaswordTokens);
                    _unitOfWork.Repository<UserEntity>().Delete(user);
                    await _fileHelper.DeleteUserFiles(userId);
                    tran.Commit();
                }
            }
        }

        public SystemDetailsModel GetSystemDetails()
        {
            return _unitOfWork.Repository<UserEntity>().Set
                .Select(x => new SystemDetailsModel {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Password = x.Password
                })
                .FirstOrDefault(x => x.Id == _authUser.Id);
        }

        public SystemDetailsModel UpdateSystemDetails(SystemDetailsModel model)
        {
            var entity = _unitOfWork.Repository<UserEntity>().Set
                .FirstOrDefault(x => x.Id == _authUser.Id);
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            if (entity.Password != model.Password)
            {
                var salt = _cryptoContext.GenerateSaltAsBase64();
                var password = Convert.ToBase64String(_cryptoContext.DeriveKey(model.Password, salt));
                entity.Salt = salt;
                entity.Password = password;
            }
            _unitOfWork.Repository<UserEntity>().Update(entity);
            return model;
        }

        public async Task<ProfileModel> Create(ProfileModel model)
        {
            using (var tran = _unitOfWork.BeginTransaction())
            {
                var entity = new ProfileEntity();
                entity.City = model.City;
                entity.Gender = model.Gender;
                entity.BirthDate = model.BirthDate;
                entity.Address = model.Address;
                entity.Siblings = model.Siblings;
                entity.Phone = model.Phone;
                entity.AcademicParents = model.AcademicParents;
                entity.Status = model.Status;
                entity.UserId = model.UserId;

                var salt = _cryptoContext.GenerateSaltAsBase64();
                var password = Convert.ToBase64String(_cryptoContext.DeriveKey("123456", salt));
                entity.User = new UserEntity
                {
                    Id = model.UserId,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Role = RoleEnum.User,
                    Created = DateTime.Now,
                    Password = password,
                    Salt = salt,
                    Confirmation = new ConfirmationEntity
                    {
                        Code = Guid.NewGuid().ToString(),
                        Confirmed = true,
                        Id = model.UserId,
                    }
                };

                entity.AcademicStudies = new AcademicStudiesEntity();
                if (model.AcademicStudies != null)
                {
                    entity.AcademicStudies.AcademicDegree = model.AcademicStudies.AcademicDegree;
                    entity.AcademicStudies.FieldOfStudy = model.AcademicStudies.FieldOfStudy;
                    entity.AcademicStudies.AcademicInstitution = model.AcademicStudies.AcademicInstitution;
                    entity.AcademicStudies.Residence = model.AcademicStudies.Residence;
                    entity.AcademicStudies.GraduationYear = model.AcademicStudies.GraduationYear;

                    entity.AcademicStudies.BeginningDegree = model.AcademicStudies.BeginningDegree;
                    entity.AcademicStudies.Tuition = model.AcademicStudies.Tuition;
                    entity.AcademicStudies.OtherFundings = model.AcademicStudies.OtherFundings;
                    entity.AcademicStudies.FundStudies = model.AcademicStudies.FundStudies;
                    entity.AcademicStudies.StudyYear = model.AcademicStudies.StudyYear;
                    entity.AcademicStudies.TypeOfDegree = model.AcademicStudies.TypeOfDegree;

                    if (!string.IsNullOrEmpty(model.AcademicStudies.Aproval?.FileName))
                    {
                        var newName = await _fileHelper.SaveOrUpdateUserFile(model.AcademicStudies.Aproval, string.Empty, nameof(model.AcademicStudies.Aproval), entity.UserId);
                        model.AcademicStudies.AprovalDownloadName = newName;
                        entity.AcademicStudies.AprovalDownloadName = newName;
                        model.AcademicStudies.AprovalFileName = model.AcademicStudies.Aproval.FileName;
                        entity.AcademicStudies.AprovalFileName = model.AcademicStudies.Aproval.FileName;

                        model.AcademicStudies.Aproval = null;

                    }

                    if (!string.IsNullOrEmpty(model.AcademicStudies.Grades?.FileName))
                    {
                        var newName = await _fileHelper.SaveOrUpdateUserFile(model.AcademicStudies.Grades, string.Empty, nameof(model.AcademicStudies.Grades), entity.UserId);

                        model.AcademicStudies.GradesDownloadName = newName;
                        entity.AcademicStudies.GradesDownloadName = newName;
                        model.AcademicStudies.GradesFileName = model.AcademicStudies.Grades.FileName;
                        entity.AcademicStudies.GradesFileName = model.AcademicStudies.Grades.FileName;

                        model.AcademicStudies.Grades = null;
                    }
                }

                
                entity.CandidateAdditionalData = new CandidateAdditionalDataEntity();
                if (model.CandidateAdditionalData != null)
                {
                    entity.CandidateAdditionalData.Difficulties = model.CandidateAdditionalData.Difficulties;
                    entity.CandidateAdditionalData.FamilyDifficulties = model.CandidateAdditionalData.FamilyDifficulties;
                    entity.CandidateAdditionalData.FinancialProblems = model.CandidateAdditionalData.FinancialProblems;
                    entity.CandidateAdditionalData.HealthProblems = model.CandidateAdditionalData.HealthProblems;
                    entity.CandidateAdditionalData.LifeStory = model.CandidateAdditionalData.LifeStory;
                    entity.CandidateAdditionalData.ParticipationDescription = model.CandidateAdditionalData.ParticipationDescription;
                    entity.CandidateAdditionalData.Reason = model.CandidateAdditionalData.Reason;
                    entity.CandidateAdditionalData.SituationDetails = model.CandidateAdditionalData.SituationDetails;
                    entity.CandidateAdditionalData.Status = model.CandidateAdditionalData.Status;
                }
                entity.General = new GeneralEntity();
                if (model.General != null)
                {
                    entity.General.Disabilities = model.General.Disabilities;
                    entity.General.Points = model.General.Points;
                    entity.General.PsychometricGrade = model.General.PsychometricGrade;
                    entity.General.WorthyOfAdvancment = model.General.WorthyOfAdvancment;
                    if (model.General.ParticipationInPrograms != null)
                    {
                        var gp_new = _unitOfWork.Repository<ParticipationProgramEntity>().Set
                            .Join(model.General.ParticipationInPrograms.Where(x => x.Value),
                                x => x.ProgramName,
                                x => x.Id,
                                (x, y) => new GeneralParticipationEntity
                                {
                                    GeneralId = entity.General.Id,
                                    Description = y.Description,
                                    ProgramId = x.Id
                                })
                            .ToList();
                        _unitOfWork.Repository<GeneralParticipationEntity>().InsertRange(gp_new);
                    }
                    
                    
                }
                if (model.HighSchool != null)
                {
                    entity.HighSchool = new HighSchoolEntity();
                    entity.HighSchool.Name = model.HighSchool.Name;
                    entity.HighSchool.Year = model.HighSchool.Year;
                }
                if (model.MilitaryService != null)
                {
                    entity.MilitaryService = new MilitaryServiceEntity();
                    entity.MilitaryService.Details = model.MilitaryService.Details;
                    entity.MilitaryService.Role = model.MilitaryService.Role;
                    entity.MilitaryService.TypeOfSevice = model.MilitaryService.TypeOfService;
                    entity.MilitaryService.EaseOfService = model.MilitaryService.EaseOfService;
                    entity.MilitaryService.Ease = model.MilitaryService.Ease;
                }

                //Student part
                if ((entity.Status == UserStatusEnum.NewScholarship || entity.Status == UserStatusEnum.Scholarship))
                {
                            entity.BankInfo = new BankInfoEntity();
                    if (model.BankInfo != null)
                    {
                        entity.BankInfo.BankName = model.BankInfo.BankName;
                        entity.BankInfo.AccountNumber = model.BankInfo.AccountNumber;
                        entity.BankInfo.BranchNumber = model.BankInfo.BranchNumber;
                    }
                            entity.ScholarDetails = new ScholarDetailsEntity();
                    if (model.ScholarDetails != null)
                    {
                        entity.ScholarDetails.Amount = model.ScholarDetails.Amount;
                        entity.ScholarDetails.Budgeting = model.ScholarDetails.Budgeting;
                        entity.ScholarDetails.Refund = model.ScholarDetails.Refund;
                    }
                }
                _unitOfWork.Repository<ProfileEntity>().Insert(entity);

                var form = _formService.GenerateForm(entity.User.Id);
                _unitOfWork.Repository<FormEntity>().Insert(form);
                tran.Commit();
            }
            return model;
        }
    }
}
