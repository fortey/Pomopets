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
        public int Experience { get; set; }

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
