using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RayaHesab.Models
{
    public partial class Sp_gardeshkala
    {
        [Key]
        public Int64 Rnk { get; set; }
        public Int32 Mid { get; set; }
        public int? Typec { get; set; }
        public int? Factid { get; set; }
        public int? Nofact { get; set; }
        public int? Codekala { get; set; }
        public string Datec { get; set; }
        public string Namekala { get; set; }
        public string Namep { get; set; }
        public string Note { get; set; }
        public string Nameanbar { get; set; }
        public Double? Tedadv { get; set; }
        public Double? Tedads { get; set; }
        public Double? Mandeh { get; set; }
        public decimal? Price { get; set; }
        public decimal? Pricekol { get; set; }
        public int? Idkala { get; set; }
        public decimal? Miyangin { get; set; }
        

    }
}
