using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class StatisticCandidatesModel
    {
        public List<KeyValue<string, int>> Cities { get; set; }
        public List<KeyValue<GenderEnum, int>> Sex { get; set; }
        public int AcademicInstitution { get; set; }
        public int PsychometricGrade { get; set; }
        public int FullMilitary { get; set; }
        public int DisabilityLearning { get; set; }
    }
}
