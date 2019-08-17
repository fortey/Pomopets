using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor.Models
{
    public class PomidorViewModel
    {
        public TimeSpan Rest { get; set; }
        public DateTime EndTime { get; set; }
        public string Time { get
            {
                return EndTime.ToString("yyyy-MM-ddTHH:mm:ss");
            } }
    }
}
