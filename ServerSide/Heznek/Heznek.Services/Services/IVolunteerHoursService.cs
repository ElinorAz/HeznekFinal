using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services
{
    public interface IVolunteerHoursService
    {
        SeparatedVolunteerHoursModel GetMyHours();
        SeparatedVolunteerHoursModel GetHours(string userId);
        VolunteerHoursModel Insert(VolunteerHoursModel model);
    }
}
