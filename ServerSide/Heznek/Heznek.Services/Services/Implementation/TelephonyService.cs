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
    public class TelephonyService : AbstractService, ITelephonyService
    {
        public TelephonyService(IUnitOfWork unitOfWork, IAuthenticatedUser authUser) 
            : base(unitOfWork, authUser)
        {
        }

        public List<TelephonyProfileModel> Get(string name)
        {
            var tokens = name.Split(' ');
            return _unitOfWork.Repository<ProfileEntity>().Include(
                    x => x.Telephony,
                    x => x.General,
                    x => x.User,
                    x => x.HighSchool,
                    x => x.MilitaryService,
                    x => x.CandidateAdditionalData)
                .Where(x => (x.Status == UserStatusEnum.InactiveCandidateInProcess || x.Status == UserStatusEnum.InactiveCandidateTermination)&&((x.User.FirstName+" "+x.User.LastName == name) || tokens.Contains(x.User.FirstName) || tokens.Contains(x.User.LastName) || tokens.Contains(x.User.Id) || tokens.Contains(x.Phone)))
                .Select(x => new TelephonyProfileModel
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
                    Telephony = x.Telephony == null ? null : new TelephonyModel
                    {
                        Remarks = x.Telephony.Remarks,
                        Thoughts = x.Telephony.Thoughts,
                        FundingAvailability = x.Telephony.FundingAvailability,
                        DateBackFirst = x.Telephony.DateBackFirst,
                        DateBackSecond = x.Telephony.DateBackSecond,
                        DateBackThird = x.Telephony.DateBackThird
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
                    }
                })
                .ToList();
        }

        public TelephonyEventsModel MyEvents()
        {
            var latest = _unitOfWork.Repository<EventEntity>().Set.Where(x => x.Attendees.Any(n => n.Profile.UserId == _authUser.Id))
                .OrderByDescending(x => x.Date)
                .Select(x => new EventModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    Name = x.Name,
                    Expected = x.Expected,
                    Location = x.Location,
                    Subject = x.Subject,
                    Time = x.Time,
                    FinishTime = x.FinishTime,
                    Attend = true,
                    ParticipantTypes = x.ParticipantTypes.Select(n => n.UserType).ToList(),
                })
                .FirstOrDefault();
            var profile = _unitOfWork.Repository<ProfileEntity>().Set.FirstOrDefault(x => x.UserId == _authUser.Id);
            var invited = _unitOfWork.Repository<EventEntity>().Set
                .Where(x => x.ParticipantTypes.Any(n => n.UserType == profile.Status) && !x.Attendees.Any(n => n.ProfileId == profile.Id))
                .Select(x => x.Name)
                .ToList();
            

            return  new TelephonyEventsModel
            {
                LatestEvent = latest,
                NewEvents = invited
            };
        }

        public TelephonyProfileModel Update(TelephonyProfileModel model)
        {
            var entity = _unitOfWork.Repository<ProfileEntity>()
                .Include(
                    x => x.Telephony,
                    x => x.General,
                    x => x.User,
                    x => x.HighSchool,
                    x => x.MilitaryService,
                    x => x.CandidateAdditionalData)
                .FirstOrDefault(x => x.UserId == model.UserId);

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
                    if (entity.User.Email != model.Email && !string.IsNullOrWhiteSpace(model.Email))
                    {
                        var exist = _unitOfWork.Repository<UserEntity>().Set.Any(x => x.Email == model.Email);
                        if (!exist)
                        {
                            entity.User.Email = model.Email;
                        }
                    }

                    if (model.Telephony != null)
                    {
                        if (entity.Telephony == null)
                        {
                            entity.Telephony = new TelephonyEntity();
                            _unitOfWork.Repository<TelephonyEntity>().Insert(entity.Telephony);
                        }
                        entity.Telephony.Remarks = model.Telephony.Remarks;
                        entity.Telephony.Thoughts = model.Telephony.Thoughts;
                        entity.Telephony.FundingAvailability = model.Telephony.FundingAvailability;
                        entity.Telephony.DateBackFirst = model.Telephony.DateBackFirst;
                        entity.Telephony.DateBackSecond = model.Telephony.DateBackSecond;
                        entity.Telephony.DateBackThird = model.Telephony.DateBackThird;

                    }

                    if (model.CandidateAdditionalData != null)
                    {
                        if (entity.CandidateAdditionalData == null)
                        {
                            entity.CandidateAdditionalData = new CandidateAdditionalDataEntity();
                            _unitOfWork.Repository<CandidateAdditionalDataEntity>().Insert(entity.CandidateAdditionalData);
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
                            _unitOfWork.Repository<GeneralEntity>().Insert(entity.General);
                        }

                        entity.General.Disabilities = model.General.Disabilities;

                        entity.General.Points = model.General.Points;
                        entity.General.PsychometricGrade = model.General.PsychometricGrade;
                        entity.General.WorthyOfAdvancment = model.General.WorthyOfAdvancment;

                        var gp_delete = _unitOfWork.Repository<GeneralParticipationEntity>().Set.Where(x => x.GeneralId == entity.General.Id).ToList();
                        _unitOfWork.Repository<GeneralParticipationEntity>().DeleteRange(gp_delete);

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

                    if (model.HighSchool != null)
                    {
                        if (entity.HighSchool == null)
                        {
                            entity.HighSchool = new HighSchoolEntity();
                            _unitOfWork.Repository<HighSchoolEntity>().Insert(entity.HighSchool);
                        }
                        entity.HighSchool.Name = model.HighSchool.Name;
                        entity.HighSchool.Year = model.HighSchool.Year;
                    }

                    if (model.MilitaryService != null)
                    {
                        if (entity.MilitaryService == null)
                        {
                            entity.MilitaryService = new MilitaryServiceEntity();
                            _unitOfWork.Repository<MilitaryServiceEntity>().Insert(entity.MilitaryService);
                        }
                        entity.MilitaryService.Details = model.MilitaryService.Details;
                        entity.MilitaryService.Role = model.MilitaryService.Role;
                        entity.MilitaryService.TypeOfSevice = model.MilitaryService.TypeOfService;
                        entity.MilitaryService.EaseOfService = model.MilitaryService.EaseOfService;
                        entity.MilitaryService.Ease = model.MilitaryService.Ease;
                    }

                    _unitOfWork.Repository<ProfileEntity>().Update(entity);
                    tran.Commit();
                }
                    return model;
            }
            return null;
        }
    }
}
