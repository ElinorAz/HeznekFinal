using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class VolunteerHoursModel:PersistentModel<int>
    {
        public DateTime Date { get; set; }
        public string Start { get; set; }
        public string End { get; set; }
        public ActivityTypeEnum ActivityType { get; set; }
        public SemesterEnum Semester { get; set; }
    }
}
