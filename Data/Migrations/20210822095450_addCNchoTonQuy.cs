using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addCNchoTonQuy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "DieuChinh",
                table: "TT621s",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaCn",
                table: "TonQuies",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaCn",
                table: "TonQuies");

            migrationBuilder.AlterColumn<bool>(
                name: "DieuChinh",
                table: "TT621s",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
