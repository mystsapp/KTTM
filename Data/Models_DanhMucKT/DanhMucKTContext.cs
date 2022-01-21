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
        public virtual DbSet<Dmkhbp> Dmkhbps { get; set; }
        public virtual DbSet<DvPhi> DvPhis { get; set; }
        public virtual DbSet<EmailKhachHang> EmailKhachHangs { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LoaiCard> LoaiCards { get; set; }
        public virtual DbSet<MatHang> MatHangs { get; set; }
        public virtual DbSet<NgoaiTe> NgoaiTes { get; set; }
        public virtual DbSet<PhongBan> PhongBans { get; set; }
        public virtual DbSet<Quay> Quays { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<TaiKhoanNganHang> TaiKhoanNganHangs { get; set; }
        public virtual DbSet<TkCongNo> TkCongNos { get; set; }
        public virtual DbSet<Tknh> Tknhs { get; set; }
        public virtual DbSet<VHangHk> VHangHks { get; set; }
        public virtual DbSet<ViewDmHttc> ViewDmHttcs { get; set; }
        public virtual DbSet<ViewDmTk> ViewDmTks { get; set; }
        public virtual DbSet<ViewMatHang> ViewMatHangs { get; set; }
        public virtual DbSet<ViewPhongBan> ViewPhongBans { get; set; }
        public virtual DbSet<ViewQuay> ViewQuays { get; set; }
        public virtual DbSet<ViewSupplier> ViewSuppliers { get; set; }
        public virtual DbSet<ViewSupplierCode> ViewSupplierCodes { get; set; }
        public virtual DbSet<VwDmkh> VwDmkhs { get; set; }
        public virtual DbSet<VwDmkhach> VwDmkhaches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=118.68.170.128;database=DanhMucKT;Trusted_Connection=true;User Id=vanhong;Password=Hong@2019;Integrated security=false;MultipleActiveResultSets=true");
            //            }
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

            modelBuilder.Entity<Dmkhbp>(entity =>
            {
                entity.ToTable("dmkhbp");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fax");

                entity.Property(e => e.Lienhe)
                    .HasMaxLength(50)
                    .HasColumnName("lienhe");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(50)
                    .HasColumnName("msthue");

                entity.Property(e => e.Source)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("source");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("tencoquan");
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

            modelBuilder.Entity<EmailKhachHang>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.Maviettat });

                entity.Property(e => e.Code)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Maviettat)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Email).HasMaxLength(200);
            });

            modelBuilder.Entity<KhachHang>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.Property(e => e.Code)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Contact).HasMaxLength(150);

                entity.Property(e => e.DaiLyVmb)
                    .IsRequired()
                    .HasColumnName("DaiLyVMB")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.DiaChi).HasMaxLength(300);

                entity.Property(e => e.DienThoai)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.KyHieuHd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("KyHieuHD");

                entity.Property(e => e.LinkHddt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("LinkHDDT");

                entity.Property(e => e.LoaiKh).HasMaxLength(50);

                entity.Property(e => e.MaSoThue)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.MauSoHd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("MauSoHD");

                entity.Property(e => e.NgaySua).HasColumnType("datetime");

                entity.Property(e => e.NgayTao).HasColumnType("datetime");

                entity.Property(e => e.NguoiSua)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NguoiTao)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.QuocGia).HasMaxLength(100);

                entity.Property(e => e.TenGiaoDich).HasMaxLength(300);

                entity.Property(e => e.TenNganHang).HasMaxLength(150);

                entity.Property(e => e.TenThuongMai).HasMaxLength(300);

                entity.Property(e => e.ThanhPho).HasMaxLength(100);

                entity.Property(e => e.TknganHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TKNganHang");

                entity.Property(e => e.Tpnh)
                    .HasMaxLength(100)
                    .HasColumnName("TPNH");
            });

            modelBuilder.Entity<LoaiCard>(entity =>
            {
                entity.HasKey(e => e.Loaithe)
                    .HasName("PK_loaicard");

                entity.ToTable("LoaiCard");

                entity.Property(e => e.Loaithe)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("loaithe");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(50)
                    .HasColumnName("diengiai");
            });

            modelBuilder.Entity<MatHang>(entity =>
            {
                entity.Property(e => e.Mathang1)
                    .HasMaxLength(100)
                    .HasColumnName("MATHANG");
            });

            modelBuilder.Entity<NgoaiTe>(entity =>
            {
                entity.HasKey(e => e.MaNt);

                entity.Property(e => e.MaNt)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("MaNT");

                entity.Property(e => e.TenNt)
                    .HasMaxLength(100)
                    .HasColumnName("TenNT");
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

            modelBuilder.Entity<TaiKhoanNganHang>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Code)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TenNganHang).HasMaxLength(150);

                entity.Property(e => e.TknganHang)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TKNganHang");

                entity.Property(e => e.Tpnh)
                    .HasMaxLength(100)
                    .HasColumnName("TPNH");
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

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

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

            modelBuilder.Entity<VHangHk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHangHK");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(300)
                    .HasColumnName("diachi");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(300)
                    .HasColumnName("tencoquan");
            });

            modelBuilder.Entity<ViewDmHttc>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_DmHTTC");

                entity.Property(e => e.DienGiai).HasMaxLength(100);

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("HTTC");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.MaIn).HasColumnName("Ma_In");
            });

            modelBuilder.Entity<ViewDmTk>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_DmTk");

                entity.Property(e => e.GhiChu).HasMaxLength(100);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

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

            modelBuilder.Entity<ViewMatHang>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_MatHang");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Mathang)
                    .HasMaxLength(100)
                    .HasColumnName("MATHANG");
            });

            modelBuilder.Entity<ViewPhongBan>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_PhongBan");

                entity.Property(e => e.BoPhan)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.TenBoPhan).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewQuay>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Quay");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Quay)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("QUAY");

                entity.Property(e => e.TenQuay).HasMaxLength(50);
            });

            modelBuilder.Entity<ViewSupplier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Suppliers");

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

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(200);

                entity.Property(e => e.Nation).HasMaxLength(15);

                entity.Property(e => e.PaymentCod)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.RealName).HasMaxLength(200);

                entity.Property(e => e.Supplier)
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

            modelBuilder.Entity<ViewSupplierCode>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_SupplierCode");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VwDmkh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_dmkh");

                entity.Property(e => e.Chinhanh).HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Daily).HasColumnName("daily");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(300)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fax");

                entity.Property(e => e.Hanmucno).HasColumnName("hanmucno");

                entity.Property(e => e.Hethanhd).HasColumnName("hethanhd");

                entity.Property(e => e.Hieuluchd).HasColumnName("hieuluchd");

                entity.Property(e => e.Httt)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Kyhieuhd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("kyhieuhd");

                entity.Property(e => e.Lienhe)
                    .HasMaxLength(150)
                    .HasColumnName("lienhe");

                entity.Property(e => e.Loaikhach)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("loaikhach");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Muave).HasColumnName("muave");

                entity.Property(e => e.Sales)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("sales");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(300)
                    .HasColumnName("tencoquan");
            });

            modelBuilder.Entity<VwDmkhach>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_dmkhach");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(300);

                entity.Property(e => e.Name).HasMaxLength(300);

                entity.Property(e => e.NganHang)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.SwiftCode)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.TaiKhoan)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TaxCode)
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.TaxSign)
                    .IsRequired()
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.ThanhPho)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            //OnModelCreatingPartial(modelBuilder);
        }

        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}