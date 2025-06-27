using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using WorkoutTracker.API.Middleware;
using WorkoutTracker.Business.Services.Auth;
using WorkoutTracker.Business.Services.Email;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Data;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;

namespace WorkoutTracker.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            #region Auth
            builder.Services.AddAuthorization();
            builder.Services.AddIdentityApiEndpoints<WorkoutTrackerUserModel>
                (opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Lockout = new LockoutOptions
                    {
                        DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
                        AllowedForNewUsers = true,
                        MaxFailedAccessAttempts = 5
                    };
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<WorkoutTrackerDbContext>();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => opt.TokenLifespan = TimeSpan.FromHours(1));
            #endregion

            #region Database
            builder.Services.AddDbContext<WorkoutTrackerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WorkoutTrackerDb")));

            builder.Services.AddScoped<IWorkoutTrackerRepository, WorkoutTrackerRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IEmailService, EmailService>();
            builder.Services.AddScoped<IExerciseService, ExerciseService>();
            builder.Services.AddScoped<IMuscleService, MuscleService>();
            #endregion

            //Load Smtp Credentials
            builder.Configuration.AddJsonFile("smtpsettings.json", false);

            var app = builder.Build();

            //Middleware for exception handling
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x => x.DocExpansion(DocExpansion.None));

                var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
                app.UseCors(options => options.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod().AllowCredentials());

                //Seed data in development environment
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    if (app.Environment.IsDevelopment())
                    {
                        await SeedData.SeedUsersAndRolesAsync(services);
                    }
                }
            }

            app.UseCors("default");

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
