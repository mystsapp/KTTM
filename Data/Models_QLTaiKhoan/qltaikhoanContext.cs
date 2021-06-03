﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Data.Models_QLTaiKhoan
{
    public partial class qltaikhoanContext : DbContext
    {
        public qltaikhoanContext()
        {
        }

        public qltaikhoanContext(DbContextOptions<qltaikhoanContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppUser> AppUsers { get; set; }
        public virtual DbSet<Application> Applications { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public virtual DbSet<City> Citys { get; set; }
        public virtual DbSet<Dichvu> Dichvus { get; set; }
        public virtual DbSet<Dmchinhanh> Dmchinhanhs { get; set; }
        public virtual DbSet<Dmdaily> Dmdailies { get; set; }
        public virtual DbSet<FolderUser> FolderUsers { get; set; }
        public virtual DbSet<Mien> Miens { get; set; }
        public virtual DbSet<National> Nationals { get; set; }
        public virtual DbSet<Phongban> Phongbans { get; set; }
        public virtual DbSet<Quan> Quans { get; set; }
        public virtual DbSet<Quocgium> Quocgia { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Supplierob> Supplierobs { get; set; }
        public virtual DbSet<Thanhpho> Thanhphos { get; set; }
        public virtual DbSet<Thanhpho1> Thanhpho1s { get; set; }
        public virtual DbSet<Tinh> Tinhs { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<VSupplierobnation> VSupplierobnations { get; set; }
        public virtual DbSet<VTinh> VTinhs { get; set; }
        public virtual DbSet<Vungmien> Vungmiens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.4.198,1434;database=qltaikhoan;Trusted_Connection=true;User Id=sa;Password=123456;Integrated security=false;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.ToTable("AppUser");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Chuongtrinh)
                    .HasMaxLength(70)
                    .HasColumnName("chuongtrinh");

                entity.Property(e => e.Mact)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("mact");

                entity.Property(e => e.Mota)
                    .HasMaxLength(100)
                    .HasColumnName("mota");

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Application>(entity =>
            {
                entity.HasKey(e => e.Mact)
                    .HasName("PK_chuongtrinh");

                entity.ToTable("Application");

                entity.Property(e => e.Mact)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("mact");

                entity.Property(e => e.Chuongtrinh)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("chuongtrinh");

                entity.Property(e => e.Link)
                    .HasMaxLength(50)
                    .HasColumnName("link");

                entity.Property(e => e.Mota)
                    .HasMaxLength(150)
                    .HasColumnName("mota");
            });

            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.HasKey(e => new { e.Username, e.Mact });

                entity.ToTable("ApplicationUser");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Mact)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("mact");

                entity.HasOne(d => d.MactNavigation)
                    .WithMany(p => p.ApplicationUsers)
                    .HasForeignKey(d => d.Mact)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ApplicationUser_Application");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasKey(e => e.CityCode);

                entity.Property(e => e.CityCode)
                    .HasMaxLength(6)
                    .IsUnicode(false);

                entity.Property(e => e.CityName).HasMaxLength(50);

                entity.Property(e => e.NationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Dichvu>(entity =>
            {
                entity.HasKey(e => e.Iddichvu);

                entity.ToTable("dichvu");

                entity.Property(e => e.Iddichvu)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("iddichvu");

                entity.Property(e => e.Tendv)
                    .HasMaxLength(50)
                    .HasColumnName("tendv");

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Dmchinhanh>(entity =>
            {
                entity.ToTable("dmchinhanh");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("fax");

                entity.Property(e => e.Macn)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("macn");

                entity.Property(e => e.Masothue)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("masothue");

                entity.Property(e => e.Tencn)
                    .HasMaxLength(50)
                    .HasColumnName("tencn");

                entity.Property(e => e.Thanhpho)
                    .HasMaxLength(50)
                    .HasColumnName("thanhpho");

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Dmdaily>(entity =>
            {
                entity.ToTable("dmdaily");

                entity.Property(e => e.Daily)
                    .HasMaxLength(25)
                    .HasColumnName("daily");

                entity.Property(e => e.Diachi)
                    .HasMaxLength(100)
                    .HasColumnName("diachi");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(50)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Fax)
                    .HasMaxLength(50)
                    .HasColumnName("fax");

                entity.Property(e => e.Macn)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("macn");

                entity.Property(e => e.Tendaily)
                    .HasMaxLength(100)
                    .HasColumnName("tendaily");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<FolderUser>(entity =>
            {
                entity.ToTable("FolderUser");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.Path)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Mien>(entity =>
            {
                entity.ToTable("Mien");

                entity.Property(e => e.MienId)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.TenMien).HasMaxLength(50);
            });

            modelBuilder.Entity<National>(entity =>
            {
                entity.HasKey(e => e.NationCode);

                entity.Property(e => e.NationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Continent).HasMaxLength(50);

                entity.Property(e => e.NationName).HasMaxLength(50);

                entity.Property(e => e.Territory).HasMaxLength(50);
            });

            modelBuilder.Entity<Phongban>(entity =>
            {
                entity.HasKey(e => e.Maphong);

                entity.ToTable("phongban");

                entity.Property(e => e.Maphong)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("maphong");

                entity.Property(e => e.Hdvat).HasColumnName("hdvat");

                entity.Property(e => e.Macode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("macode");

                entity.Property(e => e.Tenphong)
                    .HasMaxLength(50)
                    .HasColumnName("tenphong");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<Quan>(entity =>
            {
                entity.ToTable("quan");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Tenquan)
                    .HasMaxLength(50)
                    .HasColumnName("tenquan");

                entity.Property(e => e.Tentp)
                    .HasMaxLength(50)
                    .HasColumnName("tentp");
            });

            modelBuilder.Entity<Quocgium>(entity =>
            {
                entity.Property(e => e.TenNuoc).HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("roleId");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("roleName");

                entity.Property(e => e.Trangthai)
                    .IsRequired()
                    .HasColumnName("trangthai")
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.Code)
                    .HasName("PK_supplier_1");

                entity.ToTable("supplier");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi).HasMaxLength(150);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(150);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Masothue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nganhnghe).HasMaxLength(150);

                entity.Property(e => e.Ngayhethan).HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoilienhe).HasMaxLength(150);

                entity.Property(e => e.Nguoitao).HasMaxLength(50);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tapdoan).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(100);

                entity.Property(e => e.Tennganhang).HasMaxLength(50);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(100);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.Tinhtp).HasMaxLength(50);

                entity.Property(e => e.Tknganhang).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(200);
            });

            modelBuilder.Entity<Supplierob>(entity =>
            {
                entity.HasKey(e => e.Code);

                entity.ToTable("supplierob");

                entity.Property(e => e.Code)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Chinhanh)
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Diachi).HasMaxLength(250);

                entity.Property(e => e.Dienthoai).HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.Fax).HasMaxLength(50);

                entity.Property(e => e.Masothue)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nganhnghe).HasMaxLength(300);

                entity.Property(e => e.Ngayhethan).HasColumnType("date");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoilienhe).HasMaxLength(100);

                entity.Property(e => e.Nguoitao).HasMaxLength(50);

                entity.Property(e => e.Quocgia).HasMaxLength(50);

                entity.Property(e => e.Tapdoan).HasMaxLength(50);

                entity.Property(e => e.Tengiaodich).HasMaxLength(100);

                entity.Property(e => e.Tennganhang).HasMaxLength(50);

                entity.Property(e => e.Tenthuongmai).HasMaxLength(100);

                entity.Property(e => e.Thanhpho).HasMaxLength(50);

                entity.Property(e => e.Tinhtp).HasMaxLength(50);

                entity.Property(e => e.Tknganhang).HasMaxLength(50);

                entity.Property(e => e.Website).HasMaxLength(150);
            });

            modelBuilder.Entity<Thanhpho>(entity =>
            {
                entity.HasKey(e => e.Matp);

                entity.ToTable("thanhpho");

                entity.Property(e => e.Matp)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("matp");

                entity.Property(e => e.Mien)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mien");

                entity.Property(e => e.Tentp)
                    .HasMaxLength(50)
                    .HasColumnName("tentp");
            });

            modelBuilder.Entity<Thanhpho1>(entity =>
            {
                entity.HasKey(e => e.Matp)
                    .HasName("PK_thanhpho1_1");

                entity.ToTable("thanhpho1");

                entity.Property(e => e.Matp).HasMaxLength(6);

                entity.Property(e => e.Matinh)
                    .IsRequired()
                    .HasMaxLength(3)
                    .IsUnicode(false);

                entity.Property(e => e.Tentp).HasMaxLength(50);
            });

            modelBuilder.Entity<Tinh>(entity =>
            {
                entity.HasKey(e => e.Matinh);

                entity.ToTable("Tinh");

                entity.Property(e => e.Matinh).HasMaxLength(3);

                entity.Property(e => e.MienId)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Tentinh).HasMaxLength(50);

                entity.Property(e => e.VungId)
                    .HasMaxLength(5)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("users");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.Property(e => e.Dienthoai)
                    .HasMaxLength(50)
                    .HasColumnName("dienthoai");

                entity.Property(e => e.Doimk).HasColumnName("doimk");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Hoten)
                    .HasMaxLength(50)
                    .HasColumnName("hoten");

                entity.Property(e => e.Macn)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("macn");

                entity.Property(e => e.Maphong)
                    .HasMaxLength(50)
                    .HasColumnName("maphong");

                entity.Property(e => e.Ngaydoimk)
                    .HasColumnType("date")
                    .HasColumnName("ngaydoimk")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ngaytao)
                    .HasColumnType("datetime")
                    .HasColumnName("ngaytao")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Nguoitao)
                    .HasMaxLength(50)
                    .HasColumnName("nguoitao");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("roleId");

                entity.Property(e => e.Trangthai).HasColumnName("trangthai");
            });

            modelBuilder.Entity<VSupplierobnation>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vSupplierobnation");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Tengiaodich)
                    .HasMaxLength(166)
                    .HasColumnName("tengiaodich");
            });

            modelBuilder.Entity<VTinh>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vTinh");

                entity.Property(e => e.Matinh)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Mien)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mien");

                entity.Property(e => e.TenMien).HasMaxLength(50);

                entity.Property(e => e.TenVung).HasMaxLength(50);

                entity.Property(e => e.Tentinh).HasMaxLength(50);

                entity.Property(e => e.VungId)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("vungId");
            });

            modelBuilder.Entity<Vungmien>(entity =>
            {
                entity.HasKey(e => e.VungId);

                entity.ToTable("vungmien");

                entity.Property(e => e.VungId)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("vungId");

                entity.Property(e => e.Mien)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("mien");

                entity.Property(e => e.TenVung).HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
