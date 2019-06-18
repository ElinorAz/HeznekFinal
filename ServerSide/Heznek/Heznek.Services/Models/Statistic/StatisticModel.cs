using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class StatisticModel
    {
        public StatisticScholarshipStudentsModel Students { get; set; }
        public StatisticCandidatesModel Candidates { get; set; }
        public StatisticScholarshipCandidatesModel ScholarshipCandidates { get; set; }
        public List<KeyValue<UserStatusEnum, int>> Users { get; set; }
    }
}
