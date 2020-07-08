using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Mchangeanbartb
    {
        public Mchangeanbartb()
        {
            Dchangeanbartb = new HashSet<Dchangeanbartb>();
        }

        public int Mid { get; set; }
        public string Datec { get; set; }
        public string Nametahvil { get; set; }
        public string Namegirande { get; set; }
        public string Note { get; set; }
        public int? Nofact { get; set; }
        public string Userid { get; set; }

        public Usertb User { get; set; }
        public ICollection<Dchangeanbartb> Dchangeanbartb { get; set; }
    }
}
