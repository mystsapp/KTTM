using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addSuaKVCTPCT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogFile",
                table: "KVCTPCTs",
                type: "nvarchar(MAX)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySua",
                table: "KVCTPCTs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "NgayTao",
                table: "KVCTPCTs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiSua",
                table: "KVCTPCTs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiTao",
                table: "KVCTPCTs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogFile",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "NgaySua",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "NgayTao",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "NguoiSua",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "NguoiTao",
                table: "KVCTPCTs");
        }
    }
}
