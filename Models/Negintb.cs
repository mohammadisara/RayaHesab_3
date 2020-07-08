using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Negintb
    {
        public Negintb()
        {
            Rlaterekabnegintb = new HashSet<Rlaterekabnegintb>();
        }

        public int Mid { get; set; }
        public string Coden { get; set; }
        public string Namec { get; set; }
        public double? Vazn { get; set; }
        public int? Idvahed { get; set; }

        public Looupsubtb IdvahedNavigation { get; set; }
        public ICollection<Rlaterekabnegintb> Rlaterekabnegintb { get; set; }
    }
}
