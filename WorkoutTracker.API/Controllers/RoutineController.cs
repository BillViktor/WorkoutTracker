using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services.RoutineService;
using WorkoutTracker.Shared.Dto.Pagination;
using WorkoutTracker.Shared.Dto.Result;
using WorkoutTracker.Shared.Dto.Routine;

namespace WorkoutTracker.API.Controllers
{
    /// <summary>
    /// Controller for managing routines in the workout tracker application.
    /// </summary>
    [ApiController]
    [Route("routines")]
    public class RoutineController : ControllerBase
    {
        private readonly IRoutineService routineService;

        public RoutineController(IRoutineService routineService)
        {
            this.routineService = routineService;
        }

        /// <summary>
        /// Returns a paginated, sorted and filtered list of routines
        /// </summary>
        [HttpGet("list")]
        public async Task<ActionResult<ResultModel<EntityResult<RoutineDto>>>> GetRoutines([FromQuery] EntityParameters parameters, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<EntityResult<RoutineDto>>
            {
                ResultObject = await routineService.GetRoutines(parameters, cancellationToken)
            });
        }

        /// <summary>
        /// Returns the routine with the specified id, including its days and exercises
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<ResultModel<RoutineDto>>> GetRoutine([FromRoute] long id, CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDto>
            {
                ResultObject = await routineService.GetRoutine(id, cancellationToken)
            });
        }

        /// <summary>
        /// Adds a new routine to the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<ResultModel<RoutineDto>>> AddRoutine(
            [FromBody] AddRoutineDto routine,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDto>
            {
                ResultObject = await routineService.AddRoutine(routine, cancellationToken)
            });
        }

        /// <summary>
        /// Updates an existing routine in the database
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResultModel<RoutineDto>>> UpdateRoutine(
            [FromRoute] long id,
            [FromBody] UpdateRoutineDto routine,
            CancellationToken cancellationToken)
        {
            return Ok(new ResultModel<RoutineDto>
            {
                ResultObject = await routineService.UpdateRoutine(id, routine, cancellationToken)
            });
        }

        /// <summary>
        /// Deletes the routine with the specified id
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResultModel>> DeleteRoutine([FromRoute] long id, CancellationToken cancellationToken)
        {
            await routineService.DeleteRoutine(id, cancellationToken);
            return Ok(new ResultModel { Message = $"Routine with Id: {id} successfully deleted." });
        }
    }
}
