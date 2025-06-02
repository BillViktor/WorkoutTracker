using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Web.Clients
{
    public interface IExerciseClient
    {
        Task<List<MuscleDto>> GetMuscles();
    }
}
