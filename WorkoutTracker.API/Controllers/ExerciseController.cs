using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for managing exercises in the workout tracker application.
    /// </summary>
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
        /// Returns a paginated, sorted and filtered list of exercises
        /// </summary>
        [HttpPost("list")]
        public async Task<ActionResult<ResultModel<EntityResult<ExerciseDto>>>> GetExercises(EntityParameters entityParameters, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<EntityResult<ExerciseDto>>
            {
                ResultObject = await exerciseService.GetExercises(entityParameters, cancellationToken)
            });
        }

        /// <summary>
        /// Returns the exercise with the specified id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel<ExerciseDto>>> GetExercise(long id, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<ExerciseDto>
            {
                ResultObject = await exerciseService.GetExercise(id, cancellationToken)
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
