using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ThemOHDDTtrongTT141 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LinkHDDT",
                table: "TT621s",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTraCuu",
                table: "TT621s",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "TT621s",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TkTruyCap",
                table: "TT621s",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkHDDT",
                table: "TT621s");

            migrationBuilder.DropColumn(
                name: "MaTraCuu",
                table: "TT621s");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "TT621s");

            migrationBuilder.DropColumn(
                name: "TkTruyCap",
                table: "TT621s");
        }
    }
}
