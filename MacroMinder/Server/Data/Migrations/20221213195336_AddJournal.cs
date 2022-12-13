using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MacroMinder.Server.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddJournal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BreakfastFoods",
                table: "Food",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DinnerFoods",
                table: "Food",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LunchFood",
                table: "Food",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SnackFood",
                table: "Food",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MacroDailyReport",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroDailyReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MacroDailyReport_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Food_BreakfastFoods",
                table: "Food",
                column: "BreakfastFoods");

            migrationBuilder.CreateIndex(
                name: "IX_Food_DinnerFoods",
                table: "Food",
                column: "DinnerFoods");

            migrationBuilder.CreateIndex(
                name: "IX_Food_LunchFood",
                table: "Food",
                column: "LunchFood");

            migrationBuilder.CreateIndex(
                name: "IX_Food_SnackFood",
                table: "Food",
                column: "SnackFood");

            migrationBuilder.CreateIndex(
                name: "IX_MacroDailyReport_UserId",
                table: "MacroDailyReport",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_MacroDailyReport_BreakfastFoods",
                table: "Food",
                column: "BreakfastFoods",
                principalTable: "MacroDailyReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_MacroDailyReport_DinnerFoods",
                table: "Food",
                column: "DinnerFoods",
                principalTable: "MacroDailyReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_MacroDailyReport_LunchFood",
                table: "Food",
                column: "LunchFood",
                principalTable: "MacroDailyReport",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Food_MacroDailyReport_SnackFood",
                table: "Food",
                column: "SnackFood",
                principalTable: "MacroDailyReport",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Food_MacroDailyReport_BreakfastFoods",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_MacroDailyReport_DinnerFoods",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_MacroDailyReport_LunchFood",
                table: "Food");

            migrationBuilder.DropForeignKey(
                name: "FK_Food_MacroDailyReport_SnackFood",
                table: "Food");

            migrationBuilder.DropTable(
                name: "MacroDailyReport");

            migrationBuilder.DropIndex(
                name: "IX_Food_BreakfastFoods",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_DinnerFoods",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_LunchFood",
                table: "Food");

            migrationBuilder.DropIndex(
                name: "IX_Food_SnackFood",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "BreakfastFoods",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "DinnerFoods",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "LunchFood",
                table: "Food");

            migrationBuilder.DropColumn(
                name: "SnackFood",
                table: "Food");
        }
    }
}
