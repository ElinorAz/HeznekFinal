using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class EventEntity: PersistentEntity<int>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string FinishTime { get; set; }
        public string Location { get; set; }
        public int Expected { get; set; }

        public List<EventAttendeeEntity> Attendees { get; set; }
        public List<EventParticipantTypeEntity> ParticipantTypes { get; set; }

    }
}
