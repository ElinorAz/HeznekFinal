using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class EventModel:PersistentModel<int>
    {
        public string Name { get; set; }
        public string Subject { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string FinishTime { get; set; }
        public string Location { get; set; }
        public int Expected { get; set; }
        public bool? Attend { get; set; }

        public List<UserModel> Attending { get; set; }
        public List<UserModel> NotAttending { get; set; }
        public List<UserStatusEnum> ParticipantTypes { get; set; }
    }
}
