using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class TokenModel
    {
        public string AccessToken { get; set; }

        public DateTimeOffset IssueAt { get; set; }

        public DateTimeOffset ExpiresAt { get; set; }

        public string Id { get; set; }
    }
}