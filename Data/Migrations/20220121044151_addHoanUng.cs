using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addHoanUng : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HoanUng",
                table: "KVCTPTCs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoanUng",
                table: "KVCTPTCs");
        }
    }
}
