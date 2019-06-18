using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class ProfileEntity :PersistentEntity<int>
    {
        public string UserId { get; set; }
        public UserEntity User { get; set; }

        public string City { get; set; }
        public GenderEnum? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Siblings { get; set; }
        public int AcademicParents { get; set; }
        public UserStatusEnum Status { get; set; }

        public AcademicStudiesEntity AcademicStudies { get; set; }
        public CandidateAdditionalDataEntity CandidateAdditionalData { get; set; }
        public GeneralEntity General { get; set; }
        public HighSchoolEntity HighSchool { get; set; }
        public MilitaryServiceEntity MilitaryService { get; set; }
        public BankInfoEntity BankInfo { get; set; }
        public ScholarDetailsEntity ScholarDetails { get; set; }
        public List<VolunteerHoursEntity> VolunteerHours { get; set; }
        public VolunteerDetailsEntity VolunteerDetails { get; set; }
        public List<StudentScholarshipEntity> Scholarships { get; set; }
        public List<EventAttendeeEntity> AttendingEvents { get; set; }
        public TelephonyEntity Telephony { get; set; }
    }
}
