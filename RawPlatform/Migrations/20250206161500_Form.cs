using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RawPlatform.Migrations
{
    /// <inheritdoc />
    public partial class Form : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FormDetails");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "MarketingUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "MarketingUsers");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "MarketingUsers",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "MarketingUsers",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "MarketingUsers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "MarketingUsers");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "MarketingUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "MarketingUsers",
                type: "character varying(30)",
                maxLength: 30,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FormDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MarketingUserId = table.Column<int>(type: "integer", nullable: false),
                    Message = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    RowVersion = table.Column<int>(type: "integer", nullable: false),
                    SavedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FormDetails_MarketingUsers_MarketingUserId",
                        column: x => x.MarketingUserId,
                        principalTable: "MarketingUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormDetails_MarketingUserId",
                table: "FormDetails",
                column: "MarketingUserId");
        }
    }
}
