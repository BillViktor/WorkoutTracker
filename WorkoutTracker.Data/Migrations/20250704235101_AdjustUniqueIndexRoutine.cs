using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdjustUniqueIndexRoutine : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoutineDays_Id_SortOrder",
                table: "RoutineDays");

            migrationBuilder.DropIndex(
                name: "IX_RoutineDays_RoutineId",
                table: "RoutineDays");

            migrationBuilder.DropIndex(
                name: "IX_RoutineDayExercises_Id_SortOrder",
                table: "RoutineDayExercises");

            migrationBuilder.DropIndex(
                name: "IX_RoutineDayExercises_WorkoutRoutineDayId",
                table: "RoutineDayExercises");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDays_RoutineId_SortOrder",
                table: "RoutineDays",
                columns: new[] { "RoutineId", "SortOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDayExercises_WorkoutRoutineDayId_SortOrder",
                table: "RoutineDayExercises",
                columns: new[] { "WorkoutRoutineDayId", "SortOrder" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RoutineDays_RoutineId_SortOrder",
                table: "RoutineDays");

            migrationBuilder.DropIndex(
                name: "IX_RoutineDayExercises_WorkoutRoutineDayId_SortOrder",
                table: "RoutineDayExercises");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDays_Id_SortOrder",
                table: "RoutineDays",
                columns: new[] { "Id", "SortOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDays_RoutineId",
                table: "RoutineDays",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDayExercises_Id_SortOrder",
                table: "RoutineDayExercises",
                columns: new[] { "Id", "SortOrder" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoutineDayExercises_WorkoutRoutineDayId",
                table: "RoutineDayExercises",
                column: "WorkoutRoutineDayId");
        }
    }
}
