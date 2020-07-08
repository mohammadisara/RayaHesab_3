using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Pertb
    {
        public int Mid { get; set; }
        public int? Menuid { get; set; }
        public string Userid { get; set; }
        public string Title { get; set; }
        public int? Gkalaid { get; set; }
        public int? Idanbar { get; set; }
        public int? Gperson { get; set; }
        public int? Bank { get; set; }

        public Banktb BankNavigation { get; set; }
        public PanelTb Gkala { get; set; }
        public Gorohpersontb GpersonNavigation { get; set; }
        public Anbartb IdanbarNavigation { get; set; }
        public Menutb Menu { get; set; }
        public AspNetUsers User { get; set; }
    }
}
