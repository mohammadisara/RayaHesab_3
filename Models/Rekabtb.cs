using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Rekabtb
    {
        public Rekabtb()
        {
            Anbarrekabtb = new HashSet<Anbarrekabtb>();
            Rlaterekabnegintb = new HashSet<Rlaterekabnegintb>();
        }

        public int Mid { get; set; }
        public string Codem { get; set; }
        public string Namec { get; set; }
        public int? Idvahed { get; set; }

        public Looupsubtb IdvahedNavigation { get; set; }
        public ICollection<Anbarrekabtb> Anbarrekabtb { get; set; }
        public ICollection<Rlaterekabnegintb> Rlaterekabnegintb { get; set; }
    }
}
