using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Shared.Models;
using WorkoutTracker.Shared.Models.Pagination;
using WorkoutTracker.Shared.Models.Result;

namespace WorkoutTracker.API.Controllers
{
    [ApiController]
    [Route("exercise")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService exerciseService;

        public ExerciseController(IExerciseService exerciseService)
        {
            this.exerciseService = exerciseService;
        }

        /// <summary>
        /// Returns a paginated, sortered and filtered list of exercises
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ResultModel<EntityResult<Exercise>>>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<EntityResult<Exercise>>
            {
                ResultObject = await exerciseService.GetExercises(entityParameters, cancellationToken)
            });
        }

        /// <summary>
        /// Deletes the exercise with the specified id
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteExercise(long id, CancellationToken cancellationToken)
        {
            await exerciseService.DeleteExercise(id, cancellationToken);
            return Ok(new ResultModel { Message = $"Exercise with Id: {id} successfully deleted." });
        }
    }
}
