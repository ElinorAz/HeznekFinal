using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class ScholarshipEntity:PersistentEntity<int>
    {
        public string Name { get; set; }
        public string Admission { get; set; }
        public ScholarshipStatusEnum Status { get; set; }
        public decimal Budget { get; set; }

        public List<StudentScholarshipEntity> Students { get; set; }
    }
}
