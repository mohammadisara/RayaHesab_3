using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Rlaterekabnegintb
    {
        public int Mid { get; set; }
        public int? Mamid { get; set; }
        public int? Neginid { get; set; }
        public int? Tedad { get; set; }

        public Rekabtb Mam { get; set; }
        public Negintb Negin { get; set; }
    }
}
