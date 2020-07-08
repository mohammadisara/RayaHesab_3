using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Subpacktb
    {
        public int Mid { get; set; }
        public int Packid { get; set; }
        public int Idkala { get; set; }

        public Kalatb IdkalaNavigation { get; set; }
        public Packtb Pack { get; set; }
    }
}
