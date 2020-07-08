using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Hesabusertb
    {
        public int Mid { get; set; }
        public int Typehesab { get; set; }
        public string Userid { get; set; }
        public int Hesabid { get; set; }
        public bool? Isdef { get; set; }

        public Usertb User { get; set; }
    }
}
