using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class ScholarDetailsEntity:PersistentEntity<int>
    {
        public decimal Amount { get; set; }
        public bool Budgeting { get; set; }
        public bool Refund { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
