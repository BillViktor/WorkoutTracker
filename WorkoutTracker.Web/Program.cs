using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WorkoutTracker.Web.Clients.AuthClient;
using WorkoutTracker.Web.Clients.ExerciseClient;
using WorkoutTracker.Web.Clients.MuscleClient;
using WorkoutTracker.Web.Clients.RoutineClient;
using WorkoutTracker.Web.Identity;
using WorkoutTracker.Web.ViewModels.AuthViewModel;
using WorkoutTracker.Web.ViewModels.ExerciseViewModel;
using WorkoutTracker.Web.ViewModels.MuscleViewModel;
using WorkoutTracker.Web.ViewModels.RoutineViewModel;

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

            #region Auth
            builder.Services.AddTransient<CookieHandler>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<CookieAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(p => p.GetService<CookieAuthenticationStateProvider>());
            #endregion


            #region Clients
            string baseUrl = builder.Configuration["WorkoutTrackerApiBaseUrl"];

            builder.Services.AddHttpClient<IAuthClient, AuthClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "auth/");
            }).AddHttpMessageHandler<CookieHandler>();

            builder.Services.AddHttpClient<IExerciseClient, ExerciseClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "exercises");
            }).AddHttpMessageHandler<CookieHandler>();

            builder.Services.AddHttpClient<IMuscleClient, MuscleClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "muscles");
            }).AddHttpMessageHandler<CookieHandler>();

            builder.Services.AddHttpClient<IRoutineClient, RoutineClient>(client =>
            {
                client.BaseAddress = new Uri(baseUrl + "routines");
            }).AddHttpMessageHandler<CookieHandler>();
            #endregion


            #region ViewModels
            builder.Services.AddScoped<IAuthViewModel, AuthViewModel>();
            builder.Services.AddScoped<IExerciseViewModel, ExerciseViewModel>();
            builder.Services.AddScoped<IMuscleViewModel, MuscleViewModel>();
            builder.Services.AddScoped<IRoutineViewModel, RoutineViewModel>();
            #endregion


            await builder.Build().RunAsync();
        }
    }
}
