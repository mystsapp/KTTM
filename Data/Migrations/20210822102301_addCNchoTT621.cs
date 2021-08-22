using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addCNchoTT621 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaCn",
                table: "TT621s",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaCn",
                table: "TT621s");
        }
    }
}
