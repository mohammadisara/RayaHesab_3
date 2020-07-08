using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Sanadtb
    {
        public int Mid { get; set; }
        public int Nosanad { get; set; }
        public int Moinid { get; set; }
        public int? Tafid { get; set; }
        public int? Pid { get; set; }
        public string Note { get; set; }
        public decimal? Bed { get; set; }
        public decimal? Bes { get; set; }
        public int? Pardarid { get; set; }
        public int? Factid { get; set; }
        public string Userid { get; set; }
        public int? Idanbar { get; set; }
        public DateTime? Datemiladi { get; set; }
        public string Nofish { get; set; }
        public int? Factpid { get; set; }
        public int? Sbimarid { get; set; }
        public int? Isedit { get; set; }
        public int? Idchange { get; set; }

        public Facttb Fact { get; set; }
        public Anbartb IdanbarNavigation { get; set; }
        public Mointb Moin { get; set; }
        public Mastersanadtb NosanadNavigation { get; set; }
        public Persontb P { get; set; }
        public Pardartb Pardar { get; set; }
        public Submointb Taf { get; set; }
        public AspNetUsers User { get; set; }
    }
}
