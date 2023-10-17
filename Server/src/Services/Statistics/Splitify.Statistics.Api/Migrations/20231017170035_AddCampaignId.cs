using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Splitify.Statistics.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCampaignId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CampaignId",
                table: "Links",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampaignId",
                table: "Links");
        }
    }
}
