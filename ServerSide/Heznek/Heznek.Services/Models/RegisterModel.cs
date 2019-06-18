using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class RegisterModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
