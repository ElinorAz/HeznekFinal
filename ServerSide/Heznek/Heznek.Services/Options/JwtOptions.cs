using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Options
{
    public class JwtOptions
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public string Key { get; set; }
        public int LifetimeInMinutes { get; set; }
    }
}