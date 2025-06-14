using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.MuscleService;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MuscleController : ControllerBase
    {
        private readonly ILogger<MuscleController> logger;
        private readonly IMuscleService muscleService;

        public MuscleController(ILogger<MuscleController> logger, IMuscleService muscleService)
        {
            this.logger = logger;
            this.muscleService = muscleService;
        }

        [HttpPost]
        public async Task<EntityResult<Muscle>> GetMuscles(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            return await muscleService.GetMuscles(entityParameters, cancellationToken);
        }
    }
}
