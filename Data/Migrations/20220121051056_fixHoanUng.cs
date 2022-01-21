using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixHoanUng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoanUng",
                table: "KVCTPTCs");

            migrationBuilder.AddColumn<string>(
                name: "HoanUngTU",
                table: "KVCTPTCs",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoanUngTU",
                table: "KVCTPTCs");

            migrationBuilder.AddColumn<bool>(
                name: "HoanUng",
                table: "KVCTPTCs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
