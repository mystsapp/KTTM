using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixMaKhNo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaKhNo",
                table: "TamUngs",
                type: "varchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(12)",
                oldMaxLength: 12,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MaKhNo",
                table: "TamUngs",
                type: "varchar(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(25)",
                oldMaxLength: 25,
                oldNullable: true);
        }
    }
}
