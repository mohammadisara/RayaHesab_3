using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Rlatekalatb
    {
        public int Mid { get; set; }
        public int? Kalaid { get; set; }
        public int? Gid { get; set; }

        public Gorohkalatb G { get; set; }
        public Kalatb Kala { get; set; }
    }
}
