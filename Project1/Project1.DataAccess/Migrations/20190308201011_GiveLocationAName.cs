using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.DataAccess.Migrations
{
    public partial class GiveLocationAName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Project1",
                table: "Location",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "UQ__Location__2HzBmwvgSOEaXvTL",
                schema: "Project1",
                table: "Location",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UQ__Location__2HzBmwvgSOEaXvTL",
                schema: "Project1",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Project1",
                table: "Location");
        }
    }
}
