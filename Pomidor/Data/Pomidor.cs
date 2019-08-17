using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Pomidor
{
    public class Pomidor
    {
        public static int MoneyAward { get; set; } = 10;
        public static int ExperienceAward { get; set; } = 10;

        public int ID { get; set; }
        //public const int DurationTime = 25;
        public string UserID { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public bool PrizeIssued { get; set; }
        public TimeSpan Rest {
            get
            {
                var rest = EndTime.Subtract(DateTime.UtcNow);
                if (rest.TotalMilliseconds < 0) return TimeSpan.Zero;
                return rest;
            }
        }

        public void Start(int duration)
        {
            Duration = duration;
            EndTime = DateTime.UtcNow.AddMinutes(duration);
            PrizeIssued = false;
        }

    }
}
