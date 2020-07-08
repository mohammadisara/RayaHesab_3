using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class PanelTb
    {
        public PanelTb()
        {
            InverseCodeparentNavigation = new HashSet<PanelTb>();
            Kalatb = new HashSet<Kalatb>();
            Pertb = new HashSet<Pertb>();
        }

        public int Mid { get; set; }
        public string Harf { get; set; }
        public string Namep { get; set; }
        public int? Codeparent { get; set; }
        public int? Typec { get; set; }
        public int? Sortc { get; set; }
        public string Fullname { get; set; }
        public string Css { get; set; }
        public int? Fcode { get; set; }
        public int? Codegroup { get; set; }

        public PanelTb CodeparentNavigation { get; set; }
        public ICollection<PanelTb> InverseCodeparentNavigation { get; set; }
        public ICollection<Kalatb> Kalatb { get; set; }
        public ICollection<Pertb> Pertb { get; set; }
    }
}
