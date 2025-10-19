using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackPro.Migrations
{
    /// <inheritdoc />
    public partial class AddBodyMeasurements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Weight = table.Column<float>(type: "REAL", nullable: false),
                    BodyFat = table.Column<float>(type: "REAL", nullable: true),
                    MuscleMass = table.Column<float>(type: "REAL", nullable: true),
                    Chest = table.Column<float>(type: "REAL", nullable: true),
                    Arms = table.Column<float>(type: "REAL", nullable: true),
                    Waist = table.Column<float>(type: "REAL", nullable: true),
                    Legs = table.Column<float>(type: "REAL", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyMeasurements", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyMeasurements");
        }
    }
}
