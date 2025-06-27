using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.API.Controllers
{
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
        /// Returns a list of all muscles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ResultModel<List<Muscle>>>> GetMuscles(CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<List<Muscle>>
            {
                ResultObject = await muscleService.GetMuscles(cancellationToken)
            });
        }
    }
}
