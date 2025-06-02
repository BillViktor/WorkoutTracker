using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkoutTracker.Data.Migrations
{
    /// <inheritdoc />
    public partial class MusclesData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            INSERT INTO [Muscles]
            ([Name], [Description], [ImageUrl])
            VALUES
('Triceps', 'Muscles on the back of the upper arm responsible for extending the elbow and straightening the arm.', 'images/muscles/triceps.jpeg'),
('Chest', 'Primarily the pectoral muscles, used for pushing movements and bringing the arms together across the body.', 'images/muscles/chest.jpeg'),
('Shoulders', 'Includes the deltoid muscles, which help lift and rotate the arms in various directions.', 'images/muscles/shoulders.jpeg'),
('Biceps', 'Front of the upper arm, responsible for bending the elbow and rotating the forearm.', 'images/muscles/biceps.jpeg'),
('Core', 'Muscles around the trunk and pelvis, including abs and obliques, essential for stability, balance, and posture.', 'images/muscles/core.jpeg'),
('Back', 'Covers upper and lower back muscles like the lats, traps, and erector spinae, used for pulling and posture.', 'images/muscles/back.jpeg'),
('Forearms', 'Muscles of the lower arm that control grip strength and wrist movements.', 'images/muscles/forearms.jpeg'),
('Upper Legs', 'Includes quads and hamstrings, vital for squatting, running, and jumping.', 'images/muscles/upperlegs.jpeg'),
('Glutes', 'The muscles of the buttocks, important for hip movement, posture, and lower body strength.', 'images/muscles/glutes.jpeg'),
('Cardio', 'Not a muscle group, but refers to exercises that raise heart rate and improve cardiovascular health.', 'images/muscles/cardio.jpeg'),
('Lower Legs', 'Includes calves and muscles around the ankle, important for walking, running, and jumping.', 'images/muscles/lowerlegs.jpeg')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Muscles];");
        }
    }
}
