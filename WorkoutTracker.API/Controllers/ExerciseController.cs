using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
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
        [Authorize(Roles = "Admin")]
        [HttpPost("list")]
        public async Task<ActionResult<ResultModel<EntityResult<Exercise>>>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<EntityResult<Exercise>>
            {
                ResultObject = await exerciseService.GetExercises(entityParameters, cancellationToken)
            });
        }

        /// <summary>
        /// Adds a new exercise to the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ResultModel<Exercise>>> AddExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<Exercise>
            {
                ResultObject = await exerciseService.AddExercise(exercise, cancellationToken)
            });
        }

        /// <summary>
        /// Updates an exercise in the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<ResultModel<Exercise>>> UpdateExercise(Exercise exercise, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<Exercise>
            {
                ResultObject = await exerciseService.UpdateExercise(exercise, cancellationToken)
            });
        }

        /// <summary>
        /// Deletes the exercise with the specified id
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteExercise(long id, CancellationToken cancellationToken)
        {
            await exerciseService.DeleteExercise(id, cancellationToken);
            return Ok(new ResultModel { Message = $"Exercise with Id: {id} successfully deleted." });
        }
    }
}
