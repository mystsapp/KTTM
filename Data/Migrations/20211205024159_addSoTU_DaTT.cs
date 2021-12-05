using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addSoTU_DaTT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoTU_DaTT",
                table: "KVCTPTCs",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoTU_DaTT",
                table: "KVCTPTCs");
        }
    }
}
