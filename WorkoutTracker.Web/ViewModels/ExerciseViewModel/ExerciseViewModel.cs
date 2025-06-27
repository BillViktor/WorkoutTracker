using WorkoutTracker.Shared.Models;
using WorkoutTracker.Web.Clients.ExerciseClient;

namespace WorkoutTracker.Web.ViewModels.ExerciseViewModel
{
    public class ExerciseViewModel : EntityViewModel<Exercise>, IExerciseViewModel
    {
        private readonly IExerciseClient exerciseClient;

        //Constructor
        public ExerciseViewModel(IExerciseClient exerciseClient)
        {
            this.exerciseClient = exerciseClient;
        }

        #region Methods
        public override async Task GetEntities()
        {
            var result = await exerciseClient.GetExercises(EntityParameters);

            if(result.Success)
            {
                Entities = result.ResultObject;
            }
        }

        public override async Task<bool> Add(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Delete(Exercise exercise)
        {
            var result = await exerciseClient.DeleteExercise(exercise.Id);

            return result.Success;
        }

        public override async Task<bool> Update(Exercise exercise)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
