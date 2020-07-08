using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Dtolidtb
    {
        public int Mid { get; set; }
        public int? Idkala { get; set; }
        public int? Idanbar { get; set; }
        public double? Tedad { get; set; }
        public decimal? Price { get; set; }
        public int? Masterid { get; set; }
        public double? Tedadd { get; set; }
        public int? Typeexit { get; set; }
        public string Noted { get; set; }
        public double? Vazn { get; set; }

        public Anbartb IdanbarNavigation { get; set; }
        public Kalatb IdkalaNavigation { get; set; }
        public Mastertolidtb Master { get; set; }
    }
}
