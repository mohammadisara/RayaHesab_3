using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RayaHesab.Models
{
    public partial class factpersonrepview
    {
        [Key]
        public int Mid { get; set; }
        public int typec { get; set; }
        public int idanbar { get; set; }
        public int? Nofact { get; set; }
        public int? personkharid { get; set; }
        public int? idperson { get; set; }
        public string Namec { get; set; }
        public string Datec { get; set; }
        public decimal? pricekhales { get; set; }
        public decimal? pricekol { get; set; }
        public decimal? takhfif { get; set; }

    }
}
