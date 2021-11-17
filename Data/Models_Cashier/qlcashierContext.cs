using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_Cashier
{
    public partial class qlcashierContext : DbContext
    {
        public qlcashierContext()
        {
        }

        public qlcashierContext(DbContextOptions<qlcashierContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ctbill> Ctbills { get; set; }
        public virtual DbSet<CtbillDel> CtbillDels { get; set; }
        public virtual DbSet<Noptien> Noptiens { get; set; }
        public virtual DbSet<Ntbill> Ntbills { get; set; }
        public virtual DbSet<VBienhanHangkhong> VBienhanHangkhongs { get; set; }
        public virtual DbSet<VBiennhanInBound> VBiennhanInBounds { get; set; }
        public virtual DbSet<VBiennhanVanchuyen> VBiennhanVanchuyens { get; set; }
        public virtual DbSet<VCuocHangkhong> VCuocHangkhongs { get; set; }
        public virtual DbSet<VDataDatcocCashier> VDataDatcocCashiers { get; set; }
        public virtual DbSet<VDataVetourCashier> VDataVetourCashiers { get; set; }
        public virtual DbSet<VHoadonInBound> VHoadonInBounds { get; set; }
        public virtual DbSet<VHoadonVanchuyen> VHoadonVanchuyens { get; set; }
        public virtual DbSet<VHoanHangkhong> VHoanHangkhongs { get; set; }
        public virtual DbSet<VHoanVanchuyen> VHoanVanchuyens { get; set; }
        public virtual DbSet<VHoanVetourCashier> VHoanVetourCashiers { get; set; }
        public virtual DbSet<VLephihuyVetourCashier> VLephihuyVetourCashiers { get; set; }
        public virtual DbSet<VPhiDvHangkhong> VPhiDvHangkhongs { get; set; }
        public virtual DbSet<VPhihoanHangkhong> VPhihoanHangkhongs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=118.68.170.128;database=qlcashier;Trusted_Connection=true;User Id=vanhong;Password=Hong@2019;Integrated security=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Ctbill>(entity =>
            {
                entity.HasKey(e => e.Idctbill)
                    .HasName("PK_ctbill_1");

                entity.ToTable("ctbill");

                entity.Property(e => e.Idctbill)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Bophan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("bophan");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(50)
                    .HasColumnName("cardnumber");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(200)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Giamgia)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("giamgia");

                entity.Property(e => e.Hdvat).HasColumnName("hdvat");

                entity.Property(e => e.Idntbill)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("idntbill");

                entity.Property(e => e.Loaicard)
                    .HasMaxLength(50)
                    .HasColumnName("loaicard");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Nguoithu)
                    .HasMaxLength(50)
                    .HasColumnName("nguoithu");

                entity.Property(e => e.Ppv)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ppv");

                entity.Property(e => e.Saleslip)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("saleslip");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Soct)
                    .IsRequired()
                    .HasMaxLength(12)
                    .HasColumnName("soct");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Stt)
                    .HasMaxLength(20)
                    .HasColumnName("stt");

                entity.Property(e => e.Thanhtoanthe).HasColumnName("thanhtoanthe");

                entity.Property(e => e.Thuevat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("thuevat");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(6, 0)")
                    .HasColumnName("tygia")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("vat");
            });

            modelBuilder.Entity<CtbillDel>(entity =>
            {
                entity.HasKey(e => e.Idctbill);

                entity.ToTable("ctbill_del");

                entity.Property(e => e.Idctbill).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Bophan)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("bophan");

                entity.Property(e => e.Cardnumber)
                    .HasMaxLength(50)
                    .HasColumnName("cardnumber");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(200)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Giamgia)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("giamgia");

                entity.Property(e => e.Hdvat).HasColumnName("hdvat");

                entity.Property(e => e.Idntbill)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("idntbill");

                entity.Property(e => e.Loaicard)
                    .HasMaxLength(50)
                    .HasColumnName("loaicard");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Logfile).HasColumnName("logfile");

                entity.Property(e => e.Nguoithu)
                    .HasMaxLength(50)
                    .HasColumnName("nguoithu");

                entity.Property(e => e.Ppv)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("ppv");

                entity.Property(e => e.Saleslip)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("saleslip");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Soct)
                    .HasMaxLength(12)
                    .HasColumnName("soct");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .HasColumnName("stt");

                entity.Property(e => e.Thanhtoanthe).HasColumnName("thanhtoanthe");

                entity.Property(e => e.Thuevat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("thuevat");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(6, 0)")
                    .HasColumnName("tygia")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Vat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("vat");
            });

            modelBuilder.Entity<Noptien>(entity =>
            {
                entity.HasKey(e => new { e.Soct, e.Chinhanh });

                entity.ToTable("noptien");

                entity.Property(e => e.Soct)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("soct");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(50)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngaypt)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaypt");

                entity.Property(e => e.Nguoithu)
                    .HasMaxLength(50)
                    .HasColumnName("nguoithu");

                entity.Property(e => e.Phieuthu)
                    .HasMaxLength(50)
                    .HasColumnName("phieuthu");
            });

            modelBuilder.Entity<Ntbill>(entity =>
            {
                entity.HasKey(e => e.Idntbill)
                    .HasName("PK_ntbill_1");

                entity.ToTable("ntbill");

                entity.Property(e => e.Idntbill)
                    .HasColumnType("decimal(18, 0)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Bill)
                    .HasMaxLength(50)
                    .HasColumnName("bill");

                entity.Property(e => e.Bophan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("bophan");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(150)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Httt)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Huy).HasColumnName("huy");

                entity.Property(e => e.Idparent)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Khachle)
                    .IsRequired()
                    .HasColumnName("khachle")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Loaihd)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaihd");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(50)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngaybill)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaybill");

                entity.Property(e => e.Ngaytt)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytt");

                entity.Property(e => e.Nguoithu)
                    .HasMaxLength(50)
                    .HasColumnName("nguoithu");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Soct)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("soct");

                entity.Property(e => e.Stt)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(150)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VBienhanHangkhong>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vBienhanHangkhong");

                entity.Property(e => e.Biennhan)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("biennhan");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(150)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(150)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Httt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Loaitien)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("loaitien");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(100)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tygia)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("tygia");
            });

            modelBuilder.Entity<VBiennhanInBound>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vBiennhanInBound");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Diengiai)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ghichu");

                entity.Property(e => e.LoaiTien)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Macoquan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Ngaybiennhan).HasColumnName("ngaybiennhan");

                entity.Property(e => e.Ngaybn)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngaybn");

                entity.Property(e => e.PhongDh)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("PhongDH");

                entity.Property(e => e.Sgtcode)
                    .HasMaxLength(17)
                    .IsUnicode(false);

                entity.Property(e => e.SoBn)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("SoBN");

                entity.Property(e => e.SoKhachDk).HasColumnName("SoKhachDK");

                entity.Property(e => e.SoTien).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("sotiennt");

                entity.Property(e => e.TenKhach).HasMaxLength(150);

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(80)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.TyGia).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<VBiennhanVanchuyen>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vBiennhanVanchuyen");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Codedoan)
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("codedoan");

                entity.Property(e => e.Httt)
                    .HasMaxLength(10)
                    .HasColumnName("httt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Ngay)
                    .HasColumnType("datetime")
                    .HasColumnName("ngay");

                entity.Property(e => e.Ngaybn)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngaybn");

                entity.Property(e => e.Nguoitao)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("nguoitao");

                entity.Property(e => e.Noidung)
                    .HasMaxLength(250)
                    .HasColumnName("noidung");

                entity.Property(e => e.Sobiennhan)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("sobiennhan");

                entity.Property(e => e.Sotien)
                    .HasColumnType("numeric(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(250)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(100)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VCuocHangkhong>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vCuocHangkhong");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Sotien)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Stt)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tencoquan)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VDataDatcocCashier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDataDatcocCashier");

                entity.Property(e => e.Biennhan)
                    .HasMaxLength(12)
                    .HasColumnName("biennhan");

                entity.Property(e => e.Daily)
                    .HasMaxLength(25)
                    .HasColumnName("daily");

                entity.Property(e => e.Diengiai).HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(25)
                    .HasColumnName("ghichu")
                    .UseCollation("Latin1_General_CS_AS");

                entity.Property(e => e.Iddatcoc).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kenhtt)
                    .HasMaxLength(30)
                    .HasColumnName("kenhtt");

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Ngaydatcoc)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaydatcoc");

                entity.Property(e => e.Ngaydc)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngaydc");

                entity.Property(e => e.Ngayhuy)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngaythu)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaythu");

                entity.Property(e => e.Nguoihuy)
                    .HasMaxLength(50)
                    .HasColumnName("nguoihuy");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Sotiennt)
                    .HasColumnType("decimal(12, 0)")
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
            });

            modelBuilder.Entity<VDataVetourCashier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vDataVetourCashier");

                entity.Property(e => e.Dailyhuyve)
                    .HasMaxLength(25)
                    .HasColumnName("dailyhuyve");

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

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuyve");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaythutien");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxuatve");

                entity.Property(e => e.Nguoihuyve)
                    .HasMaxLength(25)
                    .HasColumnName("nguoihuyve");

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

            modelBuilder.Entity<VHoadonInBound>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHoadonInBound");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(80)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(25)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngayin");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(200)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VHoadonVanchuyen>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHoadonVanchuyen");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(80)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(50)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngayin");

                entity.Property(e => e.Sotien)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(200)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VHoanHangkhong>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHoanHangkhong");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Httt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayhoan)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhoan");

                entity.Property(e => e.Phieuhoan)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phieuhoan");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Sotien)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Tencoquan)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(150)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VHoanVanchuyen>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHoanVanchuyen");

                entity.Property(e => e.Chinhanh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(180)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(50)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Idhoadon)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Ngayin)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngayin");

                entity.Property(e => e.Stt)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(200)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VHoanVetourCashier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vHoanVetourCashier");

                entity.Property(e => e.Dailyhuyve)
                    .HasMaxLength(25)
                    .HasColumnName("dailyhuyve");

                entity.Property(e => e.Dailyxuatve)
                    .HasMaxLength(25)
                    .HasColumnName("dailyxuatve");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(160)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(25)
                    .HasColumnName("ghichu")
                    .UseCollation("Latin1_General_CS_AS");

                entity.Property(e => e.Huyve)
                    .HasMaxLength(12)
                    .HasColumnName("huyve");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kenhtt)
                    .HasMaxLength(30)
                    .HasColumnName("kenhtt");

                entity.Property(e => e.Lephihuy)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("lephihuy");

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Ngayhuy)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuyve");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaythutien");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxuatve");

                entity.Property(e => e.Nguoihuyve)
                    .HasMaxLength(25)
                    .HasColumnName("nguoihuyve");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(60)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tienhoan)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("tienhoan");

                entity.Property(e => e.Tour)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tour");
            });

            modelBuilder.Entity<VLephihuyVetourCashier>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vLephihuyVetourCashier");

                entity.Property(e => e.Dailyhuyve)
                    .HasMaxLength(25)
                    .HasColumnName("dailyhuyve");

                entity.Property(e => e.Dailyxuatve)
                    .HasMaxLength(25)
                    .HasColumnName("dailyxuatve");

                entity.Property(e => e.Diengiai)
                    .HasMaxLength(160)
                    .HasColumnName("diengiai");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(25)
                    .HasColumnName("ghichu")
                    .UseCollation("Latin1_General_CS_AS");

                entity.Property(e => e.Huyve)
                    .HasMaxLength(12)
                    .HasColumnName("huyve");

                entity.Property(e => e.Id).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Kenhtt)
                    .HasMaxLength(30)
                    .HasColumnName("kenhtt");

                entity.Property(e => e.Lephihuy)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("lephihuy");

                entity.Property(e => e.Macoquan)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("macoquan");

                entity.Property(e => e.Ngayhuy)
                    .HasMaxLength(19)
                    .IsUnicode(false)
                    .HasColumnName("ngayhuy");

                entity.Property(e => e.Ngayhuyve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhuyve");

                entity.Property(e => e.Ngaythutien)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaythutien");

                entity.Property(e => e.Ngayxuatve)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayxuatve");

                entity.Property(e => e.Nguoihuyve)
                    .HasMaxLength(25)
                    .HasColumnName("nguoihuyve");

                entity.Property(e => e.Sgtcode)
                    .IsRequired()
                    .HasMaxLength(17)
                    .IsUnicode(false)
                    .HasColumnName("sgtcode");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Tencoquan)
                    .HasMaxLength(60)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");

                entity.Property(e => e.Tienhoan)
                    .HasColumnType("decimal(12, 0)")
                    .HasColumnName("tienhoan");

                entity.Property(e => e.Tour)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("tour");
            });

            modelBuilder.Entity<VPhiDvHangkhong>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPhiDvHangkhong");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Httt)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayct)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayct");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Sotien)
                    .HasColumnType("numeric(38, 4)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Stt)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("stt");

                entity.Property(e => e.Tencoquan)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(50)
                    .HasColumnName("tenkhach");
            });

            modelBuilder.Entity<VPhihoanHangkhong>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vPhihoanHangkhong");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("chinhanh");

                entity.Property(e => e.Coquan)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("coquan");

                entity.Property(e => e.Ghichu)
                    .HasMaxLength(100)
                    .HasColumnName("ghichu");

                entity.Property(e => e.Hdvat)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("hdvat");

                entity.Property(e => e.Httt)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("httt");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Msthue)
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("msthue");

                entity.Property(e => e.Ngayhoan)
                    .HasColumnType("datetime")
                    .HasColumnName("ngayhoan");

                entity.Property(e => e.Phieuhoan)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("phieuhoan");

                entity.Property(e => e.Sk).HasColumnName("sk");

                entity.Property(e => e.Sotien)
                    .HasColumnType("numeric(38, 2)")
                    .HasColumnName("sotien");

                entity.Property(e => e.Tencoquan)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("tencoquan");

                entity.Property(e => e.Tenkhach)
                    .HasMaxLength(150)
                    .HasColumnName("tenkhach");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
