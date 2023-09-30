using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Campaign.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Campaigns",
                newName: "IsRunning");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRunning",
                table: "Campaigns",
                newName: "IsActive");
        }
    }
}
