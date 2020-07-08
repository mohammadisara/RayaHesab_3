using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Gorohkalatb
    {
        public Gorohkalatb()
        {
            Rlatekalatb = new HashSet<Rlatekalatb>();
        }

        public int Mid { get; set; }
        public string Namegoroh { get; set; }

        public ICollection<Rlatekalatb> Rlatekalatb { get; set; }
    }
}
