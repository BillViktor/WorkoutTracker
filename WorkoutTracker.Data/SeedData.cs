using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;

namespace WorkoutTracker.Data
{
    public static class SeedData
    {
        public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<WorkoutTrackerUserModel>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            //Create roles if they don't exist
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Create dummy user
            string userEmail = "user@dummy.com";
            string userName = "DummyUser";
            string userPassword = "Dummy@123";

            if (await userManager.FindByNameAsync(userName) == null)
            {
                var user = new WorkoutTrackerUserModel
                {
                    UserName = userName,
                    Email = userEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(user, userPassword);
            }

            //Create dummy admin
            string adminEmail = "admin@workout.com";
            string adminName = "DummyAdmin";
            string adminPassword = "Dummy@123";

            if (await userManager.FindByNameAsync(adminName) == null)
            {
                var user = new WorkoutTrackerUserModel
                {
                    UserName = adminName,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        /// <summary>
        /// Seeds the database with initial muscle data if it doesn't already exist.
        /// </summary>
        public static async Task SeedMusclesAsync(IServiceProvider serviceProvider)
        {
            var workoutTrackerRepository = serviceProvider.GetRequiredService<IWorkoutTrackerRepository>();

            // Check if muscles already exist
            var muscles = await workoutTrackerRepository.GetEntities<Muscle>(CancellationToken.None);
            if (muscles.Count > 0)
            {
                return; // Muscles already seeded
            }
            muscles = new List<Muscle>
            {
                new Muscle { Name = "Triceps", Description = "Muscles on the back of the upper arm responsible for extending the elbow and straightening the arm.", ImageUrl = "images/muscles/triceps.jpg" },
                new Muscle { Name = "Chest", Description = "Primarily the pectoral muscles, used for pushing movements and bringing the arms together across the body.", ImageUrl = "images/muscles/chest.jpg" },
                new Muscle { Name = "Shoulders", Description = "Includes the deltoid muscles, which help lift and rotate the arms in various directions.", ImageUrl = "images/muscles/shoulders.jpg" },
                new Muscle { Name = "Biceps", Description = "Front of the upper arm, responsible for bending the elbow and rotating the forearm.", ImageUrl = "images/muscles/biceps.jpg" },
                new Muscle { Name = "Core", Description = "Muscles around the trunk and pelvis, including abs and obliques, essential for stability, balance, and posture.", ImageUrl = "images/muscles/core.jpg" },
                new Muscle { Name = "Back", Description = "Covers upper and lower back muscles like the lats, traps, and erector spinae, used for pulling and posture.", ImageUrl = "images/muscles/back.jpg" },
                new Muscle { Name = "Forearms", Description = "Muscles of the lower arm that control grip strength and wrist movements.", ImageUrl = "images/muscles/forearms.jpg" },
                new Muscle { Name = "Upper Legs", Description = "Includes quads and hamstrings, vital for squatting, running, and jumping.", ImageUrl = "images/muscles/upper_legs.jpg" },
                new Muscle { Name = "Glutes", Description = "The muscles of the buttocks, important for hip movement, posture, and lower body strength.", ImageUrl = "images/muscles/glutes.jpg" },
                new Muscle { Name = "Lower Legs", Description = "Includes calves and muscles around the ankle, important for walking, running, and jumping.", ImageUrl = "images/muscles/lower_legs.jpg" },
            };
            await workoutTrackerRepository.AddRangeAsync(muscles, CancellationToken.None);
        }
    }
}
