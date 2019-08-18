using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pomidor
{
    public class GoodViewModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string ImgFolder { get; set; }
        public string Image { get => $"{ImgFolder}/1.png"; }
        public bool IsAvailable { get; set; }
    }
}
