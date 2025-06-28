using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSecondaryMuscles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseMuscle");

            migrationBuilder.AddColumn<long>(
                name: "MuscleId",
                table: "Exercise",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Exercise_MuscleId",
                table: "Exercise",
                column: "MuscleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercise_Muscles_MuscleId",
                table: "Exercise",
                column: "MuscleId",
                principalTable: "Muscles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercise_Muscles_MuscleId",
                table: "Exercise");

            migrationBuilder.DropIndex(
                name: "IX_Exercise_MuscleId",
                table: "Exercise");

            migrationBuilder.DropColumn(
                name: "MuscleId",
                table: "Exercise");

            migrationBuilder.CreateTable(
                name: "ExerciseMuscle",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExerciseId = table.Column<long>(type: "bigint", nullable: false),
                    MuscleId = table.Column<long>(type: "bigint", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseMuscle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseMuscle_Exercise_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExerciseMuscle_Muscles_MuscleId",
                        column: x => x.MuscleId,
                        principalTable: "Muscles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMuscle_ExerciseId_MuscleId",
                table: "ExerciseMuscle",
                columns: new[] { "ExerciseId", "MuscleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseMuscle_MuscleId",
                table: "ExerciseMuscle",
                column: "MuscleId");
        }
    }
}
