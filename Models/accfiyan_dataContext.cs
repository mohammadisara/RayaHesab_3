using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace RayaHesab.Models
{
    public partial class accfiyan_dataContext : DbContext
    {
        public accfiyan_dataContext()
        {
        }

        public accfiyan_dataContext(DbContextOptions<accfiyan_dataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Anbartb> Anbartb { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<Banktb> Banktb { get; set; }
        public virtual DbSet<Checkqtb> Checkqtb { get; set; }
        public virtual DbSet<Darkhasttb> Darkhasttb { get; set; }
        public virtual DbSet<Dchangeanbartb> Dchangeanbartb { get; set; }
        public virtual DbSet<Defcodetb> Defcodetb { get; set; }
        public virtual DbSet<Dtolidtb> Dtolidtb { get; set; }
        public virtual DbSet<Factkalatb> Factkalatb { get; set; }
        public virtual DbSet<Facttb> Facttb { get; set; }
        public virtual DbSet<Filetb> Filetb { get; set; }
        public virtual DbSet<Gorohkalatb> Gorohkalatb { get; set; }
        public virtual DbSet<Gorohpersontb> Gorohpersontb { get; set; }
        public virtual DbSet<Gorohtb> Gorohtb { get; set; }
        public virtual DbSet<Hesabusertb> Hesabusertb { get; set; }
        public virtual DbSet<Kalatb> Kalatb { get; set; }
        public virtual DbSet<Koltb> Koltb { get; set; }
        public virtual DbSet<Lookuptb> Lookuptb { get; set; }
        public virtual DbSet<Looupsubtb> Looupsubtb { get; set; }
        public virtual DbSet<Masterchack> Masterchack { get; set; }
        public virtual DbSet<Mastermenutb> Mastermenutb { get; set; }
        public virtual DbSet<Mastersanadtb> Mastersanadtb { get; set; }
        public virtual DbSet<Mastertolidtb> Mastertolidtb { get; set; }
        public virtual DbSet<Mchangeanbartb> Mchangeanbartb { get; set; }
        public virtual DbSet<Menutb> Menutb { get; set; }
        public virtual DbSet<Mointb> Mointb { get; set; }
        public virtual DbSet<Otherhesabtb> Otherhesabtb { get; set; }
        public virtual DbSet<Packtb> Packtb { get; set; }
        public virtual DbSet<PanelTb> PanelTb { get; set; }
        public virtual DbSet<Pardartb> Pardartb { get; set; }
        public virtual DbSet<Persontb> Persontb { get; set; }
        public virtual DbSet<Pertb> Pertb { get; set; }
        public virtual DbSet<Pishfacttb> Pishfacttb { get; set; }
        public virtual DbSet<Rlate2kalatb> Rlate2kalatb { get; set; }
        public virtual DbSet<Rlatekalatb> Rlatekalatb { get; set; }
        public virtual DbSet<RlateMstb> RlateMstb { get; set; }
        public virtual DbSet<Sanadtb> Sanadtb { get; set; }
        public virtual DbSet<Setuptb> Setuptb { get; set; }
        public virtual DbSet<Shortcuttb> Shortcuttb { get; set; }
        public virtual DbSet<Submointb> Submointb { get; set; }
        public virtual DbSet<Subpacktb> Subpacktb { get; set; }
        public virtual DbSet<Tintb> Tintb { get; set; }
        public virtual DbSet<Tmppersontb> Tmppersontb { get; set; }
        public virtual DbSet<Usertb> Usertb { get; set; }

        // Unable to generate entity type for table 'dbo.logfacttb'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.logfactkalatb'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=178.32.117.242\\sql2016;Initial Catalog=accfiyan_data;User ID=sa;Password=Mm55478816_;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Anbartb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("anbartb");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Moinid).HasColumnName("moinid");

                entity.Property(e => e.Namec)
                    .IsRequired()
                    .HasColumnName("namec")
                    .HasMaxLength(50);

                entity.Property(e => e.Tafid).HasColumnName("tafid");

                entity.HasOne(d => d.Moin)
                    .WithMany(p => p.Anbartb)
                    .HasForeignKey(d => d.Moinid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_anbartb_mointb");

                entity.HasOne(d => d.Taf)
                    .WithMany(p => p.Anbartb)
                    .HasForeignKey(d => d.Tafid)
                    .HasConstraintName("FK_anbartb_submointb");
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Firstname).HasMaxLength(50);

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Isactive).HasColumnName("isactive");

                entity.Property(e => e.Lastname).HasMaxLength(50);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_AspNetUsers_anbartb");
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Banktb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("banktb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Moinid).HasColumnName("moinid");

                entity.Property(e => e.Nohesab)
                    .HasColumnName("nohesab")
                    .HasMaxLength(20);

                entity.Property(e => e.Nokart)
                    .HasColumnName("nokart")
                    .HasMaxLength(20);

                entity.Property(e => e.Nosheba)
                    .HasColumnName("nosheba")
                    .HasMaxLength(50);

                entity.Property(e => e.Tafid).HasColumnName("tafid");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(30);

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.HasOne(d => d.Moin)
                    .WithMany(p => p.Banktb)
                    .HasForeignKey(d => d.Moinid)
                    .HasConstraintName("FK_banktb_mointb");

                entity.HasOne(d => d.Taf)
                    .WithMany(p => p.Banktb)
                    .HasForeignKey(d => d.Tafid)
                    .HasConstraintName("FK_banktb_submointb");
            });

            modelBuilder.Entity<Checkqtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("checkqtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Mcheckq).HasColumnName("mcheckq");

                entity.Property(e => e.Nobarg).HasColumnName("nobarg");

                entity.Property(e => e.Statec).HasColumnName("statec");

                entity.HasOne(d => d.McheckqNavigation)
                    .WithMany(p => p.Checkqtb)
                    .HasForeignKey(d => d.Mcheckq)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_checkqtb_masterchack");
            });

            modelBuilder.Entity<Darkhasttb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("darkhasttb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Datec)
                    .HasColumnName("datec")
                    .HasMaxLength(10)
                    .HasDefaultValueSql("([dbo].[getShamsiDate](getdate()))");

                entity.Property(e => e.Kalaid).HasColumnName("kalaid");

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.HasOne(d => d.Kala)
                    .WithMany(p => p.Darkhasttb)
                    .HasForeignKey(d => d.Kalaid)
                    .HasConstraintName("FK_darkhasttb_kalatb");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Darkhasttb)
                    .HasForeignKey(d => d.Personid)
                    .HasConstraintName("FK_darkhasttb_pertb");
            });

            modelBuilder.Entity<Dchangeanbartb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("dchangeanbartb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Fromanbar).HasColumnName("fromanbar");

                entity.Property(e => e.Idkala).HasColumnName("idkala");

                entity.Property(e => e.Masterid).HasColumnName("masterid");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Pricekol)
                    .HasColumnName("pricekol")
                    .HasComputedColumnSql("(isnull([price],(0))*isnull([tedad],(0)))");

                entity.Property(e => e.Tedad).HasColumnName("tedad");

                entity.Property(e => e.Tedadd).HasColumnName("tedadd");

                entity.Property(e => e.Toanbar).HasColumnName("toanbar");

                entity.HasOne(d => d.FromanbarNavigation)
                    .WithMany(p => p.DchangeanbartbFromanbarNavigation)
                    .HasForeignKey(d => d.Fromanbar)
                    .HasConstraintName("FK_dchangeanbartb_anbartb");

                entity.HasOne(d => d.IdkalaNavigation)
                    .WithMany(p => p.Dchangeanbartb)
                    .HasForeignKey(d => d.Idkala)
                    .HasConstraintName("FK_dchangeanbartb_kalatb");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.Dchangeanbartb)
                    .HasForeignKey(d => d.Masterid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_dchangeanbartb_mchangeanbartb");

                entity.HasOne(d => d.ToanbarNavigation)
                    .WithMany(p => p.DchangeanbartbToanbarNavigation)
                    .HasForeignKey(d => d.Toanbar)
                    .HasConstraintName("FK_dchangeanbartb_anbartb1");
            });

            modelBuilder.Entity<Defcodetb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("defcodetb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Idanbarrikhtegar).HasColumnName("idanbarrikhtegar");

                entity.Property(e => e.Idanbarshakhezan).HasColumnName("idanbarshakhezan");

                entity.Property(e => e.Idkalashemsh).HasColumnName("idkalashemsh");

                entity.Property(e => e.Idmoinef).HasColumnName("idmoinef");

                entity.Property(e => e.Idmoinnoghre).HasColumnName("idmoinnoghre");

                entity.Property(e => e.Idneginsajme).HasColumnName("idneginsajme");

                entity.Property(e => e.Idtafef).HasColumnName("idtafef");

                entity.Property(e => e.Idtafnoghre).HasColumnName("idtafnoghre");

                entity.Property(e => e.Moinidforosh).HasColumnName("moinidforosh");

                entity.Property(e => e.Moinidmiyan).HasColumnName("moinidmiyan");

                entity.Property(e => e.Moinidtakh).HasColumnName("moinidtakh");

                entity.Property(e => e.Moinidtakhf).HasColumnName("moinidtakhf");

                entity.Property(e => e.Moinpor).HasColumnName("moinpor");

                entity.Property(e => e.Mointolid).HasColumnName("mointolid");

                entity.Property(e => e.Tafidforosh).HasColumnName("tafidforosh");

                entity.Property(e => e.Tafidmiyan).HasColumnName("tafidmiyan");

                entity.Property(e => e.Tafidtakh).HasColumnName("tafidtakh");

                entity.Property(e => e.Tafidtakhf).HasColumnName("tafidtakhf");

                entity.Property(e => e.Tafpor).HasColumnName("tafpor");
            });

            modelBuilder.Entity<Dtolidtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("dtolidtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Idkala).HasColumnName("idkala");

                entity.Property(e => e.Masterid).HasColumnName("masterid");

                entity.Property(e => e.Noted)
                    .HasColumnName("noted")
                    .HasMaxLength(50);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)")
                    .HasComputedColumnSql("(CONVERT([numeric](18,0),[dbo].[getmiyangin]([idkala],[masterid],(1))*[tedad]))");

                entity.Property(e => e.Tedad).HasColumnName("tedad");

                entity.Property(e => e.Tedadd).HasColumnName("tedadd");

                entity.Property(e => e.Typeexit).HasColumnName("typeexit");

                entity.Property(e => e.Vazn).HasColumnName("vazn");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Dtolidtb)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_dtolidtb_anbartb");

                entity.HasOne(d => d.IdkalaNavigation)
                    .WithMany(p => p.Dtolidtb)
                    .HasForeignKey(d => d.Idkala)
                    .HasConstraintName("FK_dtolidtb_kalatb");

                entity.HasOne(d => d.Master)
                    .WithMany(p => p.Dtolidtb)
                    .HasForeignKey(d => d.Masterid)
                    .HasConstraintName("FK_dtolidtb_mastertolidtb");
            });

            modelBuilder.Entity<Factkalatb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("factkalatb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Act).HasColumnName("act");

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Idbazar).HasColumnName("idbazar");

                entity.Property(e => e.Iddchange).HasColumnName("iddchange");

                entity.Property(e => e.Iddtolid).HasColumnName("iddtolid");

                entity.Property(e => e.Idfact).HasColumnName("idfact");

                entity.Property(e => e.Idkala).HasColumnName("idkala");

                entity.Property(e => e.Isshow).HasColumnName("isshow");

                entity.Property(e => e.Maliyat).HasColumnName("maliyat");

                entity.Property(e => e.Namekala)
                    .HasColumnName("namekala")
                    .HasMaxLength(50);

                entity.Property(e => e.Namevahed)
                    .HasColumnName("namevahed")
                    .HasMaxLength(30);

                entity.Property(e => e.Notef)
                    .HasColumnName("notef")
                    .HasMaxLength(100);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Pricekhales)
                    .HasColumnName("pricekhales")
                    .HasComputedColumnSql("(case when isnull([tedadv],(0))>(0) then [price]*[tedadv]-isnull([takhfif],(0)) when isnull([tedaddar],(0))>(0) then [price]*[tedaddar]-isnull([takhfif],(0))  end)");

                entity.Property(e => e.Pricekharid)
                    .HasColumnName("pricekharid")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Pricekol)
                    .HasColumnName("pricekol")
                    .HasComputedColumnSql("(case when isnull([tedadv],(0))>(0) then [price]*[tedadv] when isnull([tedaddar],(0))>(0) then [price]*[tedaddar]  end)");

                entity.Property(e => e.Pricemiyan)
                    .HasColumnName("pricemiyan")
                    .HasColumnType("numeric(18, 0)")
                    .HasComputedColumnSql("(CONVERT([numeric](18,0),[dbo].[getmiyangin]([idkala],[idfact],(0))*[tedadv]))");

                entity.Property(e => e.Sahmbazar)
                    .HasColumnName("sahmbazar")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Takhfif)
                    .HasColumnName("takhfif")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Takhfifsh)
                    .HasColumnName("takhfifsh")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Tedaddar).HasColumnName("tedaddar");

                entity.Property(e => e.Tedadv).HasColumnName("tedadv");

                entity.Property(e => e.Tedadvahed1).HasColumnName("tedadvahed1");

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Factkalatb)
                    .HasForeignKey(d => d.Idanbar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factkalatb_anbartb");

                entity.HasOne(d => d.IdbazarNavigation)
                    .WithMany(p => p.Factkalatb)
                    .HasForeignKey(d => d.Idbazar)
                    .HasConstraintName("FK_factkalatb_persontb");

                entity.HasOne(d => d.IdfactNavigation)
                    .WithMany(p => p.Factkalatb)
                    .HasForeignKey(d => d.Idfact)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_factkalatb_facttb");

                entity.HasOne(d => d.IdkalaNavigation)
                    .WithMany(p => p.Factkalatb)
                    .HasForeignKey(d => d.Idkala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_factkalatb_kalatb");
            });

            modelBuilder.Entity<Facttb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("facttb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Act).HasColumnName("act");

                entity.Property(e => e.Darsadtakh)
                    .HasColumnName("darsadtakh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Datec)
                    .HasColumnName("datec")
                    .HasMaxLength(10);

                entity.Property(e => e.Dsahmb).HasColumnName("dsahmb");

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Idbank).HasColumnName("idbank");

                entity.Property(e => e.Idbazar).HasColumnName("idbazar");

                entity.Property(e => e.Idchange).HasColumnName("idchange");

                entity.Property(e => e.Idmoin).HasColumnName("idmoin");

                entity.Property(e => e.Idperson).HasColumnName("idperson");

                entity.Property(e => e.Idtaf).HasColumnName("idtaf");

                entity.Property(e => e.Idtolid).HasColumnName("idtolid");

                entity.Property(e => e.Ispublic).HasColumnName("ispublic");

                entity.Property(e => e.Istel).HasColumnName("istel");

                entity.Property(e => e.Nofact).HasColumnName("nofact");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Notetakh)
                    .HasColumnName("notetakh")
                    .HasMaxLength(100);

                entity.Property(e => e.Personkharid).HasColumnName("personkharid");

                entity.Property(e => e.Peyvast).HasColumnName("peyvast");

                entity.Property(e => e.Sahmb)
                    .HasColumnName("sahmb")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Takhfact)
                    .HasColumnName("takhfact")
                    .HasColumnType("numeric(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Tmpprice)
                    .HasColumnName("tmpprice")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.Property(e => e.Typesahmb).HasColumnName("typesahmb");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Facttb)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_facttb_anbartb");

                entity.HasOne(d => d.IdbankNavigation)
                    .WithMany(p => p.Facttb)
                    .HasForeignKey(d => d.Idbank)
                    .HasConstraintName("FK_facttb_banktb");

                entity.HasOne(d => d.IdbazarNavigation)
                    .WithMany(p => p.FacttbIdbazarNavigation)
                    .HasForeignKey(d => d.Idbazar)
                    .HasConstraintName("FK_facttb_persontb1");

                entity.HasOne(d => d.IdmoinNavigation)
                    .WithMany(p => p.Facttb)
                    .HasForeignKey(d => d.Idmoin)
                    .HasConstraintName("FK_facttb_mointb");

                entity.HasOne(d => d.IdpersonNavigation)
                    .WithMany(p => p.FacttbIdpersonNavigation)
                    .HasForeignKey(d => d.Idperson)
                    .HasConstraintName("FK_facttb_persontb");

                entity.HasOne(d => d.IdtafNavigation)
                    .WithMany(p => p.Facttb)
                    .HasForeignKey(d => d.Idtaf)
                    .HasConstraintName("FK_facttb_submointb");

                entity.HasOne(d => d.Personkhar)
                    .WithMany(p => p.FacttbPersonkhar)
                    .HasForeignKey(d => d.Personkharid)
                    .HasConstraintName("FK_facttb_persontb2");
            });

            modelBuilder.Entity<Filetb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("filetb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Gidkala).HasColumnName("gidkala");

                entity.Property(e => e.Idkala).HasColumnName("idkala");

                entity.Property(e => e.Lookid).HasColumnName("lookid");

                entity.Property(e => e.Namec)
                    .HasColumnName("namec")
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<Gorohkalatb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("gorohkalatb");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Namegoroh)
                    .HasColumnName("namegoroh")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Gorohpersontb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("gorohpersontb");

                entity.HasIndex(e => e.Moinid)
                    .HasName("IX_gorohpersontb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Moinid).HasColumnName("moinid");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Moin)
                    .WithMany(p => p.Gorohpersontb)
                    .HasForeignKey(d => d.Moinid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sandoghtb_mointb");
            });

            modelBuilder.Entity<Gorohtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Namec)
                    .HasColumnName("namec")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Hesabusertb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("hesabusertb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Hesabid).HasColumnName("hesabid");

                entity.Property(e => e.Isdef).HasColumnName("isdef");

                entity.Property(e => e.Typehesab).HasColumnName("typehesab");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Hesabusertb)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_hesabusertb_usertb");
            });

            modelBuilder.Entity<Kalatb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("kalatb");

                entity.HasIndex(e => e.Codekala)
                    .HasName("IX_kalatb")
                    .IsUnique();

                entity.HasIndex(e => e.Namekala)
                    .HasName("IX_kalatb_1")
                    .IsUnique();

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Addpic)
                    .HasColumnName("addpic")
                    .HasMaxLength(100);

                entity.Property(e => e.Barcode)
                    .HasColumnName("barcode")
                    .HasMaxLength(30);

                entity.Property(e => e.Codekala).HasColumnName("codekala");

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Idclass).HasColumnName("idclass");

                entity.Property(e => e.Idvahed1).HasColumnName("idvahed1");

                entity.Property(e => e.Idvahed2).HasColumnName("idvahed2");

                entity.Property(e => e.Isauto).HasColumnName("isauto");

                entity.Property(e => e.Ismanfi).HasColumnName("ismanfi");

                entity.Property(e => e.Methodc).HasColumnName("methodc");

                entity.Property(e => e.Namekala)
                    .IsRequired()
                    .HasColumnName("namekala")
                    .HasMaxLength(100);

                entity.Property(e => e.Price1)
                    .HasColumnName("price1")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Price2)
                    .HasColumnName("price2")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Titleprint).HasColumnName("titleprint");

                entity.Property(e => e.Vahed1).HasColumnName("vahed1");

                entity.Property(e => e.Vahed2).HasColumnName("vahed2");

                entity.HasOne(d => d.G)
                    .WithMany(p => p.Kalatb)
                    .HasForeignKey(d => d.Gid)
                    .HasConstraintName("FK_kalatb_PanelTb");

                entity.HasOne(d => d.IdclassNavigation)
                    .WithMany(p => p.KalatbIdclassNavigation)
                    .HasForeignKey(d => d.Idclass)
                    .HasConstraintName("FK_kalatb_looupsubtb3");

                entity.HasOne(d => d.Idvahed1Navigation)
                    .WithMany(p => p.KalatbIdvahed1Navigation)
                    .HasForeignKey(d => d.Idvahed1)
                    .HasConstraintName("FK_kalatb_looupsubtb");

                entity.HasOne(d => d.Idvahed2Navigation)
                    .WithMany(p => p.KalatbIdvahed2Navigation)
                    .HasForeignKey(d => d.Idvahed2)
                    .HasConstraintName("FK_kalatb_looupsubtb1");

                entity.HasOne(d => d.TitleprintNavigation)
                    .WithMany(p => p.KalatbTitleprintNavigation)
                    .HasForeignKey(d => d.Titleprint)
                    .HasConstraintName("FK_kalatb_looupsubtb2");
            });

            modelBuilder.Entity<Koltb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("koltb");

                entity.HasIndex(e => e.Codekol)
                    .HasName("IX_koltb")
                    .IsUnique();

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Codekol)
                    .HasColumnName("codekol")
                    .HasMaxLength(5);

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Kind).HasColumnName("kind");

                entity.Property(e => e.Namekol)
                    .HasColumnName("namekol")
                    .HasMaxLength(50);

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.HasOne(d => d.G)
                    .WithMany(p => p.Koltb)
                    .HasForeignKey(d => d.Gid)
                    .HasConstraintName("FK_koltb_Gorohtb");
            });

            modelBuilder.Entity<Lookuptb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("lookuptb");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Looupsubtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("looupsubtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Entitle)
                    .HasColumnName("entitle")
                    .HasMaxLength(50);

                entity.Property(e => e.Grp).HasColumnName("grp");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Valuec).HasColumnName("valuec");
            });

            modelBuilder.Entity<Masterchack>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("masterchack");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Bankid).HasColumnName("bankid");

                entity.Property(e => e.Frombarg).HasColumnName("frombarg");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Tobarg).HasColumnName("tobarg");

                entity.HasOne(d => d.Bank)
                    .WithMany(p => p.Masterchack)
                    .HasForeignKey(d => d.Bankid)
                    .HasConstraintName("FK_masterchack_banktb");
            });

            modelBuilder.Entity<Mastermenutb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("mastermenutb");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Act).HasColumnName("act");

                entity.Property(e => e.Enmenu)
                    .HasColumnName("enmenu")
                    .HasMaxLength(50);

                entity.Property(e => e.Icon)
                    .HasColumnName("icon")
                    .HasMaxLength(100);

                entity.Property(e => e.Mastersort).HasColumnName("mastersort");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Mastersanadtb>(entity =>
            {
                entity.HasKey(e => e.Nosanad);

                entity.ToTable("mastersanadtb");

                entity.HasIndex(e => e.Datec)
                    .HasName("IX_mastersanadtb");

                entity.Property(e => e.Nosanad)
                    .HasColumnName("nosanad")
                    .ValueGeneratedNever();

                entity.Property(e => e.Datec)
                    .IsRequired()
                    .HasColumnName("datec")
                    .HasMaxLength(10);

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Typec)
                    .HasColumnName("typec")
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Mastertolidtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("mastertolidtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Datec)
                    .HasColumnName("datec")
                    .HasMaxLength(10);

                entity.Property(e => e.Fanbar).HasColumnName("fanbar");

                entity.Property(e => e.Nofact).HasColumnName("nofact");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(100);

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Statec).HasColumnName("statec");

                entity.HasOne(d => d.FanbarNavigation)
                    .WithMany(p => p.Mastertolidtb)
                    .HasForeignKey(d => d.Fanbar)
                    .HasConstraintName("FK_mastertolidtb_anbartb");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Mastertolidtb)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_mastertolidtb_persontb");
            });

            modelBuilder.Entity<Mchangeanbartb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("mchangeanbartb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Datec)
                    .HasColumnName("datec")
                    .HasMaxLength(10);

                entity.Property(e => e.Namegirande)
                    .HasColumnName("namegirande")
                    .HasMaxLength(50);

                entity.Property(e => e.Nametahvil)
                    .HasColumnName("nametahvil")
                    .HasMaxLength(50);

                entity.Property(e => e.Nofact).HasColumnName("nofact");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(200);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(450);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Mchangeanbartb)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_mchangeanbartb_usertb");
            });

            modelBuilder.Entity<Menutb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("menutb");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Actionname)
                    .HasColumnName("actionname")
                    .HasMaxLength(50);

                entity.Property(e => e.Controller)
                    .HasColumnName("controller")
                    .HasMaxLength(50);

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.Property(e => e.Msort).HasColumnName("msort");

                entity.Property(e => e.Rout1)
                    .HasColumnName("rout1")
                    .HasMaxLength(50);

                entity.Property(e => e.Rout2)
                    .HasColumnName("rout2")
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Valrout1)
                    .HasColumnName("valrout1")
                    .HasMaxLength(50);

                entity.Property(e => e.Valrout2)
                    .HasColumnName("valrout2")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Menutb)
                    .HasForeignKey(d => d.Menuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_menutb_mastermenutb");
            });

            modelBuilder.Entity<Mointb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("mointb");

                entity.HasIndex(e => new { e.Kid, e.Codemoin })
                    .HasName("IX_mointb")
                    .IsUnique();

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Codemoin)
                    .HasColumnName("codemoin")
                    .HasMaxLength(5);

                entity.Property(e => e.Kid).HasColumnName("kid");

                entity.Property(e => e.Namemoin)
                    .HasColumnName("namemoin")
                    .HasMaxLength(50);

                entity.HasOne(d => d.K)
                    .WithMany(p => p.Mointb)
                    .HasForeignKey(d => d.Kid)
                    .HasConstraintName("FK_koltb_mointb");
            });

            modelBuilder.Entity<Otherhesabtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("otherhesabtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Idmoin).HasColumnName("idmoin");

                entity.Property(e => e.Idtaf).HasColumnName("idtaf");

                entity.Property(e => e.Namec)
                    .IsRequired()
                    .HasColumnName("namec")
                    .HasMaxLength(50);

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.HasOne(d => d.IdmoinNavigation)
                    .WithMany(p => p.Otherhesabtb)
                    .HasForeignKey(d => d.Idmoin)
                    .HasConstraintName("FK_otherhesabtb_mointb");

                entity.HasOne(d => d.IdtafNavigation)
                    .WithMany(p => p.Otherhesabtb)
                    .HasForeignKey(d => d.Idtaf)
                    .HasConstraintName("FK_otherhesabtb_submointb");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Otherhesabtb)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_otherhesabtb_persontb");
            });

            modelBuilder.Entity<Packtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("packtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Codepack)
                    .HasColumnName("codepack")
                    .HasMaxLength(30);

                entity.Property(e => e.Namepak)
                    .IsRequired()
                    .HasColumnName("namepak")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PanelTb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Codegroup).HasColumnName("codegroup");

                entity.Property(e => e.Codeparent).HasColumnName("codeparent");

                entity.Property(e => e.Css)
                    .HasColumnName("css")
                    .HasMaxLength(20);

                entity.Property(e => e.Fcode)
                    .HasColumnName("fcode")
                    .HasComputedColumnSql("([dbo].[getfpanel]([mid]))");

                entity.Property(e => e.Fullname)
                    .HasColumnName("fullname")
                    .HasComputedColumnSql("([dbo].[getnamepanel]([mid]))");

                entity.Property(e => e.Harf)
                    .HasColumnName("harf")
                    .HasMaxLength(1);

                entity.Property(e => e.Namep)
                    .IsRequired()
                    .HasColumnName("namep")
                    .HasMaxLength(50);

                entity.Property(e => e.Sortc).HasColumnName("sortc");

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.HasOne(d => d.CodeparentNavigation)
                    .WithMany(p => p.InverseCodeparentNavigation)
                    .HasForeignKey(d => d.Codeparent)
                    .HasConstraintName("FK_PanelTb_PanelTb");
            });

            modelBuilder.Entity<Pardartb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("pardartb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Bankdar)
                    .HasColumnName("bankdar")
                    .HasMaxLength(50);

                entity.Property(e => e.Datec)
                    .IsRequired()
                    .HasColumnName("datec")
                    .HasMaxLength(10);

                entity.Property(e => e.Datecheck)
                    .HasColumnName("datecheck")
                    .HasMaxLength(10);

                entity.Property(e => e.Datestatecheckq)
                    .HasColumnName("datestatecheckq")
                    .HasMaxLength(10);

                entity.Property(e => e.Hesabdar)
                    .HasColumnName("hesabdar")
                    .HasMaxLength(50);

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Idfact).HasColumnName("idfact");

                entity.Property(e => e.Idmoinv).HasColumnName("idmoinv");

                entity.Property(e => e.Idtafv).HasColumnName("idtafv");

                entity.Property(e => e.Mablagh)
                    .HasColumnName("mablagh")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Moinbed).HasColumnName("moinbed");

                entity.Property(e => e.Moinbes).HasColumnName("moinbes");

                entity.Property(e => e.Nocheckq)
                    .HasColumnName("nocheckq")
                    .HasMaxLength(20);

                entity.Property(e => e.Noghabz)
                    .HasColumnName("noghabz")
                    .HasMaxLength(20);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(200);

                entity.Property(e => e.Pidbed).HasColumnName("pidbed");

                entity.Property(e => e.Pidbes).HasColumnName("pidbes");

                entity.Property(e => e.Pidv).HasColumnName("pidv");

                entity.Property(e => e.Sanbed).HasColumnName("sanbed");

                entity.Property(e => e.Sanbes).HasColumnName("sanbes");

                entity.Property(e => e.Statecheckq).HasColumnName("statecheckq");

                entity.Property(e => e.Tafbed).HasColumnName("tafbed");

                entity.Property(e => e.Tafbes).HasColumnName("tafbes");

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(450);

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Pardartb)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_pardartb_anbartb");

                entity.HasOne(d => d.IdfactNavigation)
                    .WithMany(p => p.Pardartb)
                    .HasForeignKey(d => d.Idfact)
                    .HasConstraintName("FK_pardartb_facttb");

                entity.HasOne(d => d.IdmoinvNavigation)
                    .WithMany(p => p.PardartbIdmoinvNavigation)
                    .HasForeignKey(d => d.Idmoinv)
                    .HasConstraintName("FK_pardartb_mointb2");

                entity.HasOne(d => d.IdtafvNavigation)
                    .WithMany(p => p.PardartbIdtafvNavigation)
                    .HasForeignKey(d => d.Idtafv)
                    .HasConstraintName("FK_pardartb_submointb2");

                entity.HasOne(d => d.MoinbedNavigation)
                    .WithMany(p => p.PardartbMoinbedNavigation)
                    .HasForeignKey(d => d.Moinbed)
                    .HasConstraintName("FK_pardartb_mointb1");

                entity.HasOne(d => d.MoinbesNavigation)
                    .WithMany(p => p.PardartbMoinbesNavigation)
                    .HasForeignKey(d => d.Moinbes)
                    .HasConstraintName("FK_pardartb_mointb");

                entity.HasOne(d => d.PidbedNavigation)
                    .WithMany(p => p.PardartbPidbedNavigation)
                    .HasForeignKey(d => d.Pidbed)
                    .HasConstraintName("FK_pardartb_persontb");

                entity.HasOne(d => d.PidbesNavigation)
                    .WithMany(p => p.PardartbPidbesNavigation)
                    .HasForeignKey(d => d.Pidbes)
                    .HasConstraintName("FK_pardartb_persontb1");

                entity.HasOne(d => d.PidvNavigation)
                    .WithMany(p => p.PardartbPidvNavigation)
                    .HasForeignKey(d => d.Pidv)
                    .HasConstraintName("FK_pardartb_persontb2");

                entity.HasOne(d => d.SanbedNavigation)
                    .WithMany(p => p.PardartbSanbedNavigation)
                    .HasForeignKey(d => d.Sanbed)
                    .HasConstraintName("FK_pardartb_banktb");

                entity.HasOne(d => d.SanbesNavigation)
                    .WithMany(p => p.PardartbSanbesNavigation)
                    .HasForeignKey(d => d.Sanbes)
                    .HasConstraintName("FK_pardartb_banktb1");

                entity.HasOne(d => d.TafbedNavigation)
                    .WithMany(p => p.PardartbTafbedNavigation)
                    .HasForeignKey(d => d.Tafbed)
                    .HasConstraintName("FK_pardartb_submointb");

                entity.HasOne(d => d.TafbesNavigation)
                    .WithMany(p => p.PardartbTafbesNavigation)
                    .HasForeignKey(d => d.Tafbes)
                    .HasConstraintName("FK_pardartb_submointb1");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pardartb)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pardartb_AspNetUsers");
            });

            modelBuilder.Entity<Persontb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("persontb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Addressc)
                    .HasColumnName("addressc")
                    .HasMaxLength(200);

                entity.Property(e => e.Bimarid).HasColumnName("bimarid");

                entity.Property(e => e.Codegh)
                    .HasColumnName("codegh")
                    .HasMaxLength(20);

                entity.Property(e => e.Codemeli)
                    .HasColumnName("codemeli")
                    .HasMaxLength(10);

                entity.Property(e => e.Datetavalod)
                    .HasColumnName("datetavalod")
                    .HasMaxLength(10);

                entity.Property(e => e.Family)
                    .HasColumnName("family")
                    .HasMaxLength(50);

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Jensiat).HasColumnName("jensiat");

                entity.Property(e => e.Laghab)
                    .HasColumnName("laghab")
                    .HasMaxLength(20);

                entity.Property(e => e.Mob2)
                    .HasColumnName("mob2")
                    .HasMaxLength(50);

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15);

                entity.Property(e => e.Namec)
                    .HasColumnName("namec")
                    .HasMaxLength(50);

                entity.Property(e => e.Namekamel)
                    .IsRequired()
                    .HasColumnName("namekamel")
                    .HasMaxLength(122)
                    .HasComputedColumnSql("((isnull([laghab],' ')+' ')+((isnull([namec],'')+' ')+isnull([family],'')))");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(200);

                entity.Property(e => e.Statec).HasColumnName("statec");

                entity.Property(e => e.Tel)
                    .HasColumnName("tel")
                    .HasMaxLength(50);

                entity.Property(e => e.Typeperson).HasColumnName("typeperson");

                entity.HasOne(d => d.G)
                    .WithMany(p => p.Persontb)
                    .HasForeignKey(d => d.Gid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_persontb_gorohpersontb");
            });

            modelBuilder.Entity<Pertb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("pertb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Bank).HasColumnName("bank");

                entity.Property(e => e.Gkalaid).HasColumnName("gkalaid");

                entity.Property(e => e.Gperson).HasColumnName("gperson");

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(450);

                entity.HasOne(d => d.BankNavigation)
                    .WithMany(p => p.Pertb)
                    .HasForeignKey(d => d.Bank)
                    .HasConstraintName("FK_pertb_banktb");

                entity.HasOne(d => d.Gkala)
                    .WithMany(p => p.Pertb)
                    .HasForeignKey(d => d.Gkalaid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_pertb_PanelTb");

                entity.HasOne(d => d.GpersonNavigation)
                    .WithMany(p => p.Pertb)
                    .HasForeignKey(d => d.Gperson)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_pertb_gorohpersontb");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Pertb)
                    .HasForeignKey(d => d.Idanbar)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_pertb_anbartb");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Pertb)
                    .HasForeignKey(d => d.Menuid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_pertb_menutb");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Pertb)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_pertb_pertb");
            });

            modelBuilder.Entity<Pishfacttb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("pishfacttb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Act).HasColumnName("act");

                entity.Property(e => e.Darsadtakh)
                    .HasColumnName("darsadtakh")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Datec)
                    .HasColumnName("datec")
                    .HasMaxLength(10);

                entity.Property(e => e.Dsahmb).HasColumnName("dsahmb");

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Idbank).HasColumnName("idbank");

                entity.Property(e => e.Idbazar).HasColumnName("idbazar");

                entity.Property(e => e.Idchange).HasColumnName("idchange");

                entity.Property(e => e.Idmoin).HasColumnName("idmoin");

                entity.Property(e => e.Idperson).HasColumnName("idperson");

                entity.Property(e => e.Idtaf).HasColumnName("idtaf");

                entity.Property(e => e.Idtolid).HasColumnName("idtolid");

                entity.Property(e => e.Ispublic).HasColumnName("ispublic");

                entity.Property(e => e.Istel).HasColumnName("istel");

                entity.Property(e => e.Nofact).HasColumnName("nofact");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.Property(e => e.Notetakh)
                    .HasColumnName("notetakh")
                    .HasMaxLength(100);

                entity.Property(e => e.Peyvast).HasColumnName("peyvast");

                entity.Property(e => e.Sahmb)
                    .HasColumnName("sahmb")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Takhfact)
                    .HasColumnName("takhfact")
                    .HasColumnType("numeric(18, 0)")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Title)
                    .HasColumnName("title")
                    .HasMaxLength(50);

                entity.Property(e => e.Tmpprice)
                    .HasColumnName("tmpprice")
                    .HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Typec).HasColumnName("typec");

                entity.Property(e => e.Typesahmb).HasColumnName("typesahmb");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Pishfacttb)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_pishfacttb_anbartb");

                entity.HasOne(d => d.IdbankNavigation)
                    .WithMany(p => p.Pishfacttb)
                    .HasForeignKey(d => d.Idbank)
                    .HasConstraintName("FK_pishfacttb_banktb");

                entity.HasOne(d => d.IdbazarNavigation)
                    .WithMany(p => p.PishfacttbIdbazarNavigation)
                    .HasForeignKey(d => d.Idbazar)
                    .HasConstraintName("FK_pishfacttb_persontb1");

                entity.HasOne(d => d.IdmoinNavigation)
                    .WithMany(p => p.Pishfacttb)
                    .HasForeignKey(d => d.Idmoin)
                    .HasConstraintName("FK_pishfacttb_mointb");

                entity.HasOne(d => d.IdpersonNavigation)
                    .WithMany(p => p.PishfacttbIdpersonNavigation)
                    .HasForeignKey(d => d.Idperson)
                    .HasConstraintName("FK_pishfacttb_persontb");

                entity.HasOne(d => d.IdtafNavigation)
                    .WithMany(p => p.Pishfacttb)
                    .HasForeignKey(d => d.Idtaf)
                    .HasConstraintName("FK_pishfacttb_submointb");
            });

            modelBuilder.Entity<Rlate2kalatb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("rlate2kalatb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Idkalakol).HasColumnName("idkalakol");

                entity.Property(e => e.Idkalar).HasColumnName("idkalar");

                entity.Property(e => e.Tedad).HasColumnName("tedad");

                entity.Property(e => e.Tedadd).HasColumnName("tedadd");

                entity.HasOne(d => d.IdkalakolNavigation)
                    .WithMany(p => p.Rlate2kalatbIdkalakolNavigation)
                    .HasForeignKey(d => d.Idkalakol)
                    .HasConstraintName("FK_rlate2kalatb_kalatb1");

                entity.HasOne(d => d.IdkalarNavigation)
                    .WithMany(p => p.Rlate2kalatbIdkalarNavigation)
                    .HasForeignKey(d => d.Idkalar)
                    .HasConstraintName("FK_rlate2kalatb_kalatb");
            });

            modelBuilder.Entity<Rlatekalatb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("rlatekalatb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Kalaid).HasColumnName("kalaid");

                entity.HasOne(d => d.G)
                    .WithMany(p => p.Rlatekalatb)
                    .HasForeignKey(d => d.Gid)
                    .HasConstraintName("FK_rlatekalatb_gorohkalatb");

                entity.HasOne(d => d.Kala)
                    .WithMany(p => p.Rlatekalatb)
                    .HasForeignKey(d => d.Kalaid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_rlatekalatb_kalatb");
            });

            modelBuilder.Entity<RlateMstb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("rlateMStb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Moinid).HasColumnName("moinid");

                entity.Property(e => e.Tafid).HasColumnName("tafid");

                entity.HasOne(d => d.Moin)
                    .WithMany(p => p.RlateMstb)
                    .HasForeignKey(d => d.Moinid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_rlateMStb_mointb");

                entity.HasOne(d => d.Taf)
                    .WithMany(p => p.RlateMstb)
                    .HasForeignKey(d => d.Tafid)
                    .HasConstraintName("FK_rlateMStb_submointb");
            });

            modelBuilder.Entity<Sanadtb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("sanadtb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Bed)
                    .HasColumnName("bed")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Bes)
                    .HasColumnName("bes")
                    .HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Datemiladi)
                    .HasColumnName("datemiladi")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Factid).HasColumnName("factid");

                entity.Property(e => e.Factpid).HasColumnName("factpid");

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Idchange).HasColumnName("idchange");

                entity.Property(e => e.Isedit).HasColumnName("isedit");

                entity.Property(e => e.Moinid).HasColumnName("moinid");

                entity.Property(e => e.Nofish)
                    .HasColumnName("nofish")
                    .HasMaxLength(10);

                entity.Property(e => e.Nosanad).HasColumnName("nosanad");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(200);

                entity.Property(e => e.Pardarid).HasColumnName("pardarid");

                entity.Property(e => e.Pid).HasColumnName("pid");

                entity.Property(e => e.Sbimarid).HasColumnName("sbimarid");

                entity.Property(e => e.Tafid).HasColumnName("tafid");

                entity.Property(e => e.Userid)
                    .IsRequired()
                    .HasColumnName("userid")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Fact)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Factid)
                    .HasConstraintName("FK_sanadtb_facttb");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_sanadtb_anbartb1");

                entity.HasOne(d => d.Moin)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Moinid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sanadtb_mointb");

                entity.HasOne(d => d.NosanadNavigation)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Nosanad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sanadtb_mastersanadtb");

                entity.HasOne(d => d.Pardar)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Pardarid)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_sanadtb_pardartb");

                entity.HasOne(d => d.P)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Pid)
                    .HasConstraintName("FK_sanadtb_persontb");

                entity.HasOne(d => d.Taf)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Tafid)
                    .HasConstraintName("FK_sanadtb_submointb");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Sanadtb)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sanadtb_AspNetUsers");
            });

            modelBuilder.Entity<Setuptb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("setuptb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Addlogo)
                    .HasColumnName("addlogo")
                    .HasMaxLength(200);

                entity.Property(e => e.Bfforosh).HasColumnName("bfforosh");

                entity.Property(e => e.Fforosh).HasColumnName("fforosh");

                entity.Property(e => e.Fkharid).HasColumnName("fkharid");

                entity.Property(e => e.Fsal)
                    .HasColumnName("fsal")
                    .HasMaxLength(10);

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Moinidforosh).HasColumnName("moinidforosh");

                entity.Property(e => e.Moinidmiyan).HasColumnName("moinidmiyan");

                entity.Property(e => e.Moinidtakh).HasColumnName("moinidtakh");

                entity.Property(e => e.Moinidtakhf).HasColumnName("moinidtakhf");

                entity.Property(e => e.Pfforosh).HasColumnName("pfforosh");

                entity.Property(e => e.Sarbarg)
                    .HasColumnName("sarbarg")
                    .HasMaxLength(100);

                entity.Property(e => e.Sfforosh).HasColumnName("sfforosh");

                entity.Property(e => e.Sfkharid).HasColumnName("sfkharid");

                entity.Property(e => e.Tafidforosh).HasColumnName("tafidforosh");

                entity.Property(e => e.Tafidmiyan).HasColumnName("tafidmiyan");

                entity.Property(e => e.Tafidtakh).HasColumnName("tafidtakh");

                entity.Property(e => e.Tafidtakhf).HasColumnName("tafidtakhf");

                entity.Property(e => e.Tsal)
                    .HasColumnName("tsal")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Shortcuttb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("shortcuttb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.Property(e => e.Msort).HasColumnName("msort");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasMaxLength(450);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Shortcuttb)
                    .HasForeignKey(d => d.Menuid)
                    .HasConstraintName("FK_shortcuttb_menutb");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Shortcuttb)
                    .HasForeignKey(d => d.Userid)
                    .HasConstraintName("FK_shortcuttb_usertb");
            });

            modelBuilder.Entity<Submointb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("submointb");

                entity.HasIndex(e => e.Codetaf)
                    .HasName("IX_submointb")
                    .IsUnique();

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Codetaf)
                    .HasColumnName("codetaf")
                    .HasMaxLength(5);

                entity.Property(e => e.Nametaf)
                    .HasColumnName("nametaf")
                    .HasMaxLength(30);

                entity.Property(e => e.Pid).HasColumnName("pid");
            });

            modelBuilder.Entity<Subpacktb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("subpacktb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Idkala).HasColumnName("idkala");

                entity.Property(e => e.Packid).HasColumnName("packid");

                entity.HasOne(d => d.IdkalaNavigation)
                    .WithMany(p => p.Subpacktb)
                    .HasForeignKey(d => d.Idkala)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_subpacktb_kalatb");

                entity.HasOne(d => d.Pack)
                    .WithMany(p => p.Subpacktb)
                    .HasForeignKey(d => d.Packid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_subpacktb_packtb");
            });

            modelBuilder.Entity<Tintb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("tintb");

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<Tmppersontb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("tmppersontb");

                entity.Property(e => e.Mid).HasColumnName("mid");

                entity.Property(e => e.Addressc)
                    .HasColumnName("addressc")
                    .HasMaxLength(200);

                entity.Property(e => e.Bimarid).HasColumnName("bimarid");

                entity.Property(e => e.Codegh)
                    .HasColumnName("codegh")
                    .HasMaxLength(20);

                entity.Property(e => e.Datetavalod)
                    .HasColumnName("datetavalod")
                    .HasMaxLength(10);

                entity.Property(e => e.Family)
                    .HasColumnName("family")
                    .HasMaxLength(50);

                entity.Property(e => e.Gid).HasColumnName("gid");

                entity.Property(e => e.Jensiat).HasColumnName("jensiat");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15);

                entity.Property(e => e.Namec)
                    .IsRequired()
                    .HasColumnName("namec")
                    .HasMaxLength(50);

                entity.Property(e => e.Namekamel)
                    .IsRequired()
                    .HasColumnName("namekamel")
                    .HasMaxLength(101)
                    .HasComputedColumnSql("((isnull([namec],'')+' ')+isnull([family],''))");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(200);

                entity.Property(e => e.Statec).HasColumnName("statec");

                entity.Property(e => e.Typeperson).HasColumnName("typeperson");
            });

            modelBuilder.Entity<Usertb>(entity =>
            {
                entity.HasKey(e => e.Mid);

                entity.ToTable("usertb");

                entity.HasIndex(e => e.Namec)
                    .HasName("IX_usertb")
                    .IsUnique();

                entity.Property(e => e.Mid)
                    .HasColumnName("mid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Idanbar).HasColumnName("idanbar");

                entity.Property(e => e.Mobile)
                    .HasColumnName("mobile")
                    .HasMaxLength(15);

                entity.Property(e => e.Namec)
                    .IsRequired()
                    .HasColumnName("namec")
                    .HasMaxLength(20);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(20);

                entity.Property(e => e.Pass2)
                    .HasColumnName("pass2")
                    .HasMaxLength(10);

                entity.Property(e => e.Timec)
                    .HasColumnName("timec")
                    .HasColumnType("time(0)");

                entity.Property(e => e.Typec)
                    .HasColumnName("typec")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.IdanbarNavigation)
                    .WithMany(p => p.Usertb)
                    .HasForeignKey(d => d.Idanbar)
                    .HasConstraintName("FK_usertb_anbartb");
            });
        }
    }
}
