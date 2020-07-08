using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Anbartb
    {
        public Anbartb()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            DchangeanbartbFromanbarNavigation = new HashSet<Dchangeanbartb>();
            DchangeanbartbToanbarNavigation = new HashSet<Dchangeanbartb>();
            Dtolidtb = new HashSet<Dtolidtb>();
            Factkalatb = new HashSet<Factkalatb>();
            Facttb = new HashSet<Facttb>();
            Mastertolidtb = new HashSet<Mastertolidtb>();
            Pardartb = new HashSet<Pardartb>();
            Pertb = new HashSet<Pertb>();
            Pishfacttb = new HashSet<Pishfacttb>();
            Sanadtb = new HashSet<Sanadtb>();
            Usertb = new HashSet<Usertb>();
        }

        public int Mid { get; set; }
        public string Namec { get; set; }
        public int Moinid { get; set; }
        public int? Tafid { get; set; }

        public Mointb Moin { get; set; }
        public Submointb Taf { get; set; }
        public ICollection<AspNetUsers> AspNetUsers { get; set; }
        public ICollection<Dchangeanbartb> DchangeanbartbFromanbarNavigation { get; set; }
        public ICollection<Dchangeanbartb> DchangeanbartbToanbarNavigation { get; set; }
        public ICollection<Dtolidtb> Dtolidtb { get; set; }
        public ICollection<Factkalatb> Factkalatb { get; set; }
        public ICollection<Facttb> Facttb { get; set; }
        public ICollection<Mastertolidtb> Mastertolidtb { get; set; }
        public ICollection<Pardartb> Pardartb { get; set; }
        public ICollection<Pertb> Pertb { get; set; }
        public ICollection<Pishfacttb> Pishfacttb { get; set; }
        public ICollection<Sanadtb> Sanadtb { get; set; }
        public ICollection<Usertb> Usertb { get; set; }
    }
}
