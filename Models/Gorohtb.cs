using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Gorohtb
    {
        public Gorohtb()
        {
            Koltb = new HashSet<Koltb>();
        }

        public int Mid { get; set; }
        public string Namec { get; set; }

        public ICollection<Koltb> Koltb { get; set; }
    }
}
