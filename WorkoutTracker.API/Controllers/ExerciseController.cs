using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;

namespace WorkoutTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExerciseController : ControllerBase
    {
        private readonly ILogger<ExerciseController> logger;
        private readonly IExerciseService exerciseService;

        public ExerciseController(ILogger<ExerciseController> logger, IExerciseService exerciseService)
        {
            this.logger = logger;
            this.exerciseService = exerciseService;
        }

        [HttpPost]
        public async Task<EntityResult<Exercise>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            return await exerciseService.GetExercises(entityParameters, cancellationToken);
        }

        [HttpDelete("{id}")]
        public async Task DeleteExercise(long id, CancellationToken cancellationToken)
        {
            await exerciseService.DeleteExercise(id, cancellationToken);
        }
    }
}
