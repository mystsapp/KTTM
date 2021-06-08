using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addSua : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "NgaySua",
                table: "KVPCTs",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NguoiSua",
                table: "KVPCTs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NgaySua",
                table: "KVPCTs");

            migrationBuilder.DropColumn(
                name: "NguoiSua",
                table: "KVPCTs");
        }
    }
}
