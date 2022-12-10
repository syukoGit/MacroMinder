using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacroMinder.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateFood : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lipids = table.Column<double>(type: "float", nullable: false),
                    Portion = table.Column<double>(type: "float", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Food_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_UserId",
                table: "Food",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food");
        }
    }
}
