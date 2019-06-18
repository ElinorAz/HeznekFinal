using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class HighSchoolEntity:PersistentEntity<int>
    {
        public string Name { get; set; }
        public int? Year { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
