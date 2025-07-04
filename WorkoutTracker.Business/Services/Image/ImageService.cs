using Microsoft.AspNetCore.Http;
using WorkoutTracker.Shared.Constants;

namespace WorkoutTracker.Business.Services.Image
{
    /// <summary>
    /// Service for handling image-related operations such as validation and saving images.
    /// </summary>
    public class ImageService : IImageService
    {
        /// <summary>
        /// Validates and saves an image file to the specified file path.
        /// </summary>
        public async Task SaveImage(IFormFile file, string filePath, CancellationToken cancellationToken)
        {
            //Check if file is null or empty
            if (file == null || file.Length == 0)
            {
                throw new InvalidOperationException("Image file is empty.");
            }

            //Validate content type
            if (!file.ContentType.StartsWith("image/"))
            {
                throw new InvalidOperationException("File must be an image.");
            }

            //Check file size
            if (file.Length > ImageConstants.MaxFileSizeInBytes)
            {
                throw new InvalidOperationException($"Image must be smaller than {ImageConstants.MaxFileSizeInBytes / 1024}KB.");
            }

            //Validate extension
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!ImageConstants.AllowedFileExtensions.Contains(fileExtension))
            {
                throw new InvalidOperationException($"Image must be one of the following formats: {string.Join(", ", ImageConstants.AllowedFileExtensions)}.");
            }

            //Save the image to the specified file path
            using (var stream = new FileStream(Path.Combine("wwwroot", filePath), FileMode.Create))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }
        }

        /// <summary>
        /// Deletes an image file from the specified file path if it exists.
        /// </summary>
        public void DeleteImage(string filePath)
        {
            filePath = Path.Combine("wwwroot", filePath);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
