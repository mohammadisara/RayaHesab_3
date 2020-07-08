using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Submointb
    {
        public Submointb()
        {
            Anbartb = new HashSet<Anbartb>();
            Banktb = new HashSet<Banktb>();
            Facttb = new HashSet<Facttb>();
            Otherhesabtb = new HashSet<Otherhesabtb>();
            PardartbIdtafvNavigation = new HashSet<Pardartb>();
            PardartbTafbedNavigation = new HashSet<Pardartb>();
            PardartbTafbesNavigation = new HashSet<Pardartb>();
            Pishfacttb = new HashSet<Pishfacttb>();
            RlateMstb = new HashSet<RlateMstb>();
            Sanadtb = new HashSet<Sanadtb>();
        }

        public int Mid { get; set; }
        public string Codetaf { get; set; }
        public string Nametaf { get; set; }
        public int? Pid { get; set; }

        public ICollection<Anbartb> Anbartb { get; set; }
        public ICollection<Banktb> Banktb { get; set; }
        public ICollection<Facttb> Facttb { get; set; }
        public ICollection<Otherhesabtb> Otherhesabtb { get; set; }
        public ICollection<Pardartb> PardartbIdtafvNavigation { get; set; }
        public ICollection<Pardartb> PardartbTafbedNavigation { get; set; }
        public ICollection<Pardartb> PardartbTafbesNavigation { get; set; }
        public ICollection<Pishfacttb> Pishfacttb { get; set; }
        public ICollection<RlateMstb> RlateMstb { get; set; }
        public ICollection<Sanadtb> Sanadtb { get; set; }
    }
}
