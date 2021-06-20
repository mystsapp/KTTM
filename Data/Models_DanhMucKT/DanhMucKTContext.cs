using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_DanhMucKT
{
    public partial class DanhMucKTContext : DbContext
    {
        public DanhMucKTContext()
        {
        }

        public DanhMucKTContext(DbContextOptions<DanhMucKTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Dgiai> Dgiais { get; set; }
        public virtual DbSet<DmHttc> DmHttcs { get; set; }
        public virtual DbSet<DmTk> DmTks { get; set; }
        public virtual DbSet<DvPhi> DvPhis { get; set; }
        public virtual DbSet<MatHang> MatHangs { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<Quay> Quays { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TkCongNo> TkCongNos { get; set; }
        public virtual DbSet<Tknh> Tknhs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=192.168.4.198,1434;database=DanhMucKT;Trusted_Connection=true;User Id=sa;Password=123456;Integrated security=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Dgiai>(entity =>
            {
                entity.ToTable("DGiais");

                entity.Property(e => e.DienGiai).HasMaxLength(100);

                entity.Property(e => e.Tkco)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKCo");

                entity.Property(e => e.TkcoCu)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("TKCoCu");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKNo");

                entity.Property(e => e.TknoCu)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("TKNoCu");
            });

            modelBuilder.Entity<DmHttc>(entity =>
            {
                entity.ToTable("Dm_HTTCs");

                entity.Property(e => e.DienGiai).HasMaxLength(100);

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("HTTC");

                entity.Property(e => e.MaIn).HasColumnName("Ma_In");
            });

            modelBuilder.Entity<DmTk>(entity =>
            {
                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.TenTk)
                    .HasMaxLength(150)
                    .HasColumnName("TenTK");

                entity.Property(e => e.Tkhoan)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKhoan");

                entity.Property(e => e.Tkhoan1)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKhoan1");

                entity.Property(e => e.Tkhoan2)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKhoan2");

                entity.Property(e => e.TkhoanCu)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("TKhoanCu");
            });

            modelBuilder.Entity<DvPhi>(entity =>
            {
                entity.HasKey(e => e.SrvType);

                entity.Property(e => e.SrvType)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Dvphi1)
                    .HasColumnType("numeric(4, 1)")
                    .HasColumnName("DVPhi");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("HTTC");

                entity.Property(e => e.Vatra).HasColumnName("VATRa");

                entity.Property(e => e.Vatvao).HasColumnName("VATVao");
            });

            modelBuilder.Entity<MatHang>(entity =>
            {
                entity.Property(e => e.Mathang1)
                    .HasMaxLength(100)
                    .HasColumnName("MATHANG");
            });

            modelBuilder.Entity<PhongBan>(entity =>
            {
                entity.Property(e => e.BoPhan)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.TenBoPhan).HasMaxLength(50);
            });

            modelBuilder.Entity<Quay>(entity =>
            {
                entity.Property(e => e.Quay1)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("QUAY");

                entity.Property(e => e.TenQuay).HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(200);

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Contact).HasMaxLength(25);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Field).HasMaxLength(25);

                entity.Property(e => e.Httt)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("HTTT");

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Nation).HasMaxLength(15);

                entity.Property(e => e.PaymentCod)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RealName).HasMaxLength(200);

                entity.Property(e => e.Supplier1)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("SUPPLIER");

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TaxForm)
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.TaxSign)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Telephone)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TkCongNo>(entity =>
            {
                entity.Property(e => e.Tkhoan)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("TKhoan");

                entity.Property(e => e.TkhoanCu)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("TKhoanCu");
            });

            modelBuilder.Entity<Tknh>(entity =>
            {
                entity.ToTable("TKNHs");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi).HasMaxLength(150);

                entity.Property(e => e.LoaiTien)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.NganHang).HasMaxLength(150);

                entity.Property(e => e.QuocGia).HasMaxLength(50);

                entity.Property(e => e.SwiftCode)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.TaiKhoan)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.ThanhPho).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
