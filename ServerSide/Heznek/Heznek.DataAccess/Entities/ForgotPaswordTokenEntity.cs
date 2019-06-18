using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class ForgotPaswordTokenEntity : PersistentEntity<int>
    {
        public string Code { get; set; }
        public DateTime ExpireTime { get; set; }
        public bool Used { get; set; }

        public UserEntity User { get; set; }
        public string UserId { get; set; }
    }
}
