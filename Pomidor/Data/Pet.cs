using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor
{
    public class Pet
    {
        public static int[] Levels = new int[]{
            50,  100,  200,  400,  800
        };

        public int ID { get; set; }
        public string UserID { get; set; }

        public int TypeID { get; set; }
        public TypeOfPet Type { get; set; }

        public int Level { get; set; }
        public int Experience { get; private set; }

        public int ExperienceLimit { get => Levels[Level]; }

        public string Image { get => $"{Type?.ImgFolder ?? String.Empty}/{Level}.png"; }

        public void AddExperience(int count)
        {
            Experience += count;
            if (Experience >= Levels[Level] && Level+1 < Levels.Length)
            {
                Experience -= Levels[Level];
                Level++;
            }
        }
    }
}
