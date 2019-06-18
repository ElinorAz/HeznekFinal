using System;
using System.Collections.Generic;
using System.Text;

namespace Heznek.Common.Enums
{
    [Flags]
    public enum ParticipationEnum
    {
        None = 0,
        Heznek = 1,
        StartupToIndustry = 2,
        HeznekMarkntil = 4,
        HeznekPreFutures = 8
    }
}
