using Cursova.Interfaces;

namespace Cursova.Services
{
    public class ImageServise : IImageServise
    {
        private readonly string MyPath = @"D:\Програми\Cursova\Cursova\wwwroot\images\" ;
        public async Task<string> SaveImage(IFormFile file)
        {
            if (file.Length > 0)
            {
                
                string filePath = Path.Combine(MyPath, file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                {
                    await file.CopyToAsync(fileStream);
                    return file.FileName;
                }
            }
            return null;
        }
    }
}
