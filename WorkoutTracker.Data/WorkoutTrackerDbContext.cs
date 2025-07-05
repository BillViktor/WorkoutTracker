using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Data.Models.Routine;

namespace WorkoutTracker.Data
{
    /// <summary>
    /// Represents the database context for the Workout Tracker application.
    /// </summary>
    public class WorkoutTrackerDbContext : IdentityDbContext<WorkoutTrackerUserModel>
    {
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Exercise> Exercise { get; set; }

        public DbSet<WorkoutRoutine> Routines { get; set; }
        public DbSet<WorkoutRoutineDay> RoutineDays { get; set; }
        public DbSet<WorkoutRoutineDayExercise> RoutineDayExercises { get; set; }

        public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> aOptions) : base(aOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WorkoutRoutineDayExercise>(e =>
            {
                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => new { e.WorkoutRoutineDayId, e.SortOrder }).IsUnique();

                e.HasOne(e => e.Exercise)
                    .WithMany()
                        .HasForeignKey(e => e.ExerciseId)
                            .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkoutRoutineDay>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(128);

                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => new { e.RoutineId, e.SortOrder }).IsUnique();

                e.HasMany(e => e.Exercises)
                    .WithOne(e => e.WorkoutRoutineDay)
                        .HasForeignKey(e => e.WorkoutRoutineDayId)
                            .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WorkoutRoutine>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(128);
                e.Property(p => p.Description).HasMaxLength(2048);

                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasMany(e => e.Days)
                    .WithOne(e => e.Routine)
                        .HasForeignKey(e => e.RoutineId)
                            .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Muscle>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(128);
                e.Property(p => p.Description).HasMaxLength(2048);
                e.Property(p => p.ImageUrl).HasMaxLength(256);

                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<Exercise>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(128);
                e.Property(p => p.Description).HasMaxLength(2048);
                e.Property(p => p.ImageUrl).HasMaxLength(256);

                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => e.Name).IsUnique();

                e.HasOne(p => p.PrimaryMuscle)
                    .WithMany()
                        .HasForeignKey(e => e.PrimaryMuscleId)
                            .OnDelete(DeleteBehavior.Restrict);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
