using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addSoTT_DaTao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoTT_DaTao",
                table: "KVCTPTCs",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoTT_DaTao",
                table: "KVCTPTCs");
        }
    }
}
