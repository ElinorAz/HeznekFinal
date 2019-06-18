using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.DataAccess.Entities
{
    public class EventParticipantTypeEntity:PersistentEntity<int>
    {
        public int EventId { get; set; }
        public EventEntity Event { get; set; }

        public UserStatusEnum UserType { get; set; }

    }
}
