using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class replaceSoVe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SoVe",
                table: "KVCTPTCs",
                newName: "Number");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Number",
                table: "KVCTPTCs",
                newName: "SoVe");
        }
    }
}
