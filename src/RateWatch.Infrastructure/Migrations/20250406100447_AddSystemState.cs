using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RateWatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemStates",
                columns: table => new
                {
                    Key = table.Column<string>(type: "text", nullable: false),
                    IsSet = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemStates", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemStates");
        }
    }
}
