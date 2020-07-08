using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Otherhesabtb
    {
        public int Mid { get; set; }
        public string Namec { get; set; }
        public int? Idmoin { get; set; }
        public int? Idtaf { get; set; }
        public int? Pid { get; set; }

        public Mointb IdmoinNavigation { get; set; }
        public Submointb IdtafNavigation { get; set; }
        public Persontb P { get; set; }
    }
}
