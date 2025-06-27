using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Models;
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
        /// Returns a list of all muscles
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ResultModel<List<MuscleDto>>>> GetMuscles(CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<List<MuscleDto>>
            {
                ResultObject = await muscleService.GetMuscles(cancellationToken)
            });
        }
    }
}
