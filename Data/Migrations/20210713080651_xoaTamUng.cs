using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class xoaTamUng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KVCLTGs");

            migrationBuilder.DropTable(
                name: "TamUngs");

            migrationBuilder.DropTable(
                name: "TonQuies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KVCLTGs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    MaKhCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    NoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVCLTGs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TamUngs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Conlaint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    LogFile = table.Column<string>(type: "nvarchar(MAX)", nullable: true),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    NgaySua = table.Column<DateTime>(type: "datetime", nullable: true),
                    NgayTao = table.Column<DateTime>(type: "datetime", nullable: true),
                    NguoiSua = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    NguoiTao = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    PhieuChi = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    PhieuTT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true),
                    Phong = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TTTP = table.Column<bool>(type: "bit", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamUngs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TonQuies",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonQuies", x => x.Id);
                });
        }
    }
}
