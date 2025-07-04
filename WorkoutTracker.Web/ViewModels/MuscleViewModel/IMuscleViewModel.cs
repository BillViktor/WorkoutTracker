﻿using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Web.Models;
using WorkoutTracker.Web.ViewModels.Base;

namespace WorkoutTracker.Web.ViewModels.MuscleViewModel
{
    /// <summary>
    /// ViewModel interface for managing muscles in the application.
    /// </summary>
    public interface IMuscleViewModel : IBaseViewModel
    {
        List<MuscleDto> Muscles { get; set; }
        Task GetMuscles();
        Task<MuscleDto> Update(long id, UpdateMuscleClientDto muscle);
    }
}
