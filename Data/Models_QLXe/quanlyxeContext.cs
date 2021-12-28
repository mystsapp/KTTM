using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_QLXe
{
    public partial class quanlyxeContext : DbContext
    {
        public quanlyxeContext()
        {
        }

        public quanlyxeContext(DbContextOptions<quanlyxeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Thuchi> Thuchis { get; set; }
        public virtual DbSet<Vandoanh> Vandoanhs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=118.68.170.128;database=quanlyxe;Trusted_Connection=true;User Id=vanhong;Password=Hong@2019;Integrated security=false;MultipleActiveResultSets=true");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Thuchi>(entity =>
            {
                entity.ToTable("thuchi");

                entity.Property(e => e.ThuChiId).HasColumnName("ThuChiID");

                entity.Property(e => e.ChiNhanh)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DonGia).HasColumnType("money");

                entity.Property(e => e.Dvt)
                    .HasMaxLength(50)
                    .HasColumnName("DVT");

                entity.Property(e => e.HoaDonVat)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("HoaDonVAT");

                entity.Property(e => e.KhoanMuc).HasMaxLength(50);

                entity.Property(e => e.KyHieu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiThuChi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiTien)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ngay).HasColumnType("date");

                entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NgayVat)
                    .HasColumnType("date")
                    .HasColumnName("NgayVAT");

                entity.Property(e => e.NguoiCapNhat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoChungTuNb)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SoChungTuNB");

                entity.Property(e => e.SoCtKttm)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("SoCT_KTTM");

                entity.Property(e => e.SoLuong).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.SoPhieu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoTien).HasColumnType("money");

                entity.Property(e => e.SoXe)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Stt).HasColumnName("STT");

                entity.Property(e => e.TaiXe).HasMaxLength(50);

                entity.Property(e => e.TyGia).HasColumnType("money");

                entity.Property(e => e.VanDoanhId).HasColumnName("VanDoanhID");
            });

            modelBuilder.Entity<Vandoanh>(entity =>
            {
                entity.ToTable("vandoanh");

                entity.Property(e => e.VanDoanhId).HasColumnName("VanDoanhID");

                entity.Property(e => e.ChiNhanh)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.DaThuChi).HasDefaultValueSql("((0))");

                entity.Property(e => e.DenNgay).HasColumnType("date");

                entity.Property(e => e.DiemDon).HasMaxLength(250);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DienThoaiHd)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("DienThoaiHD");

                entity.Property(e => e.GhiChu).HasMaxLength(250);

                entity.Property(e => e.HuongDan).HasMaxLength(100);

                entity.Property(e => e.KhachHang).HasMaxLength(250);

                entity.Property(e => e.LoTrinh).HasMaxLength(150);

                entity.Property(e => e.LoaiXe).HasMaxLength(20);

                entity.Property(e => e.MaDoan)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");

                entity.Property(e => e.NgayCtp)
                    .HasColumnType("datetime")
                    .HasColumnName("NgayCTP");

                entity.Property(e => e.NgayDon).HasColumnType("date");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiCapNhat)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguonXe).HasMaxLength(150);

                entity.Property(e => e.PhuongThucTt)
                    .HasMaxLength(50)
                    .HasColumnName("PhuongThucTT");

                entity.Property(e => e.SoCtp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SoCTP");

                entity.Property(e => e.SoPhieu)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoXe)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Sttxe).HasColumnName("STTXe");

                entity.Property(e => e.TaiXe).HasMaxLength(50);

                entity.Property(e => e.TongTien).HasColumnType("money");

                entity.Property(e => e.YeuCauXe)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //private partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}