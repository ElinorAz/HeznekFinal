using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class StatisticScholarshipCandidatesModel
    {
        public List<KeyValue<string, int>> Cities { get; set; }
        public List<KeyValue<GenderEnum, int>> Sex { get; set; }
        public int AcademicInstitution { get; set; }
        public int DisabilityLearning { get; set; }
        public List<KeyValue<string, int>> StudyFields { get; set; }
    }
}
