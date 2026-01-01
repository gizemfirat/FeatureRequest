using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureRequestProject.Migrations
{
    /// <inheritdoc />
    public partial class Edited_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFeatureRequestVotes_AbpUsers_UserId",
                table: "AppFeatureRequestVotes");

            migrationBuilder.DropIndex(
                name: "IX_AppFeatureRequestVotes_UserId",
                table: "AppFeatureRequestVotes");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "AppFeatureRequestVotes");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "AppFeatureRequestVotes");

            migrationBuilder.RenameColumn(
                name: "VoteType",
                table: "AppFeatureRequestVotes",
                newName: "Value");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppFeatureRequests",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppFeatureRequests",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppFeatureRequests",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppFeatureRequests");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppFeatureRequests");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppFeatureRequests");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AppFeatureRequestVotes",
                newName: "VoteType");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "AppFeatureRequestVotes",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "AppFeatureRequestVotes",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestVotes_UserId",
                table: "AppFeatureRequestVotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFeatureRequestVotes_AbpUsers_UserId",
                table: "AppFeatureRequestVotes",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
