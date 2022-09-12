using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShapeAPI.Migrations
{
    public partial class PRO4028 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShapesGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShapesGroups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BaseShape",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Discriminator = table.Column<string>(type: "text", nullable: false),
                    ShapeGroupId = table.Column<int>(type: "integer", nullable: true),
                    Diameter = table.Column<double>(type: "double precision", nullable: true),
                    Lenght = table.Column<double>(type: "double precision", nullable: true),
                    Width = table.Column<double>(type: "double precision", nullable: true),
                    BaseLenght = table.Column<double>(type: "double precision", nullable: true),
                    SideOne = table.Column<double>(type: "double precision", nullable: true),
                    SideTwo = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseShape", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseShape_ShapesGroups_ShapeGroupId",
                        column: x => x.ShapeGroupId,
                        principalTable: "ShapesGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseShape_ShapeGroupId",
                table: "BaseShape",
                column: "ShapeGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseShape");

            migrationBuilder.DropTable(
                name: "ShapesGroups");
        }
    }
}
