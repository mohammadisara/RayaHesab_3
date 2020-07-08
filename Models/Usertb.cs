using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class Usertb
    {
        public Usertb()
        {
            Hesabusertb = new HashSet<Hesabusertb>();
            Mchangeanbartb = new HashSet<Mchangeanbartb>();
            Shortcuttb = new HashSet<Shortcuttb>();
        }

        public string Mid { get; set; }
        public string Namec { get; set; }
        public string Pass { get; set; }
        public int? Typec { get; set; }
        public string Mobile { get; set; }
        public string Pass2 { get; set; }
        public TimeSpan? Timec { get; set; }
        public int? Idanbar { get; set; }

        public Anbartb IdanbarNavigation { get; set; }
        public ICollection<Hesabusertb> Hesabusertb { get; set; }
        public ICollection<Mchangeanbartb> Mchangeanbartb { get; set; }
        public ICollection<Shortcuttb> Shortcuttb { get; set; }
    }
}
