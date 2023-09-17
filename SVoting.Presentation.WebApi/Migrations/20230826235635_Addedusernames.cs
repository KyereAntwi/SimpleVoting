using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVoting.Presentation.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Addedusernames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "PollingCategories",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Nominees",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "PollingCategories");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Nominees");
        }
    }
}
