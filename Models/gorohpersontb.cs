using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Gorohpersontb
    {
        public Gorohpersontb()
        {
            Persontb = new HashSet<Persontb>();
            Pertb = new HashSet<Pertb>();
        }

        public int Mid { get; set; }
        public int Moinid { get; set; }
        public string Title { get; set; }

        public Mointb Moin { get; set; }
        public ICollection<Persontb> Persontb { get; set; }
        public ICollection<Pertb> Pertb { get; set; }
    }
}
