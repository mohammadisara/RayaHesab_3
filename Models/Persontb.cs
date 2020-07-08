using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Persontb
    {
        public Persontb()
        {
            Darkhasttb = new HashSet<Darkhasttb>();
            Factkalatb = new HashSet<Factkalatb>();
            FacttbIdbazarNavigation = new HashSet<Facttb>();
            FacttbIdpersonNavigation = new HashSet<Facttb>();
            FacttbPersonkhar = new HashSet<Facttb>();
            Mastertolidtb = new HashSet<Mastertolidtb>();
            Otherhesabtb = new HashSet<Otherhesabtb>();
            PardartbPidbedNavigation = new HashSet<Pardartb>();
            PardartbPidbesNavigation = new HashSet<Pardartb>();
            PardartbPidvNavigation = new HashSet<Pardartb>();
            PishfacttbIdbazarNavigation = new HashSet<Pishfacttb>();
            PishfacttbIdpersonNavigation = new HashSet<Pishfacttb>();
            Sanadtb = new HashSet<Sanadtb>();
        }

        public int Mid { get; set; }
        public string Namec { get; set; }
        public string Family { get; set; }
        public string Mobile { get; set; }
        public string Addressc { get; set; }
        public string Datetavalod { get; set; }
        public int? Statec { get; set; }
        public int? Jensiat { get; set; }
        public int Gid { get; set; }
        public string Namekamel { get; set; }
        public string Codegh { get; set; }
        public int? Typeperson { get; set; }
        public string Note { get; set; }
        public int? Bimarid { get; set; }
        public string Laghab { get; set; }
        public string Tel { get; set; }
        public string Mob2 { get; set; }
        public string Codemeli { get; set; }

        public Gorohpersontb G { get; set; }
        public ICollection<Darkhasttb> Darkhasttb { get; set; }
        public ICollection<Factkalatb> Factkalatb { get; set; }
        public ICollection<Facttb> FacttbIdbazarNavigation { get; set; }
        public ICollection<Facttb> FacttbIdpersonNavigation { get; set; }
        public ICollection<Facttb> FacttbPersonkhar { get; set; }
        public ICollection<Mastertolidtb> Mastertolidtb { get; set; }
        public ICollection<Otherhesabtb> Otherhesabtb { get; set; }
        public ICollection<Pardartb> PardartbPidbedNavigation { get; set; }
        public ICollection<Pardartb> PardartbPidbesNavigation { get; set; }
        public ICollection<Pardartb> PardartbPidvNavigation { get; set; }
        public ICollection<Pishfacttb> PishfacttbIdbazarNavigation { get; set; }
        public ICollection<Pishfacttb> PishfacttbIdpersonNavigation { get; set; }
        public ICollection<Sanadtb> Sanadtb { get; set; }
    }
}
