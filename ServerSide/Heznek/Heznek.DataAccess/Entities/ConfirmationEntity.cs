using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class ConfirmationEntity :PersistentEntity<string>
    {
        public string Code { get; set; }
        public bool Confirmed { get; set; }
        public DateTime? ConfirmDate { get; set; }

        public UserEntity User { get; set; }

    }
}
