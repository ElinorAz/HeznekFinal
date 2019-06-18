using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class TelephonyEventsModel
    {
        public List<string> NewEvents { get; set; }
        public EventModel LatestEvent { get; set; }
    }
}
