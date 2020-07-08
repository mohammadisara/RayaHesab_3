using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Pardartb
    {
        public Pardartb()
        {
            Sanadtb = new HashSet<Sanadtb>();
        }

        public int Mid { get; set; }
        public string Datec { get; set; }
        public int? Moinbed { get; set; }
        public int? Tafbed { get; set; }
        public int? Pidbed { get; set; }
        public string Note { get; set; }
        public int? Moinbes { get; set; }
        public int? Tafbes { get; set; }
        public int? Pidbes { get; set; }
        public string Nocheckq { get; set; }
        public string Datecheck { get; set; }
        public string Noghabz { get; set; }
        public int? Typec { get; set; }
        public decimal? Mablagh { get; set; }
        public int? Sanbed { get; set; }
        public int? Sanbes { get; set; }
        public string Userid { get; set; }
        public int? Idanbar { get; set; }
        public int? Statecheckq { get; set; }
        public string Datestatecheckq { get; set; }
        public int? Idfact { get; set; }
        public int? Idmoinv { get; set; }
        public int? Idtafv { get; set; }
        public int? Pidv { get; set; }
        public string Bankdar { get; set; }
        public string Hesabdar { get; set; }

        public Anbartb IdanbarNavigation { get; set; }
        public Facttb IdfactNavigation { get; set; }
        public Mointb IdmoinvNavigation { get; set; }
        public Submointb IdtafvNavigation { get; set; }
        public Mointb MoinbedNavigation { get; set; }
        public Mointb MoinbesNavigation { get; set; }
        public Persontb PidbedNavigation { get; set; }
        public Persontb PidbesNavigation { get; set; }
        public Persontb PidvNavigation { get; set; }
        public Banktb SanbedNavigation { get; set; }
        public Banktb SanbesNavigation { get; set; }
        public Submointb TafbedNavigation { get; set; }
        public Submointb TafbesNavigation { get; set; }
        public AspNetUsers User { get; set; }
        public ICollection<Sanadtb> Sanadtb { get; set; }
    }
}
