using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Mastertolidtb
    {
        public Mastertolidtb()
        {
            Dtolidtb = new HashSet<Dtolidtb>();
        }

        public int Mid { get; set; }
        public int? Pid { get; set; }
        public string Datec { get; set; }
        public string Note { get; set; }
        public int? Fanbar { get; set; }
        public int? Nofact { get; set; }
        public int? Statec { get; set; }

        public Anbartb FanbarNavigation { get; set; }
        public Persontb P { get; set; }
        public ICollection<Dtolidtb> Dtolidtb { get; set; }
    }
}
