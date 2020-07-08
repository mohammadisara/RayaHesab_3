using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Sandoghtb
    {
        public int Mid { get; set; }
        public int? Moinid { get; set; }
        public int? Tafid { get; set; }
        public int? Pid { get; set; }
        public string Title { get; set; }

        public Mointb Moin { get; set; }
        public Persontb P { get; set; }
        public Submointb Taf { get; set; }
    }
}
