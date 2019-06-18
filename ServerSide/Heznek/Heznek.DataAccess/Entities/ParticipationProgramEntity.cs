using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class ParticipationProgramEntity:PersistentEntity<int>
    {
        public string ProgramName { get; set; }

        public List<GeneralParticipationEntity> Generals { get; set; }
    }
}
