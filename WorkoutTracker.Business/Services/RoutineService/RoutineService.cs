using Microsoft.Extensions.Configuration;
using WorkoutTracker.Data.Repository;

namespace WorkoutTracker.Business.Services.RoutineService
{
    /// <summary>
    /// Service for managing muscle-related operations.
    /// </summary>
    public class RoutineService
    {
        private readonly IWorkoutTrackerRepository workoutTrackerRepository;
        private readonly IConfiguration configuration;

        public RoutineService(IWorkoutTrackerRepository workoutTrackerRepository, IConfiguration configuration)
        {
            this.workoutTrackerRepository = workoutTrackerRepository;
            this.configuration = configuration;
        }
    }
}
