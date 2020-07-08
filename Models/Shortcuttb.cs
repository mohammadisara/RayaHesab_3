using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Shortcuttb
    {
        public int Mid { get; set; }
        public int? Menuid { get; set; }
        public string Userid { get; set; }
        public int? Msort { get; set; }

        public Menutb Menu { get; set; }
        public Usertb User { get; set; }
    }
}
