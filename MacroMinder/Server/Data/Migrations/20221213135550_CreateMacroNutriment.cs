using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacroMinder.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateMacroNutriment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Carbohydrates",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Lipids",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "Proteins",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "Calories",
                table: "Food",
                newName: "MacroNutrimentId");

            migrationBuilder.CreateTable(
                name: "MacroNutriment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false),
                    Lipids = table.Column<double>(type: "float", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroNutriment", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_MacroNutrimentId",
                table: "Food",
                column: "MacroNutrimentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_MacroNutriment_MacroNutrimentId",
                table: "Food",
                column: "MacroNutrimentId",
                principalTable: "MacroNutriment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_MacroNutriment_MacroNutrimentId",
                table: "Food");

            migrationBuilder.DropTable(
                name: "MacroNutriment");

            migrationBuilder.DropIndex(
                name: "IX_Food_MacroNutrimentId",
                table: "Food");

            migrationBuilder.RenameColumn(
                name: "MacroNutrimentId",
                table: "Food",
                newName: "Calories");

            migrationBuilder.AddColumn<double>(
                name: "Carbohydrates",
                table: "Food",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Lipids",
                table: "Food",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Proteins",
                table: "Food",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
