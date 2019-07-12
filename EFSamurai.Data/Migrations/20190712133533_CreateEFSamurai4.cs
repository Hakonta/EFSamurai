using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSamurai.Data.Migrations
{
    public partial class CreateEFSamurai4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Hair",
                table: "Samurais",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hair",
                table: "Samurais");
        }
    }
}
