using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class TelephonyModel
    {
        public string Remarks { get; set; }
        public string Thoughts { get; set; }
        public bool FundingAvailability { get; set; }

        public DateTime? DateBackFirst { get; set; }
        public DateTime? DateBackSecond { get; set; }
        public DateTime? DateBackThird { get; set; }
    }
}
