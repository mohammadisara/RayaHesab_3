using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public class Daftarperson
    {
        [Key]
        public string Mid { get; set; }
        public string Datec { get; set; }
        public string Note { get; set; }
        public string Codehesab { get; set; }
        public string Namehesab { get; set; }
        public string Nameanbar { get; set; }
        public int? Nosanad { get; set; }
        public int? Factid { get; set; }
        public decimal? Bed { get; set; }
        public decimal? Bes{ get; set; }
        public decimal? Bedn { get; set; }
        public decimal? Besn { get; set; }
        public int? Userid { get; set; }
        public int? Idanbar { get; set; }
        public int? Idmoin { get; set; }
    }
}
