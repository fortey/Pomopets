using Microsoft.EntityFrameworkCore.Migrations;

namespace Pomidor.Migrations
{
    public partial class z : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Img",
                table: "TypeOfPets",
                newName: "ImgFolder");

            migrationBuilder.AddColumn<int>(
                name: "Price",
                table: "TypeOfPets",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "TypeOfPets");

            migrationBuilder.RenameColumn(
                name: "ImgFolder",
                table: "TypeOfPets",
                newName: "Img");
        }
    }
}
