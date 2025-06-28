using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Data.Models;

namespace WorkoutTracker.Data
{
    public class WorkoutTrackerDbContext : IdentityDbContext<WorkoutTrackerUserModel>
    {
        public DbSet<Muscle> Muscles { get; set; }
        public DbSet<Exercise> Exercise { get; set; }

        public WorkoutTrackerDbContext(DbContextOptions<WorkoutTrackerDbContext> aOptions) : base(aOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
