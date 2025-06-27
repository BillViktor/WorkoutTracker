using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using WorkoutTracker.Data.Models;

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
    }
}
