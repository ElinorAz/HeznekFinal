using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Common.Options
{
    public class EmailOptions
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string From { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string ApiKey { get; set; }
    }
}
