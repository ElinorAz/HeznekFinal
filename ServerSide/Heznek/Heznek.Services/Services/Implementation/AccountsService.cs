using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heznek.Common.Email;
using Heznek.Common.Email.Models;
using Heznek.Common.Enums;
using Heznek.DataAccess.Entities;
using Heznek.DataAccess.Infrastructure;
using Heznek.Services.Crypto;
using Heznek.Services.Models;
using Heznek.Services.Providers;

namespace Heznek.Services.Implementation
{
    public class AccountsService : IAccountsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoContext _cryptoContext;
        private readonly IAuthenticatedUser _au;
        private readonly IAuthTokenProvider _tokenProvider;
        private readonly IEmailSender _emailSender;
        private readonly IFormService _formService;

        public AccountsService(IUnitOfWork unitOfWork,
            IFormService formService,
            IAuthTokenProvider tokenProvider,
            ICryptoContext cryptoContext,
            IEmailSender emailSender,
            IAuthenticatedUser au)
        {
            _unitOfWork = unitOfWork;
            _cryptoContext = cryptoContext;
            _tokenProvider = tokenProvider;
            _emailSender = emailSender;
            _au = au;
            _formService = formService;
        }

        public bool Confirm(string code)
        {
            var entity = _unitOfWork.Repository<ConfirmationEntity>().Include(x=>x.User).FirstOrDefault(x => x.Code == code);
            if(entity?.Confirmed == false)
            {
                entity.Confirmed = true;
                entity.ConfirmDate = DateTime.Now;

                _unitOfWork.Repository<ConfirmationEntity>().Update(entity);

                var profile = new ProfileEntity
                {
                    Status = UserStatusEnum.ActiveCandidate,
                    UserId = entity.User.Id,
                    AcademicStudies = new AcademicStudiesEntity(),
                    CandidateAdditionalData = new CandidateAdditionalDataEntity(),
                    General = new GeneralEntity(),
                    HighSchool = new HighSchoolEntity(),
                    MilitaryService = new MilitaryServiceEntity(),
                    BankInfo = new BankInfoEntity(),
                    ScholarDetails = new ScholarDetailsEntity(),
                    VolunteerDetails = new VolunteerDetailsEntity()
                };

                var form = _formService.GenerateForm(entity.User.Id);
                _unitOfWork.Repository<ProfileEntity>().Insert(profile);
                return true;
            }
            return false;
            
        }

        public byte[] ExportCSV(string[] ids)
        {
            var candidates = _unitOfWork.Repository<ProfileEntity>().Set
                .Where(x => ids.Any(m => m == x.UserId))
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
                }).Select(x=>$"{x.Faculty},{x.Domain},{x.University},{x.Phone},{x.Email},{x.Status.ToString()},{x.LastName},{x.FirstName},{x.Id}")
                .ToList();
            var text = string.Join(Environment.NewLine, candidates);
            var data = Encoding.UTF8.GetBytes(text.ToString());
            return Encoding.UTF8.GetPreamble().Concat(data).ToArray(); 
        }

        public List<UserExtendedModel> GetCandidates()
        {
            var users =  _unitOfWork.Repository<ProfileEntity>().Include(x=>x.User)
                .Where(x => (x.Status == UserStatusEnum.ActiveCandidate 
                         || x.Status == UserStatusEnum.InactiveCandidateInProcess || x.Status == UserStatusEnum.Staff
                         || x.Status == UserStatusEnum.InactiveCandidateTermination) && x.User.Role != RoleEnum.Admin)
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
                    University = x.AcademicStudies == null ? null : x.AcademicStudies.AcademicInstitution,
                    Role = x.User.Role,
                    City = x.City,
                    Gender = x.Gender,
                    Telephony = null,
                    //Telephony = x.Telephony == null ? null : new TelephonyModel
                    //{
                    //    Remarks = x.Telephony.Remarks,
                    //    Thoughts = x.Telephony.Thoughts,
                    //    FundingAvailability = x.Telephony.FundingAvailability,
                    //    DateBackFirst = x.Telephony.DateBackFirst,
                    //    DateBackSecond = x.Telephony.DateBackSecond,
                    //    DateBackThird = x.Telephony.DateBackThird
                    //},
                    GraduationYear = x.AcademicStudies.GraduationYear
                })
                .ToList();

            var telephonies = _unitOfWork.Repository<TelephonyEntity>().Include(x => x.Profile).Where(x => users.Any(n => n.Id == x.Profile.UserId)).ToList();
            foreach (var telephony in telephonies)
            {
                var user = users.FirstOrDefault(x => x.Id == telephony.Profile.UserId);
                user.Telephony = new TelephonyModel
                {
                    Remarks = telephony.Remarks,
                    Thoughts = telephony.Thoughts,
                    FundingAvailability = telephony.FundingAvailability,
                    DateBackFirst = telephony.DateBackFirst,
                    DateBackSecond = telephony.DateBackSecond,
                    DateBackThird = telephony.DateBackThird
                };
            }

            return users;
        }

        public List<UserExtendedModel> GetStudents()
        {
            return _unitOfWork.Repository<ProfileEntity>().Include(x => x.User)
                .Where(x => (x.Status == UserStatusEnum.NewScholarship
                         || x.Status == UserStatusEnum.Scholarship) && x.User.Role != RoleEnum.Admin)
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
                    University = x.AcademicStudies == null ? null : x.AcademicStudies.AcademicInstitution,
                    Role = x.User.Role,
                    City = x.City,
                    Gender = x.Gender,
                    GraduationYear = x.AcademicStudies.GraduationYear
                })
                .ToList();
        }

        public async Task GrandPermission(UserModel model)
        {
            var entity = _unitOfWork.Repository<UserEntity>().Set.FirstOrDefault(x => x.Id == model.Id);
            if(entity != null && model.Role != RoleEnum.Admin)
            {
                entity.Role = model.Role;
                await _unitOfWork.Repository<UserEntity>().UpdateAsync(entity);
            }
        }

        public async Task<bool> Register(RegisterModel model, string link)
        {
            var isNotNew = _unitOfWork.Repository<UserEntity>().Set.Any(x => x.Id == model.Id && x.Email == model.Email);

            if (isNotNew)
                return false;

            var salt = _cryptoContext.GenerateSaltAsBase64();
            var password = Convert.ToBase64String(_cryptoContext.DeriveKey(model.Password, salt));

            var user = new UserEntity
            {
                Id = model.Id,
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
                    Confirmed = false,
                    Id = model.Id,
                }
            };

            _unitOfWork.Repository<UserEntity>().Insert(user);
            await _emailSender.SendEmail(user.Email, "Account Confirmation", "ConfirmAccount", new ConfirmEmail { Link = $"{link}/{user.Confirmation.Code}" });
            return true;
        }

        public UserModel Update(UserModel model)
        {
            var entity = _unitOfWork.Repository<UserModel>().Set.FirstOrDefault(x => x.Id == _au.Id);

            if(entity != null)
            {
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;

                _unitOfWork.Repository<UserModel>().Update(entity);
            }

            return null;
        }
    }
}
