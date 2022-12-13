using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacroMinder.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserObjective : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ObjectiveId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ObjectiveId",
                table: "AspNetUsers",
                column: "ObjectiveId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_MacroNutriment_ObjectiveId",
                table: "AspNetUsers",
                column: "ObjectiveId",
                principalTable: "MacroNutriment",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_MacroNutriment_ObjectiveId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ObjectiveId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ObjectiveId",
                table: "AspNetUsers");
        }
    }
}
