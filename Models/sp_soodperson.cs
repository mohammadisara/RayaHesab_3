using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public class sp_soodperson
    {
        [Key]
        public int pid { get; set; }

        public string  namec { get; set; }
        public decimal? dkh { get; set; }
        public decimal? kh { get; set; }
        public decimal? pfforosh { get; set; }
        public decimal? sforosh { get; set; }
        public decimal? tahvilforosh { get; set; }
        public decimal? forosh { get; set; }
        public decimal? backkh { get; set; }
        public decimal? backf { get; set; }
        public decimal? sood { get; set; }

    }
}
