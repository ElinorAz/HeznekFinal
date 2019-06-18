using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class CandidateAdditionalDataEntity:PersistentEntity<int>
    {
        public string ParticipationDescription { get; set; }
        public string Reason { get; set; }
        public string Difficulties { get; set; }
        public string Status { get; set; }
        public string LifeStory { get; set; }
        public string SituationDetails { get; set; }

        public string HealthProblems { get; set; }
        public string FinancialProblems { get; set; }
        public string FamilyDifficulties { get; set; }
        public bool HasHealthProblems { get; set; }
        public bool HasFinancialProblems { get; set; }
        public bool HasFamilyDifficulties { get; set; }

        public ProfileEntity Profile { get; set; }
    }
}
