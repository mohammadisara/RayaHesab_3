using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Checkqtb
    {
        public int Nobarg { get; set; }
        public int Statec { get; set; }
        public int Mcheckq { get; set; }
        public int Mid { get; set; }

        public Masterchack McheckqNavigation { get; set; }
    }
}
