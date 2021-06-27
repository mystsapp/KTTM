using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class addMoreKvctpct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KVCTPCTs_KVPCTs_KVPCTId",
                table: "KVCTPCTs");

            migrationBuilder.AlterColumn<string>(
                name: "KVPCTId",
                table: "KVCTPCTs",
                type: "varchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkHDDT",
                table: "KVCTPCTs",
                type: "varchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaTraCuu",
                table: "KVCTPCTs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "KVCTPCTs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TkTruyCap",
                table: "KVCTPCTs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_KVCTPCTs_KVPCTs_KVPCTId",
                table: "KVCTPCTs",
                column: "KVPCTId",
                principalTable: "KVPCTs",
                principalColumn: "SoCT",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KVCTPCTs_KVPCTs_KVPCTId",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "LinkHDDT",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "MaTraCuu",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "KVCTPCTs");

            migrationBuilder.DropColumn(
                name: "TkTruyCap",
                table: "KVCTPCTs");

            migrationBuilder.AlterColumn<string>(
                name: "KVPCTId",
                table: "KVCTPCTs",
                type: "varchar(10)",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddForeignKey(
                name: "FK_KVCTPCTs_KVPCTs_KVPCTId",
                table: "KVCTPCTs",
                column: "KVPCTId",
                principalTable: "KVPCTs",
                principalColumn: "SoCT",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
