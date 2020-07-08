using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class RlateMstb
    {
        public int Mid { get; set; }
        public int Moinid { get; set; }
        public int Tafid { get; set; }

        public Mointb Moin { get; set; }
        public Submointb Taf { get; set; }
    }
}
