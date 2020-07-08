using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RayaHesab.Models
{
    public partial class viewforosh
    {
        [Key]
        public int Mid { get; set; }
        public int? Idanbar { get; set; }
        public string Nameanbar { get; set; }
        public string Datec { get; set; }
        public string Namekamel { get; set; }
        public double? Pkol { get; set; }
        public decimal? Takh { get; set; }
        public double? Pkhales { get; set; }
        public string Note { get; set; }
        public int? Nofact { get; set; }
        public int? Typec { get; set; }

    }
    public partial class viewkharid
    {
        [Key]
        public int Mid { get; set; }
        public int? Idanbar { get; set; }
        public string Nameanbar { get; set; }
        public string Datec { get; set; }
        public string Namekamel { get; set; }
        public double? Pkol { get; set; }
        public decimal? Takh { get; set; }
        public double? Pkhales { get; set; }
        public string Note { get; set; }
        public int? Nofact { get; set; }
        public int? Typec { get; set; }
    }

}
