using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KVPCTs",
                columns: table => new
                {
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    MFieu = table.Column<string>(type: "varchar(1)", maxLength: 1, nullable: true),
                    NgoaiTe = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DonVi = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Phong = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LapPhieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Create = table.Column<DateTime>(type: "datetime", nullable: false),
                    MayTinh = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Lock = table.Column<DateTime>(type: "datetime", nullable: false),
                    Locker = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVPCTs", x => x.SoCT);
                });

            migrationBuilder.CreateTable(
                name: "KVCTPCTs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVPCTId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    HTTC = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Sgtcode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    MaKhCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaKh = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    KhoangMuc = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    HTTT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CardNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    SalesSlip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SoXe = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    MsThue = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    LoaiHDGoc = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoCTGoc = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NgayCTGoc = table.Column<DateTime>(type: "datetime", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSKhongVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoPhan = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    STT = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    NoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    CoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    TenKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MatHang = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    KyHieu = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MauSoHD = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    DieuChinh = table.Column<bool>(type: "bit", nullable: false),
                    KC141 = table.Column<DateTime>(type: "datetime", nullable: false),
                    TamUng = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DienGiaiP = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HoaDonDT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVCTPCTs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KVCTPCTs_KVPCTs_KVPCTId",
                        column: x => x.KVPCTId,
                        principalTable: "KVPCTs",
                        principalColumn: "SoCT",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KVCTPCTs_KVPCTId",
                table: "KVCTPCTs",
                column: "KVPCTId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KVCTPCTs");

            migrationBuilder.DropTable(
                name: "KVPCTs");
        }
    }
}
