using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureRequestProject.Migrations
{
    /// <inheritdoc />
    public partial class Created_Entity_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppFeatureRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    VoteCount = table.Column<int>(type: "integer", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeatureRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppFeatureRequestComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FeatureRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeatureRequestComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFeatureRequestComments_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFeatureRequestComments_AppFeatureRequests_FeatureRequest~",
                        column: x => x.FeatureRequestId,
                        principalTable: "AppFeatureRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppFeatureRequestVotes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    VoteType = table.Column<int>(type: "integer", nullable: false),
                    FeatureRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExtraProperties = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "character varying(40)", maxLength: 40, nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFeatureRequestVotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFeatureRequestVotes_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppFeatureRequestVotes_AppFeatureRequests_FeatureRequestId",
                        column: x => x.FeatureRequestId,
                        principalTable: "AppFeatureRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestComments_FeatureRequestId",
                table: "AppFeatureRequestComments",
                column: "FeatureRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestComments_UserId",
                table: "AppFeatureRequestComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestVotes_FeatureRequestId_UserId",
                table: "AppFeatureRequestVotes",
                columns: new[] { "FeatureRequestId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppFeatureRequestVotes_UserId",
                table: "AppFeatureRequestVotes",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppFeatureRequestComments");

            migrationBuilder.DropTable(
                name: "AppFeatureRequestVotes");

            migrationBuilder.DropTable(
                name: "AppFeatureRequests");
        }
    }
}
