using Microsoft.AspNetCore.Mvc;
using WorkoutTracker.Business.Services;
using WorkoutTracker.Data.Models;
using WorkoutTracker.Shared.Dto;

namespace WorkoutTracker.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MuscleController : ControllerBase
    {
        private readonly ILogger<MuscleController> mLogger;
        private readonly IMuscleService mMuscleService;

        public MuscleController(ILogger<MuscleController> aLogger, IMuscleService aMuscleService)
        {
            mLogger = aLogger;
            mMuscleService = aMuscleService;
        }

        [HttpGet]
        public async Task<List<MuscleDto>> GetMuscles()
        {
            return await mMuscleService.GetMuscles();
        }

        [HttpPost]
        public string AddMuscle()
        {
            return "Muscle added";
        }

        [HttpPut("{aId}")]
        public string UpdateMuscle(long aId)
        {
            return "Muscle updated";
        }

        [HttpDelete("{aId}")]
        public string DeleteMuscle(long aId)
        {
            return "Muscle deleted";
        }
    }
}
