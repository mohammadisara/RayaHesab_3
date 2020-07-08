using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RayaHesab.Models
{
    public partial class Sp_mojodikala
    {
        [Key]
        public Int64 Rnk { get; set; }
        public int? Codekala { get; set; }
        public string Namekala { get; set; }
        public Double? Tv { get; set; }
        public Double? Ts { get; set; }
        public Double? Mandeh { get; set; }
        public decimal? Price { get; set; }
        public double? Pricekol { get; set; }
        public int? Idkala { get; set; }
        public int? Idanbar { get; set; }
        public string Nameanbar { get; set; }


    }
}
