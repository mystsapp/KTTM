using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class tamUngTbl1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "TamUngs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySua",
                table: "TamUngs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "TamUngs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiSua",
                table: "TamUngs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "TamUngs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "TamUngs");

            migrationBuilder.DropColumn(
                name: "NgaySua",
                table: "TamUngs");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "TamUngs");

            migrationBuilder.DropColumn(
                name: "NguoiSua",
                table: "TamUngs");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "TamUngs");
        }
    }
}
