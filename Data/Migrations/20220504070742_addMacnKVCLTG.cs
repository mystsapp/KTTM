using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addMacnKVCLTG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MaCn",
                table: "KVCLTGs",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaCn",
                table: "KVCLTGs");
        }
    }
}
