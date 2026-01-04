using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureRequestProject.Migrations
{
    /// <inheritdoc />
    public partial class Removed_UserId_From_Vote_And_Comment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFeatureRequestComments_AbpUsers_UserId",
                table: "AppFeatureRequestComments");

            migrationBuilder.DropIndex(
                name: "IX_AppFeatureRequestVotes_FeatureRequestId_UserId",
                table: "AppFeatureRequestVotes");

            migrationBuilder.DropIndex(
                name: "IX_AppFeatureRequestComments_UserId",
                table: "AppFeatureRequestComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppFeatureRequestVotes");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppFeatureRequestComments");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppFeatureRequestComments");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppFeatureRequestComments");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppFeatureRequestComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AppFeatureRequestComments");

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestVotes_FeatureRequestId_CreatorId",
                table: "AppFeatureRequestVotes",
                columns: new[] { "FeatureRequestId", "CreatorId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppFeatureRequestVotes_FeatureRequestId_CreatorId",
                table: "AppFeatureRequestVotes");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppFeatureRequestVotes",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppFeatureRequestComments",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppFeatureRequestComments",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppFeatureRequestComments",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppFeatureRequestComments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AppFeatureRequestComments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestVotes_FeatureRequestId_UserId",
                table: "AppFeatureRequestVotes",
                columns: new[] { "FeatureRequestId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestComments_UserId",
                table: "AppFeatureRequestComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFeatureRequestComments_AbpUsers_UserId",
                table: "AppFeatureRequestComments",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
