using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_KTTM_1
{
    public partial class KTTM_1Context : DbContext
    {
        public KTTM_1Context()
        {
        }

        public KTTM_1Context(DbContextOptions<KTTM_1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Kvctptc> Kvctptcs { get; set; }
        public virtual DbSet<Kvptc> Kvptcs { get; set; }
        public virtual DbSet<TamUng> TamUngs { get; set; }
        public virtual DbSet<TonQuy> TonQuies { get; set; }
        public virtual DbSet<Tt621> Tt621s { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=Dell\\sql2017;Database=KTTM_1;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Kvctptc>(entity =>
            {
                entity.ToTable("KVCTPTCs");

                entity.HasIndex(e => e.Kvptcid, "IX_KVCTPTCs_KVPTCId");

                entity.Property(e => e.BoPhan)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.CardNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CoQuay)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.DienGiai).HasMaxLength(150);

                entity.Property(e => e.DienGiaiP).HasMaxLength(150);

                entity.Property(e => e.DskhongVat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("DSKhongVAT");

                entity.Property(e => e.HoaDonDt)
                    .HasMaxLength(120)
                    .HasColumnName("HoaDonDT");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("HTTC");

                entity.Property(e => e.Httt)
                    .HasMaxLength(12)
                    .HasColumnName("HTTT");

                entity.Property(e => e.Kc141)
                    .HasColumnType("datetime")
                    .HasColumnName("KC141");

                entity.Property(e => e.KhoangMuc)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Kvptcid)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("KVPTCId");

                entity.Property(e => e.KyHieu)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LinkHddt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("LinkHDDT");

                entity.Property(e => e.LoaiHdgoc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LoaiHDGoc");

                entity.Property(e => e.LoaiTien)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MaKh)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhCo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.MaTraCuu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MatHang).HasMaxLength(60);

                entity.Property(e => e.MauSoHd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("MauSoHD");

                entity.Property(e => e.MsThue)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCtgoc)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCTGoc");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoQuay)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SalesSlip).HasMaxLength(10);

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.SoCtgoc)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("SoCTGoc");

                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SoTienNt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SoTienNT");

                entity.Property(e => e.SoXe)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.Stt)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("STT");

                entity.Property(e => e.TamUng)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(100)
                    .HasColumnName("TenKH");

                entity.Property(e => e.TkTruyCap)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tkco)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKCo");

                entity.Property(e => e.Tkno)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKNo");

                entity.Property(e => e.TyGia).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");

                entity.HasOne(d => d.Kvptc)
                    .WithMany(p => p.Kvctptcs)
                    .HasForeignKey(d => d.Kvptcid);
            });

            modelBuilder.Entity<Kvptc>(entity =>
            {
                entity.HasKey(e => e.SoCt);

                entity.ToTable("KVPTCs");

                entity.Property(e => e.SoCt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoCT");

                entity.Property(e => e.Create).HasColumnType("datetime");

                entity.Property(e => e.DonVi).HasMaxLength(150);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.LapPhieu).HasMaxLength(50);

                entity.Property(e => e.Lock).HasColumnType("datetime");

                entity.Property(e => e.Locker).HasMaxLength(50);

                entity.Property(e => e.MayTinh)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mfieu)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("MFieu");

                entity.Property(e => e.NgayCt)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCT");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgoaiTe)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phong).HasMaxLength(50);
            });

            modelBuilder.Entity<TamUng>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.ConLai).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ConLaiNt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("ConLaiNT");

                entity.Property(e => e.DienGiai).HasMaxLength(150);

                entity.Property(e => e.LoaiTien)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCt)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCT");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhieuChi)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PhieuTt)
                    .HasMaxLength(80)
                    .HasColumnName("PhieuTT");

                entity.Property(e => e.Phong).HasMaxLength(20);

                entity.Property(e => e.SoCt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoCT");

                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SoTienNt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SoTienNT");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKCo");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKNo");

                entity.Property(e => e.Tttp).HasColumnName("TTTP");

                entity.Property(e => e.TyGia).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.TamUngNavigation)
                    .HasForeignKey<TamUng>(d => d.Id);
            });

            modelBuilder.Entity<TonQuy>(entity =>
            {
                entity.Property(e => e.LoaiTien)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCt)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCT");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiTao).HasMaxLength(50);

                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SoTienNt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SoTienNT");

                entity.Property(e => e.TyGia).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Tt621>(entity =>
            {
                entity.ToTable("TT621s");

                entity.HasIndex(e => e.TamUngId, "IX_TT621s_TamUngId");

                entity.Property(e => e.BoPhan)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.CoQuay)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.DienGiai).HasMaxLength(150);

                entity.Property(e => e.DienGiaiP).HasMaxLength(150);

                entity.Property(e => e.DskhongVat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("DSKhongVAT");

                entity.Property(e => e.GhiSo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoaDonDt)
                    .HasMaxLength(120)
                    .HasColumnName("HoaDonDT");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("HTTC");

                entity.Property(e => e.KyHieu)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.KyHieuHd)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("KyHieuHD");

                entity.Property(e => e.LapPhieu).HasMaxLength(50);

                entity.Property(e => e.LoaiHdgoc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("LoaiHDGoc");

                entity.Property(e => e.LoaiTien)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhCo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.MaKhNo)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.MatHang).HasMaxLength(60);

                entity.Property(e => e.MauSoHd)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("MauSoHD");

                entity.Property(e => e.MsThue)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCt)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCT");

                entity.Property(e => e.NgayCtgoc)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCTGoc");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NoQuay)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.PhieuTc)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PhieuTC");

                entity.Property(e => e.PhieuTu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PhieuTU");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.SoCt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoCT");

                entity.Property(e => e.SoCtgoc)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("SoCTGoc");

                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SoTienNt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("SoTienNT");

                entity.Property(e => e.SoXe)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.TenKh)
                    .HasMaxLength(100)
                    .HasColumnName("TenKH");

                entity.Property(e => e.Tkco)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKCo");

                entity.Property(e => e.Tkno)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKNo");

                entity.Property(e => e.TyGia).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("VAT");

                entity.HasOne(d => d.TamUng)
                    .WithMany(p => p.Tt621s)
                    .HasForeignKey(d => d.TamUngId);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
