using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitTrackPro.Migrations
{
    /// <inheritdoc />
    public partial class AddCalorieTracking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalCaloriesBurned",
                table: "WorkoutSessions",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CaloriesBurnedPerMinute",
                table: "Exercises",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CaloriesBurnedPerRep",
                table: "Exercises",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCaloriesBurned",
                table: "WorkoutSessions");

            migrationBuilder.DropColumn(
                name: "CaloriesBurnedPerMinute",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "CaloriesBurnedPerRep",
                table: "Exercises");
        }
    }
}
