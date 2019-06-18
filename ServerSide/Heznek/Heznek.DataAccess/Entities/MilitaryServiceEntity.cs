using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class MilitaryServiceEntity : PersistentEntity<int>
    {
        public string Role { get; set; }
        public TypeOfSeviceEnum? TypeOfSevice { get; set; }
        public string Details { get; set; }
        public bool EaseOfService { get; set; }
        public string Ease { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
