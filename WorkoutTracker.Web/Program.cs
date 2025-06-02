using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WorkoutTracker.Web.Clients;
using WorkoutTracker.Web.ViewModels;

namespace WorkoutTracker.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            #region Clients
            builder.Services.AddScoped(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.Configuration["WorkoutTrackerApiBaseUrl"])
            });

            builder.Services.AddScoped<IExerciseClient, ExerciseClient>();
            #endregion

            #region ViewModels
            builder.Services.AddScoped<IExerciseViewModel, ExerciseViewModel>();
            #endregion

            await builder.Build().RunAsync();
        }
    }
}
