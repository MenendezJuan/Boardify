using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardify.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddReporterToCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReporterId",
                table: "Cards",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cards_ReporterId",
                table: "Cards",
                column: "ReporterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Users_ReporterId",
                table: "Cards",
                column: "ReporterId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cards_Users_ReporterId",
                table: "Cards");

            migrationBuilder.DropIndex(
                name: "IX_Cards_ReporterId",
                table: "Cards");

            migrationBuilder.DropColumn(
                name: "ReporterId",
                table: "Cards");
        }
    }
}