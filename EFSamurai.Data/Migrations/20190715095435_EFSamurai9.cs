using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSamurai.Data.Migrations
{
    public partial class EFSamurai9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Samurais_SamuraiID",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "SamuraiID",
                table: "Quotes",
                newName: "SamuraiId");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_SamuraiID",
                table: "Quotes",
                newName: "IX_Quotes_SamuraiId");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Samurais_SamuraiId",
                table: "Quotes",
                column: "SamuraiId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Quotes_Samurais_SamuraiId",
                table: "Quotes");

            migrationBuilder.RenameColumn(
                name: "SamuraiId",
                table: "Quotes",
                newName: "SamuraiID");

            migrationBuilder.RenameIndex(
                name: "IX_Quotes_SamuraiId",
                table: "Quotes",
                newName: "IX_Quotes_SamuraiID");

            migrationBuilder.AddForeignKey(
                name: "FK_Quotes_Samurais_SamuraiID",
                table: "Quotes",
                column: "SamuraiID",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
