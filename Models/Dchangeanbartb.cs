using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Dchangeanbartb
    {
        public int Mid { get; set; }
        public int? Idkala { get; set; }
        public int? Fromanbar { get; set; }
        public int? Toanbar { get; set; }
        public double? Tedad { get; set; }
        public string Note { get; set; }
        public decimal? Price { get; set; }
        public double? Pricekol { get; set; }
        public int? Masterid { get; set; }
        public double? Tedadd { get; set; }

        public Anbartb FromanbarNavigation { get; set; }
        public Kalatb IdkalaNavigation { get; set; }
        public Mchangeanbartb Master { get; set; }
        public Anbartb ToanbarNavigation { get; set; }
    }
}
