using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RateWatch.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCurrencyTable_UpdatedAtToExchangeRate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExchangeRateRecords_Date_FromCurrency_ToCurrency",
                table: "ExchangeRateRecords");

            migrationBuilder.DropColumn(
                name: "FromCurrency",
                table: "ExchangeRateRecords");

            migrationBuilder.DropColumn(
                name: "ToCurrency",
                table: "ExchangeRateRecords");

            migrationBuilder.AddColumn<int>(
                name: "FromCurrencyId",
                table: "ExchangeRateRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToCurrencyId",
                table: "ExchangeRateRecords",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ExchangeRateRecords",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateRecords_Date_FromCurrencyId_ToCurrencyId",
                table: "ExchangeRateRecords",
                columns: new[] { "Date", "FromCurrencyId", "ToCurrencyId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateRecords_FromCurrencyId",
                table: "ExchangeRateRecords",
                column: "FromCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateRecords_ToCurrencyId",
                table: "ExchangeRateRecords",
                column: "ToCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Currencies_Code",
                table: "Currencies",
                column: "Code",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRateRecords_Currencies_FromCurrencyId",
                table: "ExchangeRateRecords",
                column: "FromCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRateRecords_Currencies_ToCurrencyId",
                table: "ExchangeRateRecords",
                column: "ToCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRateRecords_Currencies_FromCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRateRecords_Currencies_ToCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRateRecords_Date_FromCurrencyId_ToCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRateRecords_FromCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropIndex(
                name: "IX_ExchangeRateRecords_ToCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropColumn(
                name: "FromCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropColumn(
                name: "ToCurrencyId",
                table: "ExchangeRateRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ExchangeRateRecords");

            migrationBuilder.AddColumn<string>(
                name: "FromCurrency",
                table: "ExchangeRateRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ToCurrency",
                table: "ExchangeRateRecords",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_ExchangeRateRecords_Date_FromCurrency_ToCurrency",
                table: "ExchangeRateRecords",
                columns: new[] { "Date", "FromCurrency", "ToCurrency" },
                unique: true);
        }
    }
}
