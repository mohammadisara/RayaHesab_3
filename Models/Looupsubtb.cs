using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Looupsubtb
    {
        public Looupsubtb()
        {
            KalatbIdclassNavigation = new HashSet<Kalatb>();
            KalatbIdvahed1Navigation = new HashSet<Kalatb>();
            KalatbIdvahed2Navigation = new HashSet<Kalatb>();
            KalatbTitleprintNavigation = new HashSet<Kalatb>();
        }

        public int Mid { get; set; }
        public int Grp { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public string Entitle { get; set; }
        public int? Valuec { get; set; }

        public ICollection<Kalatb> KalatbIdclassNavigation { get; set; }
        public ICollection<Kalatb> KalatbIdvahed1Navigation { get; set; }
        public ICollection<Kalatb> KalatbIdvahed2Navigation { get; set; }
        public ICollection<Kalatb> KalatbTitleprintNavigation { get; set; }
    }
}
