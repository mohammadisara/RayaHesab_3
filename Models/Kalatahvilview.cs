using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RayaHesab.Models
{
    public partial class Kalatahvilview
    {
        [Key]
        public Int32 Mid { get; set; }
        public int? Idfact { get; set; }
        public double? Tahvil { get; set; }
        public double? Tedaddar { get; set; }
        public double? Tedadv { get; set; }
        public int Idkala { get; set; }
        public int Codekala { get; set; }
        public string Namekala { get; set; }
        public string Notef { get; set; }
        public string Vahed1 { get; set; }
        public string Vahed2 { get; set; }
        public string Nameanbar { get; set; }
        public decimal Price { get; set; }
        public double? Pricekol { get; set; }
        public decimal? Takhfif { get; set; }
        public decimal? Takhfifsh { get; set; }
        public double? Pricekhales { get; set; }
        public int Idanbar { get; set; }
    }
}
