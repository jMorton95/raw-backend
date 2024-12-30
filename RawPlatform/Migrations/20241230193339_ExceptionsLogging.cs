using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RawPlatform.Migrations
{
    /// <inheritdoc />
    public partial class ExceptionsLogging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Exception",
                table: "LogEntries",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exception",
                table: "LogEntries");
        }
    }
}
