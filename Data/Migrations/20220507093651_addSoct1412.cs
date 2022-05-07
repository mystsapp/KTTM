using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addSoct1412 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SoCT1412",
                table: "KVCLTGs",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SoCT1412",
                table: "KVCLTGs");
        }
    }
}
