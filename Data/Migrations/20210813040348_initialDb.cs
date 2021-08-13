using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initialDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KVPTCs",
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
                    Lock = table.Column<DateTime>(type: "datetime", nullable: true),
                    Locker = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVPTCs", x => x.SoCT);
                });

            migrationBuilder.CreateTable(
                name: "TonQuies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NguoiTao = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonQuies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KVCTPTCs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVPTCId = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    HTTC = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    Sgtcode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    MaKhCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    MaKh = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true),
                    KhoangMuc = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    HTTT = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true),
                    CardNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true),
                    SalesSlip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SoXe = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    MsThue = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    LoaiHDGoc = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoCTGoc = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NgayCTGoc = table.Column<DateTime>(type: "datetime", nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DSKhongVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BoPhan = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    STT = table.Column<string>(type: "varchar(13)", maxLength: 13, nullable: true),
                    NoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    CoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    TenKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MatHang = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    KyHieu = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MauSoHD = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    DieuChinh = table.Column<bool>(type: "bit", nullable: true),
                    KC141 = table.Column<DateTime>(type: "datetime", nullable: true),
                    TamUng = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DienGiaiP = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HoaDonDT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    LinkHDDT = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    MaTraCuu = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    TkTruyCap = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVCTPTCs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_KVCTPTCs_KVPTCs_KVPTCId",
                        column: x => x.KVPTCId,
                        principalTable: "KVPTCs",
                        principalColumn: "SoCT",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TamUngs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KVCTPTCId = table.Column<long>(type: "bigint", nullable: true),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    PhieuChi = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ConLaiNT = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Phong = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TTTP = table.Column<bool>(type: "bit", nullable: true),
                    PhieuTT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamUngs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TamUngs_KVCTPTCs_KVCTPTCId",
                        column: x => x.KVCTPTCId,
                        principalTable: "KVCTPTCs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TT621s",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TamUngId = table.Column<long>(type: "bigint", nullable: false),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: true),
                    PhieuTC = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PhieuTU = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: false),
                    MaKhCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Sgtcode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    HTTC = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    GhiSo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    MsThue = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    NgayCTGoc = table.Column<DateTime>(type: "datetime", nullable: true),
                    LoaiHDGoc = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoCTGoc = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    KyHieuHD = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DSKhongVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BoPhan = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    NoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    CoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoXe = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    TenKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    KyHieu = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    MauSoHD = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    MatHang = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    DieuChinh = table.Column<bool>(type: "bit", nullable: false),
                    LapPhieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DienGiaiP = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    HoaDonDT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TT621s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TT621s_TamUngs_TamUngId",
                        column: x => x.TamUngId,
                        principalTable: "TamUngs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KVCTPTCs_KVPTCId",
                table: "KVCTPTCs",
                column: "KVPTCId");

            migrationBuilder.CreateIndex(
                name: "IX_TamUngs_KVCTPTCId",
                table: "TamUngs",
                column: "KVCTPTCId");

            migrationBuilder.CreateIndex(
                name: "IX_TT621s_TamUngId",
                table: "TT621s",
                column: "TamUngId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TonQuies");

            migrationBuilder.DropTable(
                name: "TT621s");

            migrationBuilder.DropTable(
                name: "TamUngs");

            migrationBuilder.DropTable(
                name: "KVCTPTCs");

            migrationBuilder.DropTable(
                name: "KVPTCs");
        }
    }
}
