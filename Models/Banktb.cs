using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Banktb
    {
        public Banktb()
        {
            Facttb = new HashSet<Facttb>();
            Masterchack = new HashSet<Masterchack>();
            PardartbSanbedNavigation = new HashSet<Pardartb>();
            PardartbSanbesNavigation = new HashSet<Pardartb>();
            Pertb = new HashSet<Pertb>();
            Pishfacttb = new HashSet<Pishfacttb>();
        }

        public int Mid { get; set; }
        public string Title { get; set; }
        public string Nokart { get; set; }
        public string Nohesab { get; set; }
        public string Nosheba { get; set; }
        public int? Typec { get; set; }
        public int? Moinid { get; set; }
        public int? Tafid { get; set; }

        public Mointb Moin { get; set; }
        public Submointb Taf { get; set; }
        public ICollection<Facttb> Facttb { get; set; }
        public ICollection<Masterchack> Masterchack { get; set; }
        public ICollection<Pardartb> PardartbSanbedNavigation { get; set; }
        public ICollection<Pardartb> PardartbSanbesNavigation { get; set; }
        public ICollection<Pertb> Pertb { get; set; }
        public ICollection<Pishfacttb> Pishfacttb { get; set; }
    }
}
