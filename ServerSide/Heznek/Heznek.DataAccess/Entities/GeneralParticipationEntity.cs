using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class GeneralParticipationEntity:PersistentEntity<int>
    {
        public GeneralEntity General { get; set; }
        public int GeneralId { get; set; }

        public string Description { get; set; }

        public ParticipationProgramEntity Program { get; set; }
        public int ProgramId { get; set; }
    }
}
