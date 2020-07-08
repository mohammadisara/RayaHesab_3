using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public class Viewkalabarcode
    {
        public int Idfact { get; set; }
        public int Idkala { get; set; }
        public double? Tedadv { get; set; }
        public decimal? Price { get; set; }
        public double? Pricekol { get; set; }
        public string Codekala { get; set; }
        public string Namekala { get; set; }

    }
}
