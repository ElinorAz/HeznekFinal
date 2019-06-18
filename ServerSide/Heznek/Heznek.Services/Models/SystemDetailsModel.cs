using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class SystemDetailsModel : PersistentModel<string>
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
