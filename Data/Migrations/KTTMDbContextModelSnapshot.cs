﻿// <auto-generated />
using System;
using Data.Models_KTTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(KTTMDbContext))]
    partial class KTTMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Models_KTTM.KVCTPTC", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoPhan")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("CardNumber")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CoQuay")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<decimal?>("DSKhongVAT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DienGiai")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("DienGiaiP")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("DieuChinh")
                        .HasColumnType("bit");

                    b.Property<string>("HTTC")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("HTTT")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("HoaDonDT")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<DateTime?>("KC141")
                        .HasColumnType("datetime");

                    b.Property<Guid>("KVPTCId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("KhoangMuc")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("KyHieu")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("LinkHDDT")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LoaiHDGoc")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LoaiThe")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("LoaiTien")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("MaCn")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("MaKh")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("MaKhCo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("MaKhNo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("MaTraCuu")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MatHang")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("MauSoHD")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("MsThue")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime?>("NgayCTGoc")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgaySua")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<string>("NguoiSua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NguoiTao")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NoQuay")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("STT")
                        .HasMaxLength(13)
                        .HasColumnType("varchar(13)");

                    b.Property<string>("SalesSlip")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Sgtcode")
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.Property<string>("SoCT")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SoCTGoc")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("SoTT_DaTao")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SoTU_DaTT")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal?>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SoTienNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SoVe")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SoXe")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("TKCo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("TKNo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("TamUng")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("TenKH")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TkTruyCap")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("TyGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("VAT")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("KVPTCId");

                    b.ToTable("KVCTPTCs");
                });

            modelBuilder.Entity("Data.Models_KTTM.KVPTC", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Create")
                        .HasColumnType("datetime");

                    b.Property<string>("DonVi")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("HoTen")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LapPhieu")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("Lock")
                        .HasColumnType("datetime");

                    b.Property<string>("Locker")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("MFieu")
                        .HasMaxLength(1)
                        .HasColumnType("varchar(1)");

                    b.Property<string>("MaCn")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("MayTinh")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<DateTime?>("NgayCT")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgaySua")
                        .HasColumnType("datetime");

                    b.Property<string>("NgoaiTe")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("NguoiSua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Phong")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("SoCT")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.HasKey("Id");

                    b.ToTable("KVPTCs");
                });

            modelBuilder.Entity("Data.Models_KTTM.TT621", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BoPhan")
                        .HasMaxLength(2)
                        .HasColumnType("varchar(2)");

                    b.Property<string>("CoQuay")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<decimal?>("DSKhongVAT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DiaChi")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("DienGiai")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("DienGiaiP")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("DieuChinh")
                        .HasColumnType("bit");

                    b.Property<string>("GhiSo")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("HTTC")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("HoaDonDT")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("KyHieu")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("KyHieuHD")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("LapPhieu")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LinkHDDT")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("LoaiHDGoc")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LoaiTien")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("MaCn")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("MaKhCo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("MaKhNo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("MaTraCuu")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("MatHang")
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("MauSoHD")
                        .HasMaxLength(11)
                        .HasColumnType("varchar(11)");

                    b.Property<string>("MsThue")
                        .HasMaxLength(16)
                        .HasColumnType("varchar(16)");

                    b.Property<DateTime?>("NgayCT")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayCTGoc")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgaySua")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<string>("NguoiSua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NguoiTao")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NoQuay")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("Password")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhieuTC")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PhieuTU")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("Sgtcode")
                        .HasMaxLength(17)
                        .HasColumnType("varchar(17)");

                    b.Property<string>("SoCT")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("SoCTGoc")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<decimal?>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SoTienNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SoVe")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("SoXe")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("TKCo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("TKNo")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<long>("TamUngId")
                        .HasColumnType("bigint");

                    b.Property<string>("TenKH")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TkTruyCap")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<decimal?>("TyGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("VAT")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("TamUngId");

                    b.ToTable("TT621s");
                });

            modelBuilder.Entity("Data.Models_KTTM.TamUng", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<decimal?>("ConLai")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("ConLaiNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DienGiai")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LoaiTien")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("MaCn")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<string>("MaKhNo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<DateTime?>("NgayCT")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgaySua")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<string>("NguoiSua")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("NguoiTao")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("PhieuChi")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("PhieuTT")
                        .HasMaxLength(80)
                        .HasColumnType("nvarchar(80)");

                    b.Property<string>("Phong")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("SoCT")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal?>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SoTienNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TKCo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("TKNo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<bool?>("TTTP")
                        .HasColumnType("bit");

                    b.Property<decimal?>("TyGia")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TamUngs");
                });

            modelBuilder.Entity("Data.Models_KTTM.TonQuy", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LoaiTien")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("MaCn")
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)");

                    b.Property<DateTime?>("NgayCT")
                        .IsRequired()
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("NgayTao")
                        .HasColumnType("datetime");

                    b.Property<string>("NguoiTao")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TyGia")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TonQuies");
                });

            modelBuilder.Entity("Data.Models_KTTM.KVCTPTC", b =>
                {
                    b.HasOne("Data.Models_KTTM.KVPTC", "KVPTC")
                        .WithMany()
                        .HasForeignKey("KVPTCId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KVPTC");
                });

            modelBuilder.Entity("Data.Models_KTTM.TT621", b =>
                {
                    b.HasOne("Data.Models_KTTM.TamUng", "TamUng")
                        .WithMany()
                        .HasForeignKey("TamUngId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TamUng");
                });

            modelBuilder.Entity("Data.Models_KTTM.TamUng", b =>
                {
                    b.HasOne("Data.Models_KTTM.KVCTPTC", "KVCTPTC")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KVCTPTC");
                });
#pragma warning restore 612, 618
        }
    }
}
