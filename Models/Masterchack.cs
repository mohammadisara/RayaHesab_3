using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Masterchack
    {
        public Masterchack()
        {
            Checkqtb = new HashSet<Checkqtb>();
        }

        public int Mid { get; set; }
        public int? Frombarg { get; set; }
        public int? Tobarg { get; set; }
        public string Title { get; set; }
        public int? Bankid { get; set; }

        public Banktb Bank { get; set; }
        public ICollection<Checkqtb> Checkqtb { get; set; }
    }
}
