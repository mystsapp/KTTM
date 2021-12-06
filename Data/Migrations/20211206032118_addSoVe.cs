using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addSoVe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoVe",
                table: "TT621s",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SoVe",
                table: "KVCTPTCs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoVe",
                table: "TT621s");

            migrationBuilder.DropColumn(
                name: "SoVe",
                table: "KVCTPTCs");
        }
    }
}
