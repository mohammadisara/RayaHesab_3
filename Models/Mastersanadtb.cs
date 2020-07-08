using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Mastersanadtb
    {
        public Mastersanadtb()
        {
            Sanadtb = new HashSet<Sanadtb>();
        }

        public string Datec { get; set; }
        public int Nosanad { get; set; }
        public string Title { get; set; }
        public int? Typec { get; set; }

        public ICollection<Sanadtb> Sanadtb { get; set; }
    }
}
