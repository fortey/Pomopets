using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor
{
    public class Player : IdentityUser
    {
        public static int[] Levels = new int[]{
            100,  210,  330,  450,  600,
            800, 1100, 1400, 1800, 2300
        };
        public int Money { get; set; }
        public int Experience { get; set; }
        public int PrimaryPet { get; set; }
        public int Level { get; set; }

        public void AddExperience(int count)
        {
            Experience += count;
            if (Experience >= Levels[Level])
            {
                Experience -= Levels[Level];
                Level++;
            }
        }
    }
}
