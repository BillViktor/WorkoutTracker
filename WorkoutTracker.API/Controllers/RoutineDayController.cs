using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.RoutineDayService;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.RoutineDay;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for managing routine days in the workout tracker application.
    /// </summary>
    [ApiController]
    [Route("routine-days")]
    public class RoutineDayController : ControllerBase
    {
        private readonly IRoutineDayService routineDayService;

        public RoutineDayController(IRoutineDayService routineDayService)
        {
            this.routineDayService = routineDayService;
        }

        /// <summary>
        /// Adds a new day to the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ResultModel<RoutineDayDto>>> AddRoutineDay(
            [FromBody] AddRoutineDayDto routine,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDayDto>
            {
                ResultObject = await routineDayService.AddRoutineDay(routine, cancellationToken)
            });
        }

        /// <summary>
        /// Updates an existing day in the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultModel<RoutineDayDto>>> UpdateRoutineDay(
            [FromRoute] long id,
            [FromBody] UpdateRoutineDayDto routine,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDayDto>
            {
                ResultObject = await routineDayService.UpdateRoutineDay(id, routine, cancellationToken)
            });
        }

        /// <summary>
        /// Deletes the day with the specified id
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteRoutineDay([FromRoute] long id, CancellationToken cancellationToken)
        {
            await routineDayService.DeleteRoutineDay(id, cancellationToken);
            return Ok(new ResultModel { Message = $"Day with Id: {id} successfully deleted." });
        }
    }
}
