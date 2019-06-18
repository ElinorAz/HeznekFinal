using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class GeneralEntity : PersistentEntity<int>
    {
        public int PsychometricGrade { get; set; }
        public bool WorthyOfAdvancment { get; set; }
        public int Points { get; set; }
        public bool Disabilities { get; set; }

        public ProfileEntity Profile { get; set; }
        public List<GeneralParticipationEntity> Participations { get; set; }
    }
}
