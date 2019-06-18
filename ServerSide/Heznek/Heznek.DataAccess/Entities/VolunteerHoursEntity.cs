using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class VolunteerHoursEntity:PersistentEntity<int>
    {
        public DateTime Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public SemesterEnum Semester { get; set; }

        public int ProfileId { get; set; }
        public ProfileEntity Profile { get; set; }
    }
}
