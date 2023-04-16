using DeepfakeDetectionFramework.Data;
using System.Net.Http;

namespace DeepfakeDetectionFramework.Interfaces;

public interface IFileService
{
    Task<(string, string, ProcessingType)> DownloadFile(string uri);
    Task<(string, string, ProcessingType)> SaveUploadedFile(IFormFile file);
}
