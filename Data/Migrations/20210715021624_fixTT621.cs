using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class fixTT621 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TT621s_KVCTPCTs_Id",
                table: "TT621s");

            migrationBuilder.AddForeignKey(
                name: "FK_TT621s_TamUngs_Id",
                table: "TT621s",
                column: "Id",
                principalTable: "TamUngs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TT621s_TamUngs_Id",
                table: "TT621s");

            migrationBuilder.AddForeignKey(
                name: "FK_TT621s_KVCTPCTs_Id",
                table: "TT621s",
                column: "Id",
                principalTable: "KVCTPCTs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
