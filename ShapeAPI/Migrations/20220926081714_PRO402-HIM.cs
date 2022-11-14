using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ShapeAPI.Migrations
{
    public partial class PRO402HIM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    X = table.Column<double>(type: "double precision", nullable: false),
                    Y = table.Column<double>(type: "double precision", nullable: false),
                    Z = table.Column<double>(type: "double precision", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ShapesGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GroupName = table.Column<string>(type: "text", nullable: false),
                    Owner = table.Column<string>(type: "text", nullable: false),
                    GroupPositionID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShapesGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShapesGroups_Positions_GroupPositionID",
                        column: x => x.GroupPositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shapes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ShapePositionID = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_Shapes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shapes_Positions_ShapePositionID",
                        column: x => x.ShapePositionID,
                        principalTable: "Positions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Shapes_ShapesGroups_ShapeGroupId",
                        column: x => x.ShapeGroupId,
                        principalTable: "ShapesGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shapes_ShapeGroupId",
                table: "Shapes",
                column: "ShapeGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Shapes_ShapePositionID",
                table: "Shapes",
                column: "ShapePositionID");

            migrationBuilder.CreateIndex(
                name: "IX_ShapesGroups_GroupPositionID",
                table: "ShapesGroups",
                column: "GroupPositionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shapes");

            migrationBuilder.DropTable(
                name: "ShapesGroups");

            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
