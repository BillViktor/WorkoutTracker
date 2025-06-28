using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for managing muscles in the workout tracker application.
    /// </summary>
    [ApiController]
    [Route("muscle")]
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
        [HttpPut]
        public async Task<ActionResult<ResultModel<MuscleDto>>> UpdateMuscle(MuscleDto muscle, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<MuscleDto>
            {
                ResultObject = await muscleService.UpdateMuscle(muscle, cancellationToken)
            });
        }
    }
}
