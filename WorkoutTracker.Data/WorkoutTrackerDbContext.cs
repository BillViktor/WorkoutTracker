using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Shared.Models;

namespace WorkoutTracker.Data
{
    public class WorkoutTrackerDbContext : DbContext
    {
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Exercise> Exercise { get; set; }

        public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> aOptions) : base(aOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseMuscle>(e =>
            {
                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => new { e.ExerciseId, e.MuscleId }).IsUnique();
            });

            modelBuilder.Entity<Muscle>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(128);
                e.Property(p => p.Description).HasMaxLength(2048);
                e.Property(p => p.ImageUrl).HasMaxLength(256);

                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => e.Name).IsUnique();

                e.HasMany(p => p.Exercises)
                    .WithOne(e => e.Muscle)
                        .HasForeignKey(e => e.MuscleId)
                            .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Exercise>(e =>
            {
                e.Property(p => p.Name).HasMaxLength(128);
                e.Property(p => p.Description).HasMaxLength(2048);
                e.Property(p => p.ImageUrl).HasMaxLength(256);

                e.Property(p => p.CreateDate).HasDefaultValueSql("getdate()");

                e.HasIndex(e => e.Name).IsUnique();

                e.HasMany(p => p.Muscles)
                    .WithOne(e => e.Exercise)
                        .HasForeignKey(e => e.ExerciseId)
                            .OnDelete(DeleteBehavior.Cascade);

            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
