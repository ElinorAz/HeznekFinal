using Heznek.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services
{
    public interface ITelephonyService
    {
        TelephonyProfileModel Update(TelephonyProfileModel model);
        List<TelephonyProfileModel> Get(string name);
        TelephonyEventsModel MyEvents();
    }
}
