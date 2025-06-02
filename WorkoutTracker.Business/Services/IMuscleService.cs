using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.Business.Services
{
    public interface IMuscleService
    {
        Task<List<MuscleDto>> GetMuscles();
    }
}