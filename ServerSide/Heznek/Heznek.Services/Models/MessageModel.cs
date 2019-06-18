using Heznek.Common.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class MessageModel
    {
        public string Topic { get; set; }
        public string Content { get; set; }
        public IFormFile File { get; set; }
        public List<UserStatusEnum> SendToWho { get; set; }
    }
}
