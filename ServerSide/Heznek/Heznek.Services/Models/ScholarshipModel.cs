using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class ScholarshipModel:PersistentModel<int>
    {
        public string Name { get; set; }
        public string Admission { get; set; }
        public ScholarshipStatusEnum Status { get; set; }
        public decimal Budget { get; set; }
        public int? StundentsCount { get; set; }

        public List<UserExtendedModel> Students { get; set; }
    }
}
