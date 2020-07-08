using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Mointb
    {
        public Mointb()
        {
            Anbartb = new HashSet<Anbartb>();
            Banktb = new HashSet<Banktb>();
            Facttb = new HashSet<Facttb>();
            Gorohpersontb = new HashSet<Gorohpersontb>();
            Otherhesabtb = new HashSet<Otherhesabtb>();
            PardartbIdmoinvNavigation = new HashSet<Pardartb>();
            PardartbMoinbedNavigation = new HashSet<Pardartb>();
            PardartbMoinbesNavigation = new HashSet<Pardartb>();
            Pishfacttb = new HashSet<Pishfacttb>();
            RlateMstb = new HashSet<RlateMstb>();
            Sanadtb = new HashSet<Sanadtb>();
        }

        public int Mid { get; set; }
        public int? Kid { get; set; }
        public string Codemoin { get; set; }
        public string Namemoin { get; set; }

        public Koltb K { get; set; }
        public ICollection<Anbartb> Anbartb { get; set; }
        public ICollection<Banktb> Banktb { get; set; }
        public ICollection<Facttb> Facttb { get; set; }
        public ICollection<Gorohpersontb> Gorohpersontb { get; set; }
        public ICollection<Otherhesabtb> Otherhesabtb { get; set; }
        public ICollection<Pardartb> PardartbIdmoinvNavigation { get; set; }
        public ICollection<Pardartb> PardartbMoinbedNavigation { get; set; }
        public ICollection<Pardartb> PardartbMoinbesNavigation { get; set; }
        public ICollection<Pishfacttb> Pishfacttb { get; set; }
        public ICollection<RlateMstb> RlateMstb { get; set; }
        public ICollection<Sanadtb> Sanadtb { get; set; }
    }
}
