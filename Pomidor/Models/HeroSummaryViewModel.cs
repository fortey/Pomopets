using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor
{
    public class HeroSummaryViewModel
    {
        public bool IsAuthenticated { get; set; } = false;
        public int Level { get; set; }
        public int Experience { get; set; }
        public int ExperienceLimit { get; set; }
        public int Money { get; set; }
    }
}
