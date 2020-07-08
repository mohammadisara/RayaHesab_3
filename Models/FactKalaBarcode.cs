using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public partial class FactKalaBarcode
    {
        public int? Idfact { get; set; }
        public int Idkala { get; set; }
        public int? Codekala { get; set; }
        public string Namekala { get; set; }
        public int Tedadv { get; set; }
        public decimal? Price { get; set; }
        public decimal? Pricekol { get; set; }


    }
}
