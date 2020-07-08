using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Packtb
    {
        public Packtb()
        {
            Subpacktb = new HashSet<Subpacktb>();
        }

        public int Mid { get; set; }
        public string Namepak { get; set; }
        public string Codepack { get; set; }

        public ICollection<Subpacktb> Subpacktb { get; set; }
    }
}
