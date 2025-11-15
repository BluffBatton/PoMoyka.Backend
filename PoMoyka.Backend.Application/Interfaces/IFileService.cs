using Microsoft.AspNetCore.Http;

namespace PoMoyka.Backend.Application.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file, string directory);
        Task DeleteFileAsync(string filePath);
        Task<string> GetFileUrlAsync(string filePath);
    }
}
