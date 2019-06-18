using Heznek.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Services.Models
{
    public class GeneralModel
    {
        public int PsychometricGrade { get; set; }
        public bool WorthyOfAdvancment { get; set; }
        public List<IdValue> ParticipationInPrograms { get; set; }
        public int Points { get; set; }
        public bool Disabilities { get; set; }
    }
}
