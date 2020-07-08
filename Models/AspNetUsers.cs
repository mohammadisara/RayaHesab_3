using System;
using System.Collections.Generic;

namespace RayaHesab.Models
{
    public partial class AspNetUsers
    {
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            AspNetUserRoles = new HashSet<AspNetUserRoles>();
            AspNetUserTokens = new HashSet<AspNetUserTokens>();
            Pardartb = new HashSet<Pardartb>();
            Pertb = new HashSet<Pertb>();
            Sanadtb = new HashSet<Sanadtb>();
        }

        public string Id { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? Isactive { get; set; }
        public int? Idanbar { get; set; }

        public Anbartb IdanbarNavigation { get; set; }
        public ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }
        public ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }
        public ICollection<AspNetUserRoles> AspNetUserRoles { get; set; }
        public ICollection<AspNetUserTokens> AspNetUserTokens { get; set; }
        public ICollection<Pardartb> Pardartb { get; set; }
        public ICollection<Pertb> Pertb { get; set; }
        public ICollection<Sanadtb> Sanadtb { get; set; }
    }
}
