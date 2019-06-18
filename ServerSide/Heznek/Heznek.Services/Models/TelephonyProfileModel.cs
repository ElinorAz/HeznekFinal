using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class TelephonyProfileModel : PersistentModel<int>
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

        public TelephonyModel Telephony { get; set; }
        public CandidateAdditionalDataModel CandidateAdditionalData { get; set; }
        public GeneralModel General { get; set; }
        public HighSchoolModel HighSchool { get; set; }
        public MilitaryServiceModel MilitaryService { get; set; }
    }
}
