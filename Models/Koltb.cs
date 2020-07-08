using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Koltb
    {
        public Koltb()
        {
            Mointb = new HashSet<Mointb>();
        }

        public int Mid { get; set; }
        public int? Gid { get; set; }
        public string Namekol { get; set; }
        public string Codekol { get; set; }
        public int? Typec { get; set; }
        public int? Kind { get; set; }

        public Gorohtb G { get; set; }
        public ICollection<Mointb> Mointb { get; set; }
    }
}
