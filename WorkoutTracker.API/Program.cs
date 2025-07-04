using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using WorkoutTracker.API.Middleware;
using WorkoutTracker.Business.Services.Auth;
using WorkoutTracker.Business.Services.Email;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Business.Services.Image;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Data;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Repository;
using WorkoutTracker.Shared.Dto.Result;

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
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
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

            builder.Services.AddMemoryCache();
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
            builder.Services.AddScoped<IImageService, ImageService>();
            builder.Services.AddScoped<IMuscleService, MuscleService>();
            #endregion


            //Load Smtp Credentials
            builder.Configuration.AddJsonFile("smtpsettings.json", false);

            //Override the default model validation response
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value?.Errors?.Count > 0)
                        .SelectMany(kvp => kvp.Value.Errors.Select(e => $"[{kvp.Key}] {e.ErrorMessage}"))
                        .ToList();

                    var resultModel = new ResultModel(errors)
                    {
                        Message = "One or more validation errors occurred.",
                        Success = false
                    };

                    return new BadRequestObjectResult(resultModel);
                };
            });

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
                        await SeedData.SeedMusclesAsync(services);
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
