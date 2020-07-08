using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public class Tarazkol
    {
        [Key]
        public int Mid { get; set; }
        public int? Kid { get; set; }
        public string Codekol { get; set; }
        public string Namekol { get; set; }
        public string Nameanbar { get; set; }
        public decimal Bednow { get; set; }
        public decimal Besnow { get; set; }
        public decimal Bed { get; set; }
        public decimal Bes { get; set; }
    }
}
