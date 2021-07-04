using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class tamUngTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TamUngs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    PhieuChi = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LoaiTien = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SoTienNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConLai = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Conlaint = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    Phong = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    TTTP = table.Column<bool>(type: "bit", nullable: false),
                    PhieuTT = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TamUngs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TamUngs");
        }
    }
}
