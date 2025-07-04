using Microsoft.AspNetCore.Http;

namespace WorkoutTracker.Business.Services.Image
{
    public interface IImageService
    {
        void DeleteImage(string filePath);
        Task SaveImage(IFormFile file, string filePath, CancellationToken cancellationToken);
    }
}