using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removeTT621 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TT621s");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TT621s",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    BoPhan = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true),
                    CoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    DSKhongVAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DienGiaiP = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DieuChinh = table.Column<bool>(type: "bit", nullable: false),
                    GhiSo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    HTTC = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    HoaDonDT = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
                    KyHieu = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    KyHieuHD = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    LapPhieu = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LoaiHDGoc = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    MaKhCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    MatHang = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    MauSoHD = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    MsThue = table.Column<string>(type: "varchar(16)", maxLength: 16, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgayCTGoc = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    PhieuTC = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PhieuTU = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Sgtcode = table.Column<string>(type: "varchar(17)", maxLength: 17, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    SoCTGoc = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoXe = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: true),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TenKH = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VAT = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TT621s", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TT621s_TamUngs_Id",
                        column: x => x.Id,
                        principalTable: "TamUngs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
