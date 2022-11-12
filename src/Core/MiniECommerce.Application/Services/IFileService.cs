using Microsoft.AspNetCore.Http;

namespace MiniECommerce.Application.Services;

public interface IFileService
{
    Task<List<(string path, string name)>> UploadAsync(string directoryPath, IFormFileCollection files);
    Task<string> RenameAsync(string name, string? path);
    Task<bool> CopyAsync(string path, IFormFile file);
}
