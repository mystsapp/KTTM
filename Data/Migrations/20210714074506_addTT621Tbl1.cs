using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addTT621Tbl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TT621s",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    PhieuTC = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PhieuTU = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
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
                        name: "FK_TT621s_KVCTPCTs_Id",
                        column: x => x.Id,
                        principalTable: "KVCTPCTs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TT621s");
        }
    }
}
