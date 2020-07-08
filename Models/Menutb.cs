using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Menutb
    {
        public Menutb()
        {
            Pertb = new HashSet<Pertb>();
            Shortcuttb = new HashSet<Shortcuttb>();
        }

        public int Mid { get; set; }
        public int Menuid { get; set; }
        public string Title { get; set; }
        public int? Msort { get; set; }
        public string Controller { get; set; }
        public string Actionname { get; set; }
        public string Rout1 { get; set; }
        public string Rout2 { get; set; }
        public string Valrout1 { get; set; }
        public string Valrout2 { get; set; }

        public Mastermenutb Menu { get; set; }
        public ICollection<Pertb> Pertb { get; set; }
        public ICollection<Shortcuttb> Shortcuttb { get; set; }
    }
}
