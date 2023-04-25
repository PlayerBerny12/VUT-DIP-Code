using DeepfakeDetectionFramework.Data;
using DeepfakeDetectionFramework.Interfaces;
using System.Security.Cryptography;

namespace DeepfakeDetectionFramework.Services;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public FileService(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<(string, string, ProcessingType)> DownloadFile(string uri)
    {
        string filename = Path.GetFileName(uri);
        using HttpResponseMessage result = await _httpClient.GetAsync(uri);

        if (!result.IsSuccessStatusCode)
        {
            throw new Exception("Unable to donwload data");

        }

        ProcessingType? fileType = GetFileType(filename, result.Content.Headers.ContentType!.ToString());
        if (fileType == null)
        {
            throw new Exception("Unsupported file type");
        }

        string savedFilename = $"{Guid.NewGuid()}{Path.GetExtension(filename)}";
        string filePath = Path.Combine(_configuration["ProcessingDataPath"]!, savedFilename);

        using var stream = new FileStream(filePath, FileMode.Create);
        await result.Content.CopyToAsync(stream);

        return (savedFilename, CalculateFileChecksum(stream), fileType.Value);
    }

    public async Task<(string, string, ProcessingType)> SaveUploadedFile(IFormFile file)
    {
        string filename = file.FileName;

        ProcessingType? fileType = GetFileType(filename, file.ContentType.ToString());
        if (fileType == null)
        {
            throw new Exception("Unsupported file type");
        }

        string savedFilename = $"{Guid.NewGuid()}{Path.GetExtension(filename)}";
        string filePath = Path.Combine(_configuration["ProcessingDataPath"]!, savedFilename);

        using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        return (savedFilename, CalculateFileChecksum(stream), fileType.Value);
    }

    public void RemoveFile(string filename)
    {
        string filePath = Path.Combine(_configuration["ProcessingDataPath"]!, filename);
        File.Delete(filePath);
    }

    private string CalculateFileChecksum(FileStream fileStream)
    {
        using SHA256 sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(fileStream));
    }

    private ProcessingType? GetFileType(string filenameWithExtension, string contentType)
    {
        Dictionary<string, List<string>> supportedFileTypes = new();
        _configuration.GetRequiredSection("SupportedFileTypes").Bind(supportedFileTypes);

        if (!supportedFileTypes.TryGetValue(Path.GetExtension(filenameWithExtension), out List<string>? expectedContentType))
        {
            return null;
        }

        if (!expectedContentType.Contains(contentType))
        {
            return null;
        }

        return contentType.Split('/')[0] switch
        {
            "video" => ProcessingType.Video,
            "image" => ProcessingType.Image,
            "audio" => ProcessingType.Audio,
            _ => null,
        };
    }
}
