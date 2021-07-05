using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class twoTbl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KVCLTGs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoCT = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    NgayCT = table.Column<DateTime>(type: "datetime", nullable: false),
                    DienGiai = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    TKNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    TKCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    SoTien = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MaKhNo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    MaKhCo = table.Column<string>(type: "varchar(12)", maxLength: 12, nullable: true),
                    NoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true),
                    CoQuay = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KVCLTGs", x => x.Id);
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
                    TyGia = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TonQuies", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KVCLTGs");

            migrationBuilder.DropTable(
                name: "TonQuies");
        }
    }
}
