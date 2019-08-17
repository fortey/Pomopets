using Microsoft.EntityFrameworkCore.Migrations;

namespace Pomidor.Migrations
{
    public partial class pp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PrizeIssued",
                table: "Pomidors",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PrizeIssued",
                table: "Pomidors");
        }
    }
}
