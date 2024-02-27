namespace Cursova.Interfaces
{
    public interface IImageServise
    {
        Task<string> SaveImage(IFormFile file);
    }
}
