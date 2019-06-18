using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class ScholarDetailsModel
    {
        public decimal Amount { get; set; }
        public bool Budgeting { get; set; }
        public bool Refund { get; set; }

        public List<StudentScholarShipModel> Scholarships { get; set; }
    }
}
