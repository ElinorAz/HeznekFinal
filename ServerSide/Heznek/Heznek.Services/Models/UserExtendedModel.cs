using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class UserExtendedModel: UserModel
    {
        public string Faculty { get; set; }
        public string Domain { get; set; }
        public string University { get; set; }
        public string Phone { get; set; }
        public UserStatusEnum Status { get; set; }
        public string City { get; set; }
        public GenderEnum? Gender { get; set; }
        public int? GraduationYear { get; set; }
        public TelephonyModel Telephony { get; set; }
    }
}
