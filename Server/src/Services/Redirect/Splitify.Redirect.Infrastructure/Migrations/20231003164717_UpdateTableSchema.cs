using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Redirect.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTableSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Redirects_RedirectId",
                table: "Destinations");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Redirects_RedirectId",
                table: "Destinations",
                column: "RedirectId",
                principalTable: "Redirects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Redirects_RedirectId",
                table: "Destinations");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Redirects_RedirectId",
                table: "Destinations",
                column: "RedirectId",
                principalTable: "Redirects",
                principalColumn: "Id");
        }
    }
}
