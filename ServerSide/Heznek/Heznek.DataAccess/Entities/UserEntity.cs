using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class UserEntity : PersistentEntity<string>
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }

        public string Password { get; set; }
        public string Salt { get; set; }
        
        public RoleEnum Role { get; set; }

        public List<ForgotPaswordTokenEntity> ForgotPaswordTokens { get; set; }
        public ConfirmationEntity Confirmation { get; set; }
        public ProfileEntity Profile { get; set; }
        public FormEntity Form { get; set; }
    }
}