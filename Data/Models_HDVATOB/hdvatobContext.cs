using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_HDVATOB
{
    public partial class hdvatobContext : DbContext
    {
        public hdvatobContext()
        {
        }

        public hdvatobContext(DbContextOptions<hdvatobContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cthdvat> Cthdvats { get; set; }
        public virtual DbSet<CthdvatDel> CthdvatDels { get; set; }
        public virtual DbSet<Cttachve> Cttachves { get; set; }
        public virtual DbSet<Dmhttc> Dmhttcs { get; set; }
        public virtual DbSet<Dmtk> Dmtks { get; set; }
        public virtual DbSet<Dvphi> Dvphis { get; set; }
        public virtual DbSet<Hoadon> Hoadons { get; set; }
        public virtual DbSet<HoadonDel> HoadonDels { get; set; }
        public virtual DbSet<HoadonHuy> HoadonHuys { get; set; }
        public virtual DbSet<Huycthdvat> Huycthdvats { get; set; }
        public virtual DbSet<Huyhdvat> Huyhdvats { get; set; }
        public virtual DbSet<Loaikhach> Loaikhaches { get; set; }
        public virtual DbSet<Nguonhd> Nguonhds { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Tachve> Tachves { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VDataVetourCashier> VDataVetourCashiers { get; set; }
        public virtual DbSet<VDmkhVmb> VDmkhVmbs { get; set; }
        public virtual DbSet<VPhongban> VPhongbans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=192.168.4.198,1434;database=hdvatob;Trusted_Connection=true;User Id=sa;Password=123456;Integrated security=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cthdvat>(entity =>
            {
                entity.ToTable("cthdvat");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Dichvu)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dichvu");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(150)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(160)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hoadonhuy)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hoadonhuy");

                entity.Property(e => e.Hoahong).HasColumnName("hoahong");

                entity.Property(e => e.Hoahongnt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("hoahongnt");

                entity.Property(e => e.Hoahongvnd)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("hoahongvnd");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Km).HasColumnName("km");

                entity.Property(e => e.Laixe)
                    .HasMaxLength(50)
                    .HasColumnName("laixe");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Mabh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("mabh");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Number)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.Property(e => e.Ppv)
                    .HasColumnType("decimal(2, 0)")
                    .HasColumnName("ppv");

                entity.Property(e => e.Serial)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Slve).HasColumnName("slve");

                entity.Property(e => e.Soctnb)
                    .HasMaxLength(50)
                    .HasColumnName("soctnb");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Soxe)
                    .HasMaxLength(50)
                    .HasColumnName("soxe");

                entity.Property(e => e.Sttdong).HasColumnName("sttdong");

                entity.Property(e => e.Tenbaohiem)
                    .HasMaxLength(150)
                    .HasColumnName("tenbaohiem");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(140)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tiencoupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("tiencoupon");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tour)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("tour");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(8, 1)")
                    .HasColumnName("tygia")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnType("datetime")
                    .HasColumnName("xuatve");

                entity.Property(e => e.Yeucauxe)
                    .HasMaxLength(50)
                    .HasColumnName("yeucauxe");
            });

            modelBuilder.Entity<CthdvatDel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("cthdvat_del");

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Dichvu)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dichvu");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(150)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(160)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hoadonhuy)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hoadonhuy");

                entity.Property(e => e.Hoahong).HasColumnName("hoahong");

                entity.Property(e => e.Hoahongnt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("hoahongnt");

                entity.Property(e => e.Hoahongvnd)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("hoahongvnd");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Km).HasColumnName("km");

                entity.Property(e => e.Laixe)
                    .HasMaxLength(50)
                    .HasColumnName("laixe");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Mabh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("mabh");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Number)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.Property(e => e.Ppv)
                    .HasColumnType("decimal(2, 0)")
                    .HasColumnName("ppv");

                entity.Property(e => e.Serial)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Slve).HasColumnName("slve");

                entity.Property(e => e.Soctnb)
                    .HasMaxLength(50)
                    .HasColumnName("soctnb");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Soxe)
                    .HasMaxLength(50)
                    .HasColumnName("soxe");

                entity.Property(e => e.Sttdong).HasColumnName("sttdong");

                entity.Property(e => e.Tenbaohiem)
                    .HasMaxLength(150)
                    .HasColumnName("tenbaohiem");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(140)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tiencoupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("tiencoupon");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tour)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("tour");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(8, 1)")
                    .HasColumnName("tygia");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnType("date")
                    .HasColumnName("xuatve");

                entity.Property(e => e.Yeucauxe)
                    .HasMaxLength(50)
                    .HasColumnName("yeucauxe");
            });

            modelBuilder.Entity<Cttachve>(entity =>
            {
                entity.ToTable("cttachve");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("coupon");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(150)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(60)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ppv)
                    .HasColumnType("decimal(2, 0)")
                    .HasColumnName("ppv");

                entity.Property(e => e.Serial)
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(140)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tiencoupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("tiencoupon");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tour)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tour");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(8, 1)")
                    .HasColumnName("tygia");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnType("date")
                    .HasColumnName("xuatve");
            });

            modelBuilder.Entity<Dmhttc>(entity =>
            {
                entity.HasKey(e => e.Httc);

                entity.ToTable("dmhttc");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(40)
                    .HasColumnName("diengiai");

                entity.Property(e => e.MaIn)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("ma_in");
            });

            modelBuilder.Entity<Dmtk>(entity =>
            {
                entity.ToTable("dmtk");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Sudung).HasColumnName("sudung");

                entity.Property(e => e.Tentk)
                    .HasMaxLength(100)
                    .HasColumnName("tentk");

                entity.Property(e => e.Tkhoan)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkhoan");

                entity.Property(e => e.Tkhoan1)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tkhoan1");

                entity.Property(e => e.Tkhoan2)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("tkhoan2");

                entity.Property(e => e.Tkhoancu)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("tkhoancu");
            });

            modelBuilder.Entity<Dvphi>(entity =>
            {
                entity.HasKey(e => e.Srvtype)
                    .HasName("PK_dvphi_1");

                entity.ToTable("dvphi");

                entity.Property(e => e.Srvtype)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("srvtype");

                entity.Property(e => e.Dvphi1)
                    .HasColumnType("decimal(4, 1)")
                    .HasColumnName("dvphi");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Vatra).HasColumnName("vatra");

                entity.Property(e => e.Vatvao).HasColumnName("vatvao");
            });

            modelBuilder.Entity<Hoadon>(entity =>
            {
                entity.HasKey(e => new { e.Idhoadon, e.Chinhanh })
                    .HasName("PK_hoadon_1");

                entity.ToTable("hoadon");

                entity.Property(e => e.Idhoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Batdau)
                    .HasColumnType("date")
                    .HasColumnName("batdau");

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(150)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(80)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Hopdong)
                    .HasMaxLength(120)
                    .HasColumnName("hopdong");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Ketthuc)
                    .HasColumnType("date")
                    .HasColumnName("ketthuc");

                entity.Property(e => e.Keyhddt)
                    .HasMaxLength(120)
                    .HasColumnName("keyhddt");

                entity.Property(e => e.Kyhieu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("kyhieu");

                entity.Property(e => e.Loaikhach)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("loaikhach");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayin");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxoa");

                entity.Property(e => e.Nguoitaohd)
                    .HasMaxLength(50)
                    .HasColumnName("nguoitaohd");

                entity.Property(e => e.Nguonhd)
                    .HasMaxLength(100)
                    .HasColumnName("nguonhd");

                entity.Property(e => e.Phongban)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("phongban");

                entity.Property(e => e.Serial)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(200)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.User)
                    .HasMaxLength(25)
                    .HasColumnName("user");
            });

            modelBuilder.Entity<HoadonDel>(entity =>
            {
                entity.ToTable("hoadon_del");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Batdau)
                    .HasColumnType("date")
                    .HasColumnName("batdau");

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(150)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(80)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Hopdong)
                    .HasMaxLength(12)
                    .HasColumnName("hopdong");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ketthuc)
                    .HasColumnType("date")
                    .HasColumnName("ketthuc");

                entity.Property(e => e.Keyhddt)
                    .HasMaxLength(120)
                    .HasColumnName("keyhddt");

                entity.Property(e => e.Kyhieu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("kyhieu");

                entity.Property(e => e.Loaikhach)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("loaikhach");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayin");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxoa");

                entity.Property(e => e.Nguoitaohd)
                    .HasMaxLength(50)
                    .HasColumnName("nguoitaohd");

                entity.Property(e => e.Nguonhd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nguonhd");

                entity.Property(e => e.Phongban)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("phongban");

                entity.Property(e => e.Serial)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(200)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.User)
                    .HasMaxLength(25)
                    .HasColumnName("user");
            });

            modelBuilder.Entity<HoadonHuy>(entity =>
            {
                entity.ToTable("hoadon_huy");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Batdau)
                    .HasColumnType("date")
                    .HasColumnName("batdau");

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(150)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(80)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Hopdong)
                    .HasMaxLength(12)
                    .HasColumnName("hopdong");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Ketthuc)
                    .HasColumnType("date")
                    .HasColumnName("ketthuc");

                entity.Property(e => e.Keyhddt)
                    .HasMaxLength(120)
                    .HasColumnName("keyhddt");

                entity.Property(e => e.Kyhieu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("kyhieu");

                entity.Property(e => e.Loaikhach)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("loaikhach");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayin");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxoa");

                entity.Property(e => e.Nguoitaohd)
                    .HasMaxLength(50)
                    .HasColumnName("nguoitaohd");

                entity.Property(e => e.Nguonhd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nguonhd");

                entity.Property(e => e.Phongban)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("phongban");

                entity.Property(e => e.Serial)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(200)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.User)
                    .HasMaxLength(25)
                    .HasColumnName("user");
            });

            modelBuilder.Entity<Huycthdvat>(entity =>
            {
                entity.ToTable("huycthdvat");

                entity.Property(e => e.Id)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Dichvu)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("dichvu");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(150)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hoadonhuy)
                    .HasMaxLength(20)
                    .HasColumnName("hoadonhuy");

                entity.Property(e => e.Httc)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("httc");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Khachhuy).HasColumnName("khachhuy");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Number)
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("number");

                entity.Property(e => e.Ppv)
                    .HasColumnType("decimal(5, 0)")
                    .HasColumnName("ppv");

                entity.Property(e => e.Serial)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Slve).HasColumnName("slve");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(14, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(40)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tiencoupon)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("tiencoupon");

                entity.Property(e => e.Tkco)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkco");

                entity.Property(e => e.Tkno)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("tkno");

                entity.Property(e => e.Tour)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tour");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(8, 1)")
                    .HasColumnName("tygia");

                entity.Property(e => e.Vat).HasColumnName("vat");

                entity.Property(e => e.Xuatve)
                    .HasColumnType("datetime")
                    .HasColumnName("xuatve");
            });

            modelBuilder.Entity<Huyhdvat>(entity =>
            {
                entity.HasKey(e => new { e.Idhoadon, e.Chinhanh });

                entity.ToTable("huyhdvat");

                entity.Property(e => e.Idhoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Datelock)
                    .HasColumnType("datetime")
                    .HasColumnName("datelock");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(200)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(180)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Hopdong)
                    .HasMaxLength(50)
                    .HasColumnName("hopdong");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Keyhddt)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("keyhddt");

                entity.Property(e => e.Kyhieu)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("kyhieu");

                entity.Property(e => e.Loaikhach)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("loaikhach");

                entity.Property(e => e.Locker)
                    .HasMaxLength(50)
                    .HasColumnName("locker");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Mausohd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("mausohd");

                entity.Property(e => e.Maytinh)
                    .HasMaxLength(50)
                    .HasColumnName("maytinh");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayin");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxoa");

                entity.Property(e => e.Nguoitaohd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nguoitaohd");

                entity.Property(e => e.Nguonhd)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nguonhd");

                entity.Property(e => e.Noidunghuy)
                    .HasMaxLength(100)
                    .HasColumnName("noidunghuy");

                entity.Property(e => e.Phongban)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("phongban");

                entity.Property(e => e.Serial)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(200)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<Loaikhach>(entity =>
            {
                entity.ToTable("Loaikhach");

                entity.Property(e => e.Loaikhachid)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("loaikhachid");

                entity.Property(e => e.Tenloaikhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenloaikhach");
            });

            modelBuilder.Entity<Nguonhd>(entity =>
            {
                entity.HasKey(e => e.IdNguonhd)
                    .HasName("PK_Nguonhd");

                entity.ToTable("nguonhd");

                entity.Property(e => e.IdNguonhd)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Diengiai).HasMaxLength(50);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => new { e.Code, e.Chinhanh })
                    .HasName("PK_supplier_1");

                entity.ToTable("supplier");

                entity.Property(e => e.Code)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("code");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.City)
                    .HasMaxLength(15)
                    .HasColumnName("city");

                entity.Property(e => e.Contact)
                    .HasMaxLength(50)
                    .HasColumnName("contact");

                entity.Property(e => e.Daily).HasColumnName("daily");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(200)
                    .HasColumnName("email");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .HasColumnName("fax");

                entity.Property(e => e.Field)
                    .HasMaxLength(50)
                    .HasColumnName("field");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Muave).HasColumnName("muave");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.Nation)
                    .HasMaxLength(50)
                    .HasColumnName("nation");

                entity.Property(e => e.Paymentcode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("paymentcode");

                entity.Property(e => e.Realname)
                    .HasMaxLength(200)
                    .HasColumnName("realname");

                entity.Property(e => e.Room).HasColumnName("room");

                entity.Property(e => e.Suppliercode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("suppliercode");

                entity.Property(e => e.Taxcode)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("taxcode");

                entity.Property(e => e.Taxform)
                    .HasMaxLength(50)
                    .HasColumnName("taxform");

                entity.Property(e => e.Taxsign)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("taxsign");

                entity.Property(e => e.Telephone)
                    .HasMaxLength(50)
                    .HasColumnName("telephone");

                entity.Property(e => e.Website)
                    .HasMaxLength(60)
                    .HasColumnName("website");
            });

            modelBuilder.Entity<Tachve>(entity =>
            {
                entity.HasKey(e => new { e.Idhoadon, e.Chinhanh });

                entity.ToTable("tachve");

                entity.Property(e => e.Idhoadon)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Capnhat)
                    .HasColumnType("datetime")
                    .HasColumnName("capnhat");

                entity.Property(e => e.Chuyenvat)
                    .HasMaxLength(50)
                    .HasColumnName("chuyenvat");

                entity.Property(e => e.Coupon).HasColumnName("coupon");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(200)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(50)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Hopdong)
                    .HasMaxLength(12)
                    .HasColumnName("hopdong");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Makh)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngaychuyen)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaychuyen");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("date")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayin");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Ngayxoa)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxoa");

                entity.Property(e => e.Nguoitach)
                    .HasMaxLength(50)
                    .HasColumnName("nguoitach");

                entity.Property(e => e.Serial)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("serial");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tenkh)
                    .HasMaxLength(100)
                    .HasColumnName("tenkh");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tienve)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("tienve");

                entity.Property(e => e.Tour)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tour");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username)
                    .HasName("PK_users_1");

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Accounthddt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("accounthddt");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Maviettat)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("maviettat");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao");

                entity.Property(e => e.Nguoitao)
                    .HasMaxLength(50)
                    .HasColumnName("nguoitao");

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .HasColumnName("password");

                entity.Property(e => e.Passwordhddt)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("passwordhddt");
            });

            modelBuilder.Entity<VDataVetourCashier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDataVetourCashier");

                entity.Property(e => e.Dailyxuatve)
                    .HasMaxLength(25)
                    .HasColumnName("dailyxuatve");

                entity.Property(e => e.Datcoc)
                    .HasColumnType("decimal(38, 0)")
                    .HasColumnName("datcoc");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(215)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(25)
                    .HasColumnName("ghichu")
                    .UseCollation("Latin1_General_CS_AS");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kenhtt)
                    .HasMaxLength(30)
                    .HasColumnName("kenhtt");

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Makh)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("makh");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxuatve");

                entity.Property(e => e.Serial)
                    .HasMaxLength(12)
                    .HasColumnName("serial");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(16, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(60)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tour)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tour");

                entity.Property(e => e.Xuatve)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("xuatve");
            });

            modelBuilder.Entity<VDmkhVmb>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDmkh_vmb");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Tencoquan)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");
            });

            modelBuilder.Entity<VPhongban>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPhongban");

                entity.Property(e => e.Maphong)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maphong");

                entity.Property(e => e.Tenphong)
                    .HasMaxLength(50)
                    .HasColumnName("tenphong");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
