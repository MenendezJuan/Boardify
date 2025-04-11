using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Boardify.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AñadirPropertyPosicionCards : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Cards");
        }
    }
}