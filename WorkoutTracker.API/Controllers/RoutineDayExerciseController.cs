using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.RoutineDayExerciseService;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.RoutineDayExercise;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for managing routine day exercises in the workout tracker application.
    /// </summary>
    [ApiController]
    [Route("routine-day-exercises")]
    public class RoutineDayExerciseController : ControllerBase
    {
        private readonly IRoutineDayExerciseService routineDayExerciseService;

        public RoutineDayExerciseController(IRoutineDayExerciseService routineDayExerciseService)
        {
            this.routineDayExerciseService = routineDayExerciseService;
        }

        /// <summary>
        /// Adds a new exercise to the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ResultModel<RoutineDayExerciseDto>>> AddRoutineDay(
            [FromBody] AddRoutineDayExerciseDto routine,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDayExerciseDto>
            {
                ResultObject = await routineDayExerciseService.AddRoutineDayExercise(routine, cancellationToken)
            });
        }

        /// <summary>
        /// Updates an existing exercise in the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultModel<RoutineDayExerciseDto>>> UpdateRoutineDay(
            [FromRoute] long id,
            [FromBody] UpdateRoutineDayExerciseDto routine,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDayExerciseDto>
            {
                ResultObject = await routineDayExerciseService.UpdateRoutineDayExercise(id, routine, cancellationToken)
            });
        }

        /// <summary>
        /// Deletes the exercise with the specified id
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteRoutineDay([FromRoute] long id, CancellationToken cancellationToken)
        {
            await routineDayExerciseService.DeleteRoutineDayExercise(id, cancellationToken);
            return Ok(new ResultModel { Message = $"Exercise with Id: {id} successfully deleted." });
        }
    }
}
