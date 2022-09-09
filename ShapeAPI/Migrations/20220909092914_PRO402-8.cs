using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShapeAPI.Migrations
{
    public partial class PRO4028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Shapes");

            migrationBuilder.DropColumn(
                name: "ShapeType",
                table: "Shapes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Shapes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ShapeType",
                table: "Shapes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
