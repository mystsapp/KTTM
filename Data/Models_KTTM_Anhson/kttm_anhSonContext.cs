using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_KTTM_Anhson
{
    public partial class kttm_anhSonContext : DbContext
    {
        public kttm_anhSonContext()
        {
        }

        public kttm_anhSonContext(DbContextOptions<kttm_anhSonContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Kvctptc> Kvctptcs { get; set; }
        public virtual DbSet<Kvptc> Kvptcs { get; set; }
        public virtual DbSet<Tamung> Tamungs { get; set; }
        public virtual DbSet<Tonquy> Tonquies { get; set; }
        public virtual DbSet<Tt621> Tt621s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=dell\\sql2017;database=kttm_anhSon;Trusted_Connection=true;User Id=sa;Password=123456;Integrated security=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Kvctptc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("kvctptc");

                entity.Property(e => e.Bophan)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bophan");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("cardnumber");

                entity.Property(e => e.Coquay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("coquay");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("diachi");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Diengiaip)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("diengiaip");

                entity.Property(e => e.Dieuchinh).HasColumnName("dieuchinh");

                entity.Property(e => e.Dskhongvat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("dskhongvat");

                entity.Property(e => e.Hoadondt)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("hoadondt");

                entity.Property(e => e.Httc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Httt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Kc141)
                    .HasColumnType("datetime")
                    .HasColumnName("kc141");

                entity.Property(e => e.Khoanmuc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("khoanmuc");

                entity.Property(e => e.Kyhieu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kyhieu");

                entity.Property(e => e.Loaihdgoc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loaihdgoc");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Makh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Makhco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("makhco");

                entity.Property(e => e.Makhno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("makhno");

                entity.Property(e => e.Mathang)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("mathang");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayctgoc)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayctgoc");

                entity.Property(e => e.Noquay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("noquay");

                entity.Property(e => e.Salesslip)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("salesslip");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Soct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soct");

                entity.Property(e => e.Soctgoc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soctgoc");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Soxe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soxe");

                entity.Property(e => e.Stt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tamung)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tamung");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("tygia");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("vat");
            });

            modelBuilder.Entity<Kvptc>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("kvptc");

                entity.Property(e => e.Create)
                    .HasColumnType("datetime")
                    .HasColumnName("create");

                entity.Property(e => e.Donvi)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("donvi");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("hoten");

                entity.Property(e => e.Lapphieu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lapphieu");

                entity.Property(e => e.Lock)
                    .HasColumnType("datetime")
                    .HasColumnName("lock");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("locker");

                entity.Property(e => e.Maytinh)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maytinh");

                entity.Property(e => e.Mfieu)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("mfieu");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngoaite)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ngoaite");

                entity.Property(e => e.Phong)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phong");

                entity.Property(e => e.Soct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soct");
            });

            modelBuilder.Entity<Tamung>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tamung");

                entity.Property(e => e.Conlai)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("conlai");

                entity.Property(e => e.Conlaint)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("conlaint");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Makhno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("makhno");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Phieuchi)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phieuchi");

                entity.Property(e => e.Phieutt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("phieutt");

                entity.Property(e => e.Phong)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phong");

                entity.Property(e => e.Soct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soct");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tttp).HasColumnName("tttp");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("tygia");
            });

            modelBuilder.Entity<Tonquy>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tonquy");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("tygia");
            });

            modelBuilder.Entity<Tt621>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tt621");

                entity.Property(e => e.Bophan)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bophan");

                entity.Property(e => e.Coquay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("coquay");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("diachi");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Diengiaip)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("diengiaip");

                entity.Property(e => e.Dieuchinh).HasColumnName("dieuchinh");

                entity.Property(e => e.Dskhongvat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("dskhongvat");

                entity.Property(e => e.Ghiso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ghiso");

                entity.Property(e => e.Hoadondt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("hoadondt");

                entity.Property(e => e.Httc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Kyhieu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kyhieu");

                entity.Property(e => e.Kyhieuhd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("kyhieuhd");

                entity.Property(e => e.Lapphieu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lapphieu");

                entity.Property(e => e.Loaihdgoc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loaihdgoc");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Makhco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("makhco");

                entity.Property(e => e.Makhno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("makhno");

                entity.Property(e => e.Mathang)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("mathang");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayctgoc)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayctgoc");

                entity.Property(e => e.Noquay)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("noquay");

                entity.Property(e => e.Phieutc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phieutc");

                entity.Property(e => e.Phieutu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phieutu");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Soct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soct");

                entity.Property(e => e.Soctgoc)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soctgoc");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Soxe)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("soxe");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("tygia");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("vat");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
