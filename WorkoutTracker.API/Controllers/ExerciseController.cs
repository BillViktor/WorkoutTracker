using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Models;
using WorkoutTracker.Business.Services.ExerciseService;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto.Exercise;
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
        [HttpGet("list")]
        public async Task<ActionResult<ResultModel<EntityResult<ExerciseDto>>>> GetExercises([FromQuery] ExerciseParameters parameters, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<EntityResult<ExerciseDto>>
            {
                ResultObject = await exerciseService.GetExercises(parameters, cancellationToken)
            });
        }

        /// <summary>
        /// Returns the exercise with the specified id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel<ExerciseDto>>> GetExercise([FromRoute] long id, CancellationToken cancellationToken)
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
        public async Task<ActionResult<ResultModel<ExerciseDto>>> AddExercise(
            [FromForm] AddExerciseDto exercise, 
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<ExerciseDto>
            {
                ResultObject = await exerciseService.AddExercise(exercise, cancellationToken)
            });
        }

        /// <summary>
        /// Updates an exercise in the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultModel<ExerciseDto>>> UpdateExercise(
            [FromRoute] long id, 
            [FromForm] UpdateExerciseDto exercise, 
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<ExerciseDto>
            {
                ResultObject = await exerciseService.UpdateExercise(id, exercise, cancellationToken)
            });
        }

        /// <summary>
        /// Deletes the exercise with the specified id
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteExercise([FromRoute] long id, CancellationToken cancellationToken)
        {
            await exerciseService.DeleteExercise(id, cancellationToken);
            return Ok(new ResultModel { Message = $"Exercise with Id: {id} successfully deleted." });
        }
    }
}
