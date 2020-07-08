using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Factkalatb
    {
        public int Mid { get; set; }
        public int? Idfact { get; set; }
        public int Idkala { get; set; }
        public string Namekala { get; set; }
        public string Namevahed { get; set; }
        public decimal? Price { get; set; }
        public double Tedadv { get; set; }
        public decimal? Pricekharid { get; set; }
        public decimal? Takhfif { get; set; }
        public decimal? Takhfifsh { get; set; }
        public double? Maliyat { get; set; }
        public int? Act { get; set; }
        public int Idanbar { get; set; }
        public double? Tedaddar { get; set; }
        public int? Idbazar { get; set; }
        public decimal? Sahmbazar { get; set; }
        public decimal? Pricemiyan { get; set; }
        public int? Typec { get; set; }
        public double? Tedadvahed1 { get; set; }
        public string Notef { get; set; }
        public int? Iddtolid { get; set; }
        public int? Iddchange { get; set; }
        public int? Isshow { get; set; }
        public double? Pricekol { get; set; }
        public double? Pricekhales { get; set; }

        public Anbartb IdanbarNavigation { get; set; }
        public Persontb IdbazarNavigation { get; set; }
        public Facttb IdfactNavigation { get; set; }
        public Kalatb IdkalaNavigation { get; set; }
    }
}
