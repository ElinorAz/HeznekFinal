using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class SeparatedVolunteerHoursModel
    {
        public List<VolunteerHoursModel> VolunteerHoursA { get; set; }
        public List<VolunteerHoursModel> VolunteerHoursB { get; set; }
        public List<VolunteerHoursModel> VolunteerHoursSummer { get; set; }
        public int HoursSpent { get; set; }
    }
}
