using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Mastertahviltb
    {
        public int Mid { get; set; }
        public int? Idtahvil { get; set; }
        public int? Idersal { get; set; }
        public int? Nofact { get; set; }
        public string Datec { get; set; }
        public int? Statec { get; set; }
        public int? Mamid { get; set; }
        public int? Tedad { get; set; }

        public Anbartb IdersalNavigation { get; set; }
        public Anbartb IdtahvilNavigation { get; set; }
    }
}
