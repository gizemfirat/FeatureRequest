using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureRequestProject.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Feature_Request_Vote_Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppFeatureRequestVotes");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppFeatureRequestVotes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppFeatureRequestVotes",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppFeatureRequestVotes",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
