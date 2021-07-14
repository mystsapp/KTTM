﻿// <auto-generated />
using System;
using Data.Models_KTTM;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(KTTMDbContext))]
    [Migration("20210713080946_themTamUng")]
    partial class themTamUng
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Models_KTTM.KVCTPCT", b =>
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

                    b.Property<decimal>("DSKhongVAT")
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

                    b.Property<string>("KVPCTId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

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

                    b.Property<string>("LoaiTien")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

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

                    b.Property<string>("SoCTGoc")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<decimal>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SoXe")
                        .HasMaxLength(8)
                        .HasColumnType("varchar(8)");

                    b.Property<string>("TKCo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("TKNo")
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

                    b.Property<decimal>("TyGia")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("VAT")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("KVPCTId");

                    b.ToTable("KVCTPCTs");
                });

            modelBuilder.Entity("Data.Models_KTTM.KVPCT", b =>
                {
                    b.Property<string>("SoCT")
                        .HasMaxLength(10)
                        .HasColumnType("varchar(10)");

                    b.Property<DateTime>("Create")
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

                    b.HasKey("SoCT");

                    b.ToTable("KVPCTs");
                });

            modelBuilder.Entity("Data.Models_KTTM.TamUng", b =>
                {
                    b.Property<long>("Id")
                        .HasColumnType("bigint");

                    b.Property<decimal>("ConLai")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("Conlaint")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("DienGiai")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("LoaiTien")
                        .HasMaxLength(3)
                        .HasColumnType("varchar(3)");

                    b.Property<string>("LogFile")
                        .HasColumnType("nvarchar(MAX)");

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

                    b.Property<decimal>("SoTien")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("SoTienNT")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("TKCo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<string>("TKNo")
                        .HasMaxLength(12)
                        .HasColumnType("varchar(12)");

                    b.Property<bool>("TTTP")
                        .HasColumnType("bit");

                    b.Property<decimal>("TyGia")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.ToTable("TamUngs");
                });

            modelBuilder.Entity("Data.Models_KTTM.KVCTPCT", b =>
                {
                    b.HasOne("Data.Models_KTTM.KVPCT", "KVPCT")
                        .WithMany()
                        .HasForeignKey("KVPCTId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KVPCT");
                });

            modelBuilder.Entity("Data.Models_KTTM.TamUng", b =>
                {
                    b.HasOne("Data.Models_KTTM.KVCTPCT", "KVCTPCT")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("KVCTPCT");
                });
#pragma warning restore 612, 618
        }
    }
}
