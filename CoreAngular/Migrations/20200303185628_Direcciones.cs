using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreAngular.Migrations
{
    public partial class Direcciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "angular");

            migrationBuilder.RenameTable(
                name: "Persona",
                newName: "Persona",
                newSchema: "angular");

            migrationBuilder.CreateTable(
                name: "Direccion",
                schema: "angular",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Calle = table.Column<string>(nullable: true),
                    Provincia = table.Column<string>(nullable: true),
                    PersonaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Direccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Direccion_Persona_PersonaId",
                        column: x => x.PersonaId,
                        principalSchema: "angular",
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Direccion_PersonaId",
                schema: "angular",
                table: "Direccion",
                column: "PersonaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Direccion",
                schema: "angular");

            migrationBuilder.RenameTable(
                name: "Persona",
                schema: "angular",
                newName: "Persona");
        }
    }
}
