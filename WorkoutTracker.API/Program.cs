using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using WorkoutTracker.Business.Services;
using WorkoutTracker.Data;
using WorkoutTracker.Data.Repository;

namespace WorkoutTracker.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("default", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Database
            builder.Services.AddDbContext<WorkoutTrackerDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("WorkoutTrackerDb")));

            builder.Services.AddScoped<IWorkoutTrackerRepository, WorkoutTrackerRepository>();
            #endregion

            #region Services
            builder.Services.AddScoped<IMuscleService, MuscleService>();
            #endregion

            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(x => x.DocExpansion(DocExpansion.None));
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
