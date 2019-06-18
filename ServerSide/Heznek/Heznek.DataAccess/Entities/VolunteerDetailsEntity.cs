using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class VolunteerDetailsEntity:PersistentEntity<int>
    {
        public int Hours { get; set; }
        public string Contribution { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
