using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Models;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for managing muscles in the workout tracker application.
    /// </summary>
    [ApiController]
    [Route("muscles")]
    public class MuscleController : ControllerBase
    {
        private readonly IMuscleService muscleService;

        public MuscleController(IMuscleService muscleService)
        {
            this.muscleService = muscleService;
        }

        /// <summary>
        /// Returns a list of all muscles.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ResultModel<List<MuscleDto>>>> GetMuscles(CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<List<MuscleDto>>
            {
                ResultObject = await muscleService.GetMuscles(cancellationToken)
            });
        }

        /// <summary>
        /// Updates a muscle in the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultModel<MuscleDto>>> UpdateMuscle(
            [FromRoute] long id,
            [FromForm] UpdateMuscleDto muscle,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<MuscleDto>
            {
                ResultObject = await muscleService.UpdateMuscle(id, muscle, cancellationToken)
            });
        }
    }
}
