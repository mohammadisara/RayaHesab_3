using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Pishfacttb
    {
        public int Mid { get; set; }
        public int? Ispublic { get; set; }
        public string Datec { get; set; }
        public string Note { get; set; }
        public int? Idmoin { get; set; }
        public int? Idtaf { get; set; }
        public int? Idperson { get; set; }
        public int? Idbank { get; set; }
        public int? Typec { get; set; }
        public int? Nofact { get; set; }
        public int? Act { get; set; }
        public int? Idbazar { get; set; }
        public string Notetakh { get; set; }
        public int? Idanbar { get; set; }
        public bool? Istel { get; set; }
        public int? Userid { get; set; }
        public decimal? Tmpprice { get; set; }
        public decimal? Takhfact { get; set; }
        public string Title { get; set; }
        public int? Peyvast { get; set; }
        public double? Darsadtakh { get; set; }
        public decimal? Sahmb { get; set; }
        public double? Dsahmb { get; set; }
        public int? Typesahmb { get; set; }
        public int? Idchange { get; set; }
        public int? Idtolid { get; set; }

        public Anbartb IdanbarNavigation { get; set; }
        public Banktb IdbankNavigation { get; set; }
        public Persontb IdbazarNavigation { get; set; }
        public Mointb IdmoinNavigation { get; set; }
        public Persontb IdpersonNavigation { get; set; }
        public Submointb IdtafNavigation { get; set; }
    }
}
