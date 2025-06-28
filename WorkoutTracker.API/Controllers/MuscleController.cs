using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.API.Controllers
{
    [Authorize]
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
        /// Returns a paginated, sorted and filtered list of muscles
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost("list")]
        public async Task<ActionResult<ResultModel<EntityResult<Muscle>>>> GetMuscles(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<EntityResult<Muscle>>
            {
                ResultObject = await muscleService.GetMuscles(entityParameters, cancellationToken)
            });
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
        public async Task<ActionResult<ResultModel<Muscle>>> UpdateMuscle(Muscle muscle, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<Muscle>
            {
                ResultObject = await muscleService.UpdateMuscle(muscle, cancellationToken)
            });
        }
    }
}
