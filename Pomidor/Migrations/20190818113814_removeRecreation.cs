using Microsoft.EntityFrameworkCore.Migrations;

namespace Pomidor.Migrations
{
    public partial class removeRecreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Pomidors");

            migrationBuilder.UpdateData(
                table: "TypeOfPets",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ImgFolder", "Price" },
                values: new object[] { "Cat", 50 });

            migrationBuilder.InsertData(
                table: "TypeOfPets",
                columns: new[] { "ID", "ImgFolder", "Name", "Price" },
                values: new object[] { 2, "Hog", "Hog", 100 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeOfPets",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Pomidors",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "TypeOfPets",
                keyColumn: "ID",
                keyValue: 1,
                columns: new[] { "ImgFolder", "Price" },
                values: new object[] { null, 0 });
        }
    }
}
