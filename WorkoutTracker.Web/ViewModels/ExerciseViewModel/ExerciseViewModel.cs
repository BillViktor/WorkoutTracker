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
            Entities = await exerciseClient.GetExercises(EntityParameters);
        }

        public override async Task<bool> Add(Exercise exercise)
        {
            throw new NotImplementedException();
        }

        public override async Task<bool> Delete(Exercise exercise)
        {
            return await exerciseClient.DeleteExercise(exercise.Id);
        }

        public override async Task<bool> Update(Exercise exercise)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
