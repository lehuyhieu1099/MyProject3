using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartPhoneShop.Migrations
{
    public partial class AlterBrandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Brands",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Brands");
        }
    }
}
