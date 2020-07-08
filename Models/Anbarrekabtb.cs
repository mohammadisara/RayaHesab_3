using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Anbarrekabtb
    {
        public int Mid { get; set; }
        public int? Idanbar { get; set; }
        public int? Tedadv { get; set; }
        public int? Tedads { get; set; }
        public int? Rekabid { get; set; }
        public int? Idfact { get; set; }

        public Facttb IdfactNavigation { get; set; }
        public Rekabtb Rekab { get; set; }
    }
}
