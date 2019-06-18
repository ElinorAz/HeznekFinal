using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class MilitaryServiceModel
    {
        public string Role { get; set; }
        public TypeOfSeviceEnum? TypeOfService { get; set; }
        public bool EaseOfService { get; set; }
        public string Details { get; set; }
        public string Ease { get; set; }
    }
}
