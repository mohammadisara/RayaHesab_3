using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Darkhasttb
    {
        public long Mid { get; set; }
        public int? Kalaid { get; set; }
        public int? Personid { get; set; }
        public string Datec { get; set; }

        public Kalatb Kala { get; set; }
        public Persontb Person { get; set; }
    }
}
