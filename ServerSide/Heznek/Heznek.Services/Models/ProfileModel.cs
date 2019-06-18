using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class ProfileModel :PersistentModel<int>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string UserId { get; set; }
        public string City { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Siblings { get; set; }
        public int AcademicParents { get; set; }
        public UserStatusEnum Status { get; set; }

        public AcademicStudiesModel AcademicStudies { get; set; }
        public CandidateAdditionalDataModel CandidateAdditionalData { get; set; }
        public GeneralModel General { get; set; }
        public HighSchoolModel HighSchool { get; set; }
        public MilitaryServiceModel MilitaryService { get; set; }
        public BankInfoModel BankInfo { get; set; }
        public ScholarDetailsModel ScholarDetails { get; set; }
        public VolunteerDetailsModel VolunteerDetails { get; set; }

        public List<VolunteerHoursModel> VolunteerHoursA { get; set; }
        public List<VolunteerHoursModel> VolunteerHoursB { get; set; }
        public List<VolunteerHoursModel> VolunteerHoursSummer { get; set; }

        public List<StudentScholarShipModel> Scholarships { get; set; }

        public FormModel CandidateForm { get; set; }
        public TelephonyModel Telephony { get; set; }
    }
}
