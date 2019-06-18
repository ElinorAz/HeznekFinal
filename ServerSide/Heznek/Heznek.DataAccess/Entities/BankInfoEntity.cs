using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class BankInfoEntity:PersistentEntity<int>
    {
        public string BankName { get; set; }
        public string BranchNumber { get; set; }
        public string AccountNumber { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
