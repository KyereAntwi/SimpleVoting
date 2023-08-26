using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SVoting.Presentation.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nominees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Fullname = table.Column<string>(type: "TEXT", nullable: false),
                    Photograph = table.Column<byte[]>(type: "BLOB", nullable: true),
                    Photomime = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nominees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollingCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Identifyer = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollingCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PollingSpaces",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Industry = table.Column<string>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollingSpaces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Polls",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Published = table.Column<bool>(type: "INTEGER", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PollingSpaceId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Polls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Polls_PollingSpaces_PollingSpaceId",
                        column: x => x.PollingSpaceId,
                        principalTable: "PollingSpaces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Codes",
                columns: table => new
                {
                    Identifier = table.Column<string>(type: "TEXT", nullable: false),
                    PollId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Codes", x => x.Identifier);
                    table.ForeignKey(
                        name: "FK_Codes_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PollCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PollId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PollCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PollCategories_PollingCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "PollingCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PollCategories_Polls_PollId",
                        column: x => x.PollId,
                        principalTable: "Polls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NomineeCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomineeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PollCategoryId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomineeCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NomineeCategories_Nominees_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "Nominees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NomineeCategories_PollCategories_PollCategoryId",
                        column: x => x.PollCategoryId,
                        principalTable: "PollCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    VotingCode = table.Column<string>(type: "TEXT", nullable: false),
                    PollCategoryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    NomineeId = table.Column<Guid>(type: "TEXT", nullable: false),
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Votes_Nominees_NomineeId",
                        column: x => x.NomineeId,
                        principalTable: "Nominees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Votes_PollCategories_PollCategoryId",
                        column: x => x.PollCategoryId,
                        principalTable: "PollCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Codes_PollId",
                table: "Codes",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_NomineeCategories_NomineeId",
                table: "NomineeCategories",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_NomineeCategories_PollCategoryId",
                table: "NomineeCategories",
                column: "PollCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PollCategories_CategoryId",
                table: "PollCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_PollCategories_PollId",
                table: "PollCategories",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_Polls_PollingSpaceId",
                table: "Polls",
                column: "PollingSpaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_NomineeId",
                table: "Votes",
                column: "NomineeId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_PollCategoryId",
                table: "Votes",
                column: "PollCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Codes");

            migrationBuilder.DropTable(
                name: "NomineeCategories");

            migrationBuilder.DropTable(
                name: "Votes");

            migrationBuilder.DropTable(
                name: "Nominees");

            migrationBuilder.DropTable(
                name: "PollCategories");

            migrationBuilder.DropTable(
                name: "PollingCategories");

            migrationBuilder.DropTable(
                name: "Polls");

            migrationBuilder.DropTable(
                name: "PollingSpaces");
        }
    }
}
