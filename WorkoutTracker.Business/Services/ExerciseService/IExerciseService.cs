﻿using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;

namespace WorkoutTracker.Business.Services.ExerciseService
{
    /// <summary>
    /// Interface for exercise service operations.
    /// </summary>
    public interface IExerciseService
    {
        Task<Exercise> AddExercise(Exercise exercise, CancellationToken cancellationToken);
        Task DeleteExercise(long id, CancellationToken cancellationToken);
        Task<ExerciseDto> GetExercise(long id, CancellationToken cancellationToken);
        Task<EntityResult<ExerciseDto>> GetExercises(ExerciseParameters parameters, CancellationToken cancellationToken);
        Task<Exercise> UpdateExercise(Exercise exercise, CancellationToken cancellationToken);
    }
}