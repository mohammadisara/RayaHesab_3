using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Rlate2kalatb
    {
        public int Mid { get; set; }
        public int? Idkalar { get; set; }
        public int? Idkalakol { get; set; }
        public double? Tedad { get; set; }
        public double? Tedadd { get; set; }

        public Kalatb IdkalakolNavigation { get; set; }
        public Kalatb IdkalarNavigation { get; set; }
    }
}
