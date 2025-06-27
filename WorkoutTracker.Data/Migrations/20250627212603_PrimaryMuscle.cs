using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class PrimaryMuscle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PrimaryMuscleId",
                table: "Exercise",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_PrimaryMuscleId",
                table: "Exercise",
                column: "PrimaryMuscleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Muscles_PrimaryMuscleId",
                table: "Exercise",
                column: "PrimaryMuscleId",
                principalTable: "Muscles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Muscles_PrimaryMuscleId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_PrimaryMuscleId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "PrimaryMuscleId",
                table: "Exercise");
        }
    }
}
