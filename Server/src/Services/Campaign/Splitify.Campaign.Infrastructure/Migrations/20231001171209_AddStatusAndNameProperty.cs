using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Campaign.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusAndNameProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRunning",
                table: "Campaigns");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Campaigns",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Campaigns",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Campaigns");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Campaigns");

            migrationBuilder.AddColumn<bool>(
                name: "IsRunning",
                table: "Campaigns",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
