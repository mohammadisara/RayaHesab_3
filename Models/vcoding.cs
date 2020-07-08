using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RayaHesab.Models
{
    public class vcoding
    {
        public int? mid { get; set; }
        public int? idtaf { get; set; }
        public string codetaf { get; set; }
        public string nametaf { get; set; }
        public string codemoin { get; set; }
        public int? moinid { get; set; }
        public string namemoin { get; set; }
        public int? kid { get; set; }
        public string codekol { get; set; }
        public string namekol { get; set; }
    }
}
//            modelBuilder.Query<vcoding>().ToView("vcoding");
