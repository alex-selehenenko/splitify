using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Statistics.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Links",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Links");
        }
    }
}
