using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Kalatb
    {
        public Kalatb()
        {
            Darkhasttb = new HashSet<Darkhasttb>();
            Dchangeanbartb = new HashSet<Dchangeanbartb>();
            Dtolidtb = new HashSet<Dtolidtb>();
            Factkalatb = new HashSet<Factkalatb>();
            Rlate2kalatbIdkalakolNavigation = new HashSet<Rlate2kalatb>();
            Rlate2kalatbIdkalarNavigation = new HashSet<Rlate2kalatb>();
            Rlatekalatb = new HashSet<Rlatekalatb>();
            Subpacktb = new HashSet<Subpacktb>();
        }

        public int Mid { get; set; }
        public int Codekala { get; set; }
        public string Namekala { get; set; }
        public double? Vahed1 { get; set; }
        public double? Vahed2 { get; set; }
        public decimal? Price1 { get; set; }
        public decimal? Price2 { get; set; }
        public int? Ismanfi { get; set; }
        public int? Methodc { get; set; }
        public int Isauto { get; set; }
        public string Barcode { get; set; }
        public int? Idvahed1 { get; set; }
        public int? Idvahed2 { get; set; }
        public int? Gid { get; set; }
        public string Addpic { get; set; }
        public int? Titleprint { get; set; }
        public int? Idclass { get; set; }

        public PanelTb G { get; set; }
        public Looupsubtb IdclassNavigation { get; set; }
        public Looupsubtb Idvahed1Navigation { get; set; }
        public Looupsubtb Idvahed2Navigation { get; set; }
        public Looupsubtb TitleprintNavigation { get; set; }
        public ICollection<Darkhasttb> Darkhasttb { get; set; }
        public ICollection<Dchangeanbartb> Dchangeanbartb { get; set; }
        public ICollection<Dtolidtb> Dtolidtb { get; set; }
        public ICollection<Factkalatb> Factkalatb { get; set; }
        public ICollection<Rlate2kalatb> Rlate2kalatbIdkalakolNavigation { get; set; }
        public ICollection<Rlate2kalatb> Rlate2kalatbIdkalarNavigation { get; set; }
        public ICollection<Rlatekalatb> Rlatekalatb { get; set; }
        public ICollection<Subpacktb> Subpacktb { get; set; }
    }
}
