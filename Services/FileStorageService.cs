using Microsoft.AspNetCore.Mvc;

namespace ProgettoFinaleCaroppoli1.Services 
{ }

public class FileStorageService
{
    private readonly string _basePath;

    public FileStorageService(IConfiguration config)
    {
        _basePath = config["CartellaFiles"] ?? throw new ArgumentNullException("CartellaFiles non configurata");
    }

    public async Task<string> SaveFileAsync(IFormFile file)
    {
        var folder = Path.Combine(_basePath, DateTime.Now.ToString("yyyy-MM-dd"));
        Directory.CreateDirectory(folder);

        var filePath = Path.Combine(folder, file.FileName);
        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return filePath;
    }
}


