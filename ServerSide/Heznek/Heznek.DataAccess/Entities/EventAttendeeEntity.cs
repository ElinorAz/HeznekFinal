using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class EventAttendeeEntity: PersistentEntity<int>
    {
        public int EventId { get; set; }
        public EventEntity Event { get; set; }

        public int ProfileId { get; set; }
        public ProfileEntity Profile { get; set; }
        
    }
}
