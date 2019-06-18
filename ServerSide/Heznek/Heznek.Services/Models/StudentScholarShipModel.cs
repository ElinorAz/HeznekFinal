using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class StudentScholarShipModel:PersistentModel<int>
    {
        public decimal January { get; set; }
        public decimal February { get; set; }
        public decimal March { get; set; }
        public decimal April { get; set; }
        public decimal May { get; set; }
        public decimal June { get; set; }
        public decimal July { get; set; }
        public decimal August { get; set; }
        public decimal September { get; set; }
        public decimal October { get; set; }
        public decimal November { get; set; }
        public decimal December { get; set; }

        public ScholarshipModel Scholarship { get; set; }

        public int ProfileId { get; set; } 

        public decimal Total { get; set; }

        public bool GivenInPast { get; set; }
    }
}
