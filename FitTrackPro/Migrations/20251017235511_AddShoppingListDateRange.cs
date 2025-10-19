using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackPro.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingListDateRange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "rangeEndDate",
                table: "shoppingListItems",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "rangeStartDate",
                table: "shoppingListItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "rangeEndDate",
                table: "shoppingListItems");

            migrationBuilder.DropColumn(
                name: "rangeStartDate",
                table: "shoppingListItems");
        }
    }
}
