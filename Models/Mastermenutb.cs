using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Mastermenutb
    {
        public Mastermenutb()
        {
            Menutb = new HashSet<Menutb>();
        }

        public int Mid { get; set; }
        public string Title { get; set; }
        public int? Mastersort { get; set; }
        public int? Act { get; set; }
        public string Enmenu { get; set; }
        public string Icon { get; set; }

        public ICollection<Menutb> Menutb { get; set; }
    }
}
