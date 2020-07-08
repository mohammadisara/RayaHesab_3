using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public class Daftarkol
    {
        [Key]
        public Int64 Mid { get; set; }
        public string Datec { get; set; }
        public string Note { get; set; }
        public int? Nosanad { get; set; }
        public int? Factid { get; set; }
        public decimal? Bed { get; set; }
        public decimal? Bes{ get; set; }
        public int? Userid { get; set; }
        public int? Idanbar { get; set; }
    }
}
